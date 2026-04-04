using quizApi.Models;

namespace quizApi.Services;

internal static class BilmeceQuestionsSeed
{
    internal static IReadOnlyList<Question> All =>
    [
        new Question
        {
            Id = 1,
            Text = "Gökten düşer, yere konar; beyaz örtü gibi her yeri kaplar. Nedir?",
            Options = ["Kar", "Yağmur", "Dolu", "Sis"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 2,
            Text = "Konuşur ama ağzı yoktur; sesini duyarsın, tutamazsın. Nedir?",
            Options = ["Radyo", "Rüzgâr", "Yankı", "Telefon"],
            CorrectOptionIndex = 2
        },
        new Question
        {
            Id = 3,
            Text = "Çözülünce herkes rahatlar; kendi kendine bir oyun gibidir. Nedir?",
            Options = ["Bilmece", "Şifre", "Düğüm", "Bulmaca kutusu"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 4,
            Text = "Uçar mı uçar kanadı yok, yer mi yer ayağı yok. Nedir?",
            Options = ["Bulut", "Balon", "Uçak", "Kuş"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 5,
            Text = "Gece çıkar, gündüz saklanır; gökyüzünü süsler, eline alamazsın. Nedir?",
            Options = ["Yıldız", "Güneş", "Ay", "Uçak"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 6,
            Text = "İçi boş, sesi yoktur; sallarsan çınlar, çalar. Nedir?",
            Options = ["Zil", "Çan", "Kutu", "Top"],
            CorrectOptionIndex = 1
        },
        new Question
        {
            Id = 7,
            Text = "Ne kadar çok alırsan o kadar hafif kalırsın; görünmez, yaşamın için gerekir. Nedir?",
            Options = ["Su", "Hava", "Toprak", "Kum"],
            CorrectOptionIndex = 1
        },
        new Question
        {
            Id = 8,
            Text = "Suyun üstünde yüzer, suyun içinde erir; tuzlu değildir. Nedir?",
            Options = ["Buz", "Köpük", "Yağ", "Tahta"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 9,
            Text = "Küçük kutudur odanda; dünyadan haberler, görüntüler getirir. Nedir?",
            Options = ["Televizyon", "Buzdolabı", "Çamaşır makinesi", "Fırın"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 10,
            Text = "Başı yok kuyruğu kalır; rüzgârla yükselir, çocuklar sever. Nedir?",
            Options = ["Kelebek", "Uçurtma", "Balon", "Kuş"],
            CorrectOptionIndex = 1
        },
        new Question
        {
            Id = 11,
            Text = "İki kardeş yan yanadır; açılınca konuşurlar, kapanınca susarlar. Nedir?",
            Options = ["Kitap", "Kapı", "Göz", "Kulak"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 12,
            Text = "Dalından kopar, yere düşer; sararınca çürür, tatlıdır. Nedir?",
            Options = ["Elma", "Yaprak", "Çam kozalağı", "Sopa"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 13,
            Text = "Taşırsan taşır, konuşmaz; düşürürsen çatlar, sarı içi çıkar. Nedir?",
            Options = ["Su testisi", "Cam şişe", "Ekmek", "Yumurta"],
            CorrectOptionIndex = 3
        },
        new Question
        {
            Id = 14,
            Text = "İçinde altın yok, üstünde gümüş yok; vurunca ses verir, müzik yapar. Nedir?",
            Options = ["Zil", "Davul", "Tef", "Keman"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 15,
            Text = "Küçücük ev, içinde yüzlerce aile barınır; bal yaparlar. Nedir?",
            Options = ["Arı kovanı", "Kuş yuvası", "Karınca yuvası", "Balık sürüsü"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 16,
            Text = "Lambada durur, ışık saçar; su içmez, ateşte erimez (camı). Nedir?",
            Options = ["Mum", "Ampul", "Ayna", "Fitil"],
            CorrectOptionIndex = 1
        },
        new Question
        {
            Id = 17,
            Text = "Tek sapı vardır, keskin yüzü; odun keser, ormanda iş görür. Nedir?",
            Options = ["Testere", "Balta", "Keser", "Çekiç"],
            CorrectOptionIndex = 1
        },
        new Question
        {
            Id = 18,
            Text = "Güneşi görmez, rüzgârı hisseder; yazın yeşil, sonbaharda sararır. Nedir?",
            Options = ["Ağaç", "Çimen", "Çiçek", "Yol"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 19,
            Text = "Adım adım çıkar, uçamaz; hep aynı yolu kullanır, eve çıkarır. Nedir?",
            Options = ["Merdiven", "Asansör", "Yürüyen merdiven", "Köprü"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 20,
            Text = "Tohumdan doğar, güneşe döner; çekirdeği siyah, çiçeği sarıdır. Nedir?",
            Options = ["Ayçiçeği", "Papatya", "Gül", "Menekşe"],
            CorrectOptionIndex = 0
        }
    ];
}
