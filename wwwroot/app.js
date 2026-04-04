/** Sunucu ile aynı kurallar (ScoringRules.cs) */
const POINT_CORRECT = 2;
const POINT_WRONG = -1;
const LOSE_SCORE = -10;

const $ = (id) => document.getElementById(id);

let quiz = null;
/** @type {any[]} */
let quizSummaries = [];
let selectedQuizId = null;
let selectedCategoryTitle = "";
let questionIndex = 0;
let score = 0;
/** @type {{ questionId: number, selectedOptionIndex: number }[]} */
let answers = [];
let locked = false;
let activePlayerName = "";
/** @type {ReturnType<typeof setInterval> | null} */
let roundTimer = null;

function show(id) {
  ["screen-start", "screen-categories", "screen-play", "screen-lost", "screen-done"].forEach((s) => {
    $(s).classList.toggle("hidden", s !== id);
  });
}

function currentPlayerName() {
  const stored = (activePlayerName || "").trim();
  if (stored) return stored;
  return ($("player-name").value || "").trim();
}

function updateSidebarTitle() {
  const span = $("sidebar-category");
  const label = selectedCategoryTitle || quiz?.title || "";
  span.textContent = label ? `— ${label}` : "";
}

function scoreMeterHi() {
  const n = quiz?.questions?.length ?? 1;
  return Math.max(n * POINT_CORRECT, 4);
}

function updateMeter() {
  const meter = $("score-meter");
  const lo = LOSE_SCORE;
  const hi = scoreMeterHi();
  const t = (score - lo) / (hi - lo);
  const pct = Math.max(0, Math.min(100, t * 100));
  meter.style.width = `${pct}%`;
  if (score <= -6) {
    meter.style.background = `linear-gradient(90deg, var(--bad), var(--warn))`;
  } else if (score <= 0) {
    meter.style.background = `linear-gradient(90deg, var(--warn), var(--accent))`;
  } else {
    meter.style.background = `linear-gradient(90deg, var(--ok), var(--accent))`;
  }
}

function setScoreDisplay() {
  $("score-value").textContent = String(score);
  updateMeter();
}

function clearRoundTimer() {
  if (roundTimer !== null) {
    clearInterval(roundTimer);
    roundTimer = null;
  }
  const el = $("question-timer");
  el.classList.remove("timer-warn");
}

function startRoundTimer() {
  clearRoundTimer();
  const sec = quiz?.secondsPerRound ?? quiz?.secondsPerQuestion ?? 60;
  let left = sec;
  const el = $("question-timer");
  el.textContent = String(left);
  el.classList.toggle("timer-warn", left <= 10);
  roundTimer = setInterval(() => {
    left -= 1;
    el.textContent = String(left);
    el.classList.toggle("timer-warn", left <= 10);
    if (left <= 0) {
      clearRoundTimer();
      void onRoundTimeout();
    }
  }, 1000);
}

async function api(path, options = {}) {
  const res = await fetch(path, {
    headers: { "Content-Type": "application/json", Accept: "application/json" },
    ...options,
  });
  if (!res.ok) {
    const t = await res.text();
    throw new Error(t || res.statusText);
  }
  const ct = res.headers.get("content-type");
  if (ct && ct.includes("application/json")) return res.json();
  return null;
}

async function refreshLeaderboard() {
  const qid = selectedQuizId ?? quiz?.id;
  const listEl = $("leaderboard-list");
  const emptyEl = $("leaderboard-empty");

  if (!qid) {
    listEl.innerHTML = "";
    listEl.classList.add("hidden");
    emptyEl.textContent = "Skorları görmek için önce bir kategori seç.";
    emptyEl.classList.remove("hidden");
    return;
  }

  const url = `/api/leaderboard?quizId=${qid}&take=15`;
  try {
    const rows = await api(url);
    listEl.innerHTML = "";
    if (!rows.length) {
      listEl.classList.add("hidden");
      emptyEl.textContent = "Bu kategoride henüz kayıt yok.";
      emptyEl.classList.remove("hidden");
      return;
    }
    listEl.classList.remove("hidden");
    emptyEl.classList.add("hidden");
    for (const row of rows) {
      const li = document.createElement("li");
      const when = new Date(row.playedAt).toLocaleString("tr-TR", {
        day: "2-digit",
        month: "short",
        hour: "2-digit",
        minute: "2-digit",
      });
      li.innerHTML = `<span class="lb-name">${escapeHtml(row.playerName)}</span><span class="lb-meta">${row.finalScore} puan · ${row.correctCount} doğru · ${when}</span>`;
      listEl.appendChild(li);
    }
  } catch {
    listEl.innerHTML = "";
    listEl.classList.add("hidden");
    emptyEl.textContent = "Skorlar yüklenemedi.";
    emptyEl.classList.remove("hidden");
  }
}

function escapeHtml(s) {
  const d = document.createElement("div");
  d.textContent = s;
  return d.innerHTML;
}

function submitPayload() {
  return JSON.stringify({
    playerName: currentPlayerName(),
    sessionQuestionIds: quiz.questions.map((q) => q.id),
    answers,
  });
}

function renderCategoryGrid() {
  const root = $("category-grid");
  root.innerHTML = "";
  for (const s of quizSummaries) {
    const btn = document.createElement("button");
    btn.type = "button";
    btn.className = "category-card";
    btn.innerHTML = `<span class="cat-title">${escapeHtml(s.title)}</span><span class="cat-meta">${s.questionCount} havuz · tur ${s.questionsPerRound}</span>`;
    btn.addEventListener("click", () => void selectCategory(s.id, s.title));
    root.appendChild(btn);
  }
}

async function continueToCategories() {
  const name = ($("player-name").value || "").trim();
  if (!name) {
    alert("Lütfen adını yaz.");
    $("player-name").focus();
    return;
  }
  activePlayerName = name;
  selectedQuizId = null;
  selectedCategoryTitle = "";
  quiz = null;
  updateSidebarTitle();

  try {
    quizSummaries = await api("/api/quizzes");
    if (!quizSummaries.length) {
      alert("Kategori bulunamadı.");
      return;
    }
  } catch (e) {
    alert("Kategoriler yüklenemedi. API çalışıyor mu? (" + (e && e.message) + ")");
    return;
  }

  $("greet-name").textContent = name;
  renderCategoryGrid();
  show("screen-categories");
  void refreshLeaderboard();
}

async function selectCategory(id, title) {
  selectedQuizId = id;
  selectedCategoryTitle = title;
  updateSidebarTitle();
  try {
    quiz = await api(`/api/quizzes/${id}`);
  } catch (e) {
    alert("Quiz yüklenemedi. (" + (e && e.message) + ")");
    return;
  }
  resetGame();
  $("playing-name").textContent = currentPlayerName();
  $("playing-category").textContent = quiz.title;
  show("screen-play");
  renderQuestion();
  startRoundTimer();
  void refreshLeaderboard();
}

function renderQuestion() {
  const q = quiz.questions[questionIndex];
  $("q-current").textContent = String(questionIndex + 1);
  $("q-total").textContent = String(quiz.questions.length);
  $("question-text").textContent = q.text;
  $("feedback").textContent = "";
  $("feedback").className = "feedback";

  const opts = $("options");
  opts.innerHTML = "";
  q.options.forEach((label, idx) => {
    const btn = document.createElement("button");
    btn.type = "button";
    btn.textContent = `${String.fromCharCode(65 + idx)}) ${label}`;
    btn.addEventListener("click", () => onChoose(idx));
    opts.appendChild(btn);
  });
  locked = false;
}

async function onRoundTimeout() {
  if (locked) return;
  locked = true;
  clearRoundTimer();
  const buttons = [...$("options").querySelectorAll("button")];
  buttons.forEach((b) => {
    b.disabled = true;
  });

  for (let i = questionIndex; i < quiz.questions.length; i++) {
    const q = quiz.questions[i];
    answers.push({ questionId: q.id, selectedOptionIndex: -1 });
    score += POINT_WRONG;
    if (score <= LOSE_SCORE) {
      setScoreDisplay();
      $("feedback").textContent = "Süre doldu!";
      $("feedback").className = "feedback bad";
      setTimeout(() => void submitAndLose(), 450);
      return;
    }
  }
  setScoreDisplay();
  $("feedback").textContent = "Süre doldu!";
  $("feedback").className = "feedback bad";
  setTimeout(() => void submitAndWin(), 450);
}

async function onChoose(selectedIndex) {
  if (locked) return;
  locked = true;
  const q = quiz.questions[questionIndex];
  const buttons = [...$("options").querySelectorAll("button")];
  buttons.forEach((b) => {
    b.disabled = true;
  });

  let isCorrect = false;
  try {
    const check = await api(`/api/quizzes/${quiz.id}/questions/${q.id}/check`, {
      method: "POST",
      body: JSON.stringify({ selectedOptionIndex: selectedIndex }),
    });
    isCorrect = check.isCorrect;
  } catch {
    $("feedback").textContent = "Sunucu hatası. Tekrar dene.";
    $("feedback").className = "feedback bad";
    locked = false;
    buttons.forEach((b) => {
      b.disabled = false;
    });
    return;
  }

  answers.push({ questionId: q.id, selectedOptionIndex: selectedIndex });
  const delta = isCorrect ? POINT_CORRECT : POINT_WRONG;
  score += delta;
  setScoreDisplay();

  $("feedback").textContent = isCorrect ? `Doğru! +${POINT_CORRECT}` : `Yanlış! ${POINT_WRONG}`;
  $("feedback").className = `feedback ${isCorrect ? "ok" : "bad"}`;

  if (score <= LOSE_SCORE) {
    clearRoundTimer();
    setTimeout(() => void submitAndLose(), 450);
    return;
  }

  if (questionIndex + 1 >= quiz.questions.length) {
    clearRoundTimer();
    setTimeout(() => void submitAndWin(), 450);
    return;
  }

  setTimeout(() => {
    questionIndex += 1;
    locked = false;
    renderQuestion();
  }, 650);
}

async function submitAndLose() {
  clearRoundTimer();
  try {
    await api(`/api/quizzes/${quiz.id}/submit`, {
      method: "POST",
      body: submitPayload(),
    });
  } catch {
    /* özet zorunlu değil */
  }
  $("lost-score").textContent = String(score);
  show("screen-lost");
  void refreshLeaderboard();
}

async function submitAndWin() {
  clearRoundTimer();
  let summary = null;
  try {
    summary = await api(`/api/quizzes/${quiz.id}/submit`, {
      method: "POST",
      body: submitPayload(),
    });
  } catch {
    $("done-score").textContent = String(score);
    $("done-correct").textContent = "?";
    show("screen-done");
    void refreshLeaderboard();
    return;
  }
  $("done-score").textContent = String(summary.finalScore);
  $("done-correct").textContent = String(summary.correctCount);
  show("screen-done");
  void refreshLeaderboard();
}

function resetGame() {
  clearRoundTimer();
  questionIndex = 0;
  score = 0;
  answers = [];
  setScoreDisplay();
}

async function restartSamePlayer() {
  const name = currentPlayerName();
  if (!name) {
    show("screen-start");
    alert("Önce isim girmelisin.");
    $("player-name").focus();
    return;
  }
  activePlayerName = name;
  try {
    if (!selectedQuizId) {
      alert("Önce bir kategori seçmelisin.");
      show("screen-categories");
      return;
    }
    quiz = await api(`/api/quizzes/${selectedQuizId}`);
  } catch (e) {
    alert("Quiz yüklenemedi. (" + (e && e.message) + ")");
    return;
  }
  resetGame();
  $("playing-name").textContent = name;
  $("playing-category").textContent = quiz.title;
  show("screen-play");
  renderQuestion();
  startRoundTimer();
  void refreshLeaderboard();
}

function goToChangeName() {
  clearRoundTimer();
  selectedQuizId = null;
  selectedCategoryTitle = "";
  quiz = null;
  updateSidebarTitle();
  show("screen-start");
  const input = $("player-name");
  input.focus();
  input.select();
  void refreshLeaderboard();
}

$("btn-start").addEventListener("click", () => void continueToCategories());
$("btn-back-to-name").addEventListener("click", () => {
  show("screen-start");
  $("player-name").focus();
});
$("btn-retry").addEventListener("click", () => void restartSamePlayer());
$("btn-again").addEventListener("click", () => void restartSamePlayer());
$("btn-change-name-lost").addEventListener("click", () => goToChangeName());
$("btn-change-name-done").addEventListener("click", () => goToChangeName());

updateSidebarTitle();
void refreshLeaderboard();
