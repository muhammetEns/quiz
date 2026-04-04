const $ = (id) => document.getElementById(id);

let quizzes = [];
let questions = [];
let editingId = null;

async function api(path, options = {}) {
  const res = await fetch(path, {
    headers: { "Content-Type": "application/json", Accept: "application/json" },
    ...options,
  });
  const ct = res.headers.get("content-type");
  const json = ct && ct.includes("application/json") ? await res.json() : null;
  if (!res.ok) {
    const msg = json?.message || JSON.stringify(json) || res.statusText;
    throw new Error(msg);
  }
  if (res.status === 204) return null;
  return json;
}

function selectedQuizId() {
  const v = $("quiz-select").value;
  return v ? Number(v) : null;
}

function setMsg(text, kind) {
  const el = $("form-msg");
  el.textContent = text || "";
  el.className = "msg" + (kind ? ` ${kind}` : "");
}

async function loadQuizzes() {
  quizzes = await api("/api/admin/quizzes");
  const sel = $("quiz-select");
  const prev = sel.value;
  sel.innerHTML = "";
  for (const q of quizzes) {
    const o = document.createElement("option");
    o.value = String(q.id);
    o.textContent = `${q.title} (${q.questionCount} soru)`;
    sel.appendChild(o);
  }
  if (prev && quizzes.some((q) => String(q.id) === prev)) sel.value = prev;
  await loadQuestions();
}

async function loadQuestions() {
  const qid = selectedQuizId();
  if (!qid) {
    questions = [];
    renderQuestions();
    return;
  }
  questions = await api(`/api/admin/quizzes/${qid}/questions`);
  renderQuestions();
}

function renderQuestions() {
  const wrap = $("questions-wrap");
  if (!questions.length) {
    wrap.innerHTML = "<p class=\"msg\">Bu quiz’te henüz soru yok.</p>";
    return;
  }
  const rows = questions
    .map(
      (q) => `
    <tr>
      <td>${q.id}</td>
      <td class="q-preview">${escapeHtml(q.text)}</td>
      <td>${q.correctOptionIndex}</td>
      <td>${q.sortOrder}</td>
      <td>
        <button type="button" class="btn small secondary" data-edit="${q.id}">Düzenle</button>
        <button type="button" class="btn small danger" data-del="${q.id}">Sil</button>
      </td>
    </tr>`
    )
    .join("");
  wrap.innerHTML = `
    <table>
      <thead><tr><th>ID</th><th>Soru</th><th>Doğru</th><th>Sıra</th><th></th></tr></thead>
      <tbody>${rows}</tbody>
    </table>`;
  wrap.querySelectorAll("[data-edit]").forEach((btn) => {
    btn.addEventListener("click", () => startEdit(Number(btn.getAttribute("data-edit"))));
  });
  wrap.querySelectorAll("[data-del]").forEach((btn) => {
    btn.addEventListener("click", () => void removeQuestion(Number(btn.getAttribute("data-del"))));
  });
}

function escapeHtml(s) {
  const d = document.createElement("div");
  d.textContent = s;
  return d.innerHTML;
}

function clearForm() {
  editingId = null;
  $("form-title").textContent = "Yeni soru";
  $("q-text").value = "";
  for (let i = 0; i < 4; i++) $(`opt${i}`).value = "";
  $("correct").value = "0";
  $("sort").value = "";
  setMsg("");
}

function startEdit(id) {
  const q = questions.find((x) => x.id === id);
  if (!q) return;
  editingId = id;
  $("form-title").textContent = `Soru düzenle (#${id})`;
  $("q-text").value = q.text;
  q.options.forEach((t, i) => {
    $(`opt${i}`).value = t;
  });
  $("correct").value = String(q.correctOptionIndex);
  $("sort").value = String(q.sortOrder);
  setMsg("");
  $("q-text").focus();
}

function readPayload() {
  const text = $("q-text").value.trim();
  const options = [0, 1, 2, 3].map((i) => $(`opt${i}`).value.trim());
  const correctOptionIndex = Number($("correct").value);
  const sortRaw = $("sort").value.trim();
  const sortOrder = sortRaw === "" ? null : Number(sortRaw);
  return { text, options, correctOptionIndex, sortOrder };
}

async function saveQuestion() {
  const qid = selectedQuizId();
  if (!qid) {
    setMsg("Önce quiz seç.", "err");
    return;
  }
  const body = readPayload();
  setMsg("Kaydediliyor…");
  try {
    if (editingId == null) {
      await api(`/api/admin/quizzes/${qid}/questions`, {
        method: "POST",
        body: JSON.stringify({
          text: body.text,
          options: body.options,
          correctOptionIndex: body.correctOptionIndex,
          sortOrder: body.sortOrder,
        }),
      });
      setMsg("Soru eklendi.", "ok");
    } else {
      await api(`/api/admin/questions/${editingId}`, {
        method: "PUT",
        body: JSON.stringify({
          text: body.text,
          options: body.options,
          correctOptionIndex: body.correctOptionIndex,
          sortOrder: body.sortOrder,
        }),
      });
      setMsg("Güncellendi.", "ok");
    }
    clearForm();
    await loadQuizzes();
  } catch (e) {
    setMsg(e.message || "Hata", "err");
  }
}

async function removeQuestion(id) {
  if (!confirm(`Soru #${id} silinsin mi?`)) return;
  try {
    await api(`/api/admin/questions/${id}`, { method: "DELETE" });
    await loadQuizzes();
    if (editingId === id) clearForm();
    setMsg("Silindi.", "ok");
  } catch (e) {
    setMsg(e.message || "Silinemedi", "err");
  }
}

$("quiz-select").addEventListener("change", () => void loadQuestions());
$("btn-refresh").addEventListener("click", () => void loadQuizzes());
$("btn-save").addEventListener("click", () => void saveQuestion());
$("btn-new").addEventListener("click", () => clearForm());

void loadQuizzes().catch((e) => {
  $("questions-wrap").innerHTML = `<p class="msg err">Yüklenemedi: ${escapeHtml(e.message)}</p>`;
});
