using quizApi.Models;

namespace quizApi.Services;

/// <summary>Her kategoriye eklenen 10’ar kolay soru (havuz büyütme).</summary>
internal static class PoolExtraSeed
{
    internal static IReadOnlyList<Question> FilmDizi =>
    [
        new Question
        {
            Id = 101,
            Text = "Film sonunda sık görülen isim kaydı akışına ne denir?",
            Options = ["Jenerik", "Fragman", "Altyazı", "Dublaj"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 102,
            Text = "Sinema bileti genelde ne için kullanılır?",
            Options = ["Salona giriş hakkı vermek", "Sadece hatıra", "Sadece yemek", "Sadece otopark"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 103,
            Text = "Gerçek olayları veya doğayı anlatan filmlere ne denir?",
            Options = ["Belgesel", "Korku", "Animasyon (her zaman kurgu)", "Western"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 104,
            Text = "Komedi filmleri genelde neyi hedefler?",
            Options = ["Güldürmek", "Sadece korkutmak", "Sadece uyutmak", "Sadece spor anlatmak"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 105,
            Text = "3 boyutlu (3D) gözlük sinemada ne sağlar?",
            Options = ["Derinlik hissi veren izleme", "Sadece ses", "Sadece altyazı", "Işığı kapatma"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 106,
            Text = "Dizi bölümleri arasına konan kısa reklam arasına ne denir?",
            Options = ["Reklam arası / jingle arası", "Jenerik", "Künye", "Fragman"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 107,
            Text = "Film setinde sahneyi çeken ana kişi genelde kimdir?",
            Options = ["Kameraman / görüntü yönetmeni", "Sadece oyuncu", "Sadece seyirci", "Sadece gişe görevlisi"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 108,
            Text = "Çocuklara yönelik çizgi veya animasyon içerik sık nerede bulunur?",
            Options = ["Televizyon ve dijital platformlarda", "Sadece bankada", "Sadece hastanede", "Sadece noterde"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 109,
            Text = "Film müziği genelde kim besteler?",
            Options = ["Besteci / müzik yapımcısı", "Sadece seyirci", "Sadece gişe", "Sadece garson"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 110,
            Text = "Sinema salonunda genelde sessiz olunması neden istenir?",
            Options = ["Herkesin filmi rahat izlemesi için", "Sadece yasak olduğu için keyifsizlik", "Sadece uyku için", "Sadece yemek için"],
            CorrectOptionIndex = 0
        }
    ];

    internal static IReadOnlyList<Question> Spor =>
    [
        new Question
        {
            Id = 101,
            Text = "Gol kelimesi en çok hangi sporla ilişkilendirilir?",
            Options = ["Futbol", "Yüzme", "Satranç", "Dart"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 102,
            Text = "Stadyum genelde ne tür etkinlikler içindir?",
            Options = ["Seyircili spor müsabakaları", "Sadece sinema", "Sadece düğün", "Sadece kütüphane"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 103,
            Text = "Antrenman ne anlama gelir?",
            Options = ["Düzenli çalışma / idman", "Sadece uyku", "Sadece yemek", "Sadece sınav"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 104,
            Text = "Maç öncesi ısınma hareketleri neden yapılır?",
            Options = ["Vücudu hazırlamak ve sakatlık riskini azaltmak", "Sadece forma göstermek", "Sadece zorunluluk yok", "Sadece seyirci için dans"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 105,
            Text = "Yedek oyuncu ne zaman oyuna girebilir?",
            Options = ["Takımın ihtiyaç duyduğu anlarda (kurallara göre)", "Hiç giremez", "Sadece devre arası dışında asla", "Sadece hakem olunca"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 106,
            Text = "Penaltı atışı en çok hangi sporla bilinir?",
            Options = ["Futbol", "Satranç", "Yüzme", "Golf"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 107,
            Text = "Buz pateni ve buz hokeyi hangi yüzeyde yapılır?",
            Options = ["Buz", "Kum", "Çim (her zaman)", "Su"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 108,
            Text = "Atletizmde aşağıdakilerden hangisi yaygın bir branştır?",
            Options = ["Koşu", "Satranç", "Bilardo", "Dart"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 109,
            Text = "Spor yaparken bol su içmek özellikle sıcakta neden önemlidir?",
            Options = ["Susuzluğu önlemek", "Sadece tat için", "Sadece kilo almak için", "Sadece soğuk algınlığı"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 110,
            Text = "Takım sporunda “takım ruhu” neyi ifade eder?",
            Options = ["Birlikte oynama ve destekleme", "Sadece bireysel şov", "Sadece tartışma", "Sadece seyirci azlığı"],
            CorrectOptionIndex = 0
        }
    ];

    internal static IReadOnlyList<Question> Cografya =>
    [
        new Question
        {
            Id = 101,
            Text = "Marmara Denizi Türkiye'nin hangi bölgesinde yer alır?",
            Options = ["Marmara Bölgesi çevresinde", "Sadece Güneydoğu'da", "Sadece İç Anadolu ortasında", "Sadece Afrika'da"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 102,
            Text = "Ege Denizi kıyısında aşağıdakilerden hangisi daha uygundur?",
            Options = ["İzmir", "Erzurum", "Van", "Sivas"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 103,
            Text = "Van Gölü hangi ilimizdedir?",
            Options = ["Van", "Ankara", "Antalya", "Rize"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 104,
            Text = "Pamukkale travertenleri hangi ilimizle özdeşleşmiştir?",
            Options = ["Denizli", "Edirne", "Artvin", "Şanlıurfa"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 105,
            Text = "Kapadokya bölgesi genelde hangi iç Anadolu illeriyle anılır?",
            Options = ["Nevşehir ve çevresi", "Sadece İstanbul", "Sadece Hatay", "Sadece Zonguldak"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 106,
            Text = "Türkiye genelinde yaygın kullanılan saat dilimi UTC ile ifade edildiğinde çoğunlukla hangisidir?",
            Options = ["UTC+3 civarı", "UTC+0", "UTC-8", "UTC+12"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 107,
            Text = "Çanakkale Boğazı hangi iki denizi birbirine bağlar?",
            Options = ["Ege Denizi ve Marmara Denizi", "Karadeniz ve Hazar", "Akdeniz ve Kızıldeniz", "Baltık ve Kuzey Denizi"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 108,
            Text = "Göller Yöresi hangi bölgede yer alır?",
            Options = ["İç Anadolu ve Akdeniz'e yakın göller yöresi", "Sadece Karadeniz", "Sadece Güneydoğu", "Sadece İstanbul"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 109,
            Text = "Haritada yüksek yerler genelde hangi renkle gösterilir?",
            Options = ["Kahverengi veya tonları", "Her zaman mavi", "Her zaman pembe", "Her zaman beyaz"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 110,
            Text = "Ormanlık alanlar ekosistem için genelde ne sağlar?",
            Options = ["Oksijen ve yaşam alanı", "Sadece tuz", "Sadece petrol", "Sadece plastik"],
            CorrectOptionIndex = 0
        }
    ];

    internal static IReadOnlyList<Question> GenelKultur =>
    [
        new Question
        {
            Id = 101,
            Text = "Bir saatte kaç dakika vardır?",
            Options = ["60", "30", "100", "24"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 102,
            Text = "Bir yılda genelde kaç mevsim sayılır?",
            Options = ["4", "2", "6", "12"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 103,
            Text = "Gece ve gündüz oluşumu Dünya'nın ne hareketine bağlıdır?",
            Options = ["Kendi ekseni etrafında dönmesi", "Sadece Ay'ın dönmesi", "Sadece Güneş'in sabit kalması", "Sadece yağmur"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 104,
            Text = "Telefonun temel kullanım amaçlarından biri nedir?",
            Options = ["Sesli veya yazılı iletişim kurmak", "Sadece çekiç gibi kullanmak", "Sadece buzdolabı", "Sadece çamaşır"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 105,
            Text = "Çöpleri sokak yerine çöp kutusuna atmak neden doğrudur?",
            Options = ["Çevre ve hijyen için", "Sadece ceza yememek", "Sadece moda", "Hiçbir fark yoktur"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 106,
            Text = "Düzenli diş fırçalamak neye yardımcı olur?",
            Options = ["Ağız sağlığına", "Sadece saç uzamasına", "Sadece boy uzamasına", "Sadece gözlük numarasına"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 107,
            Text = "Yeterli uyku çocuklar için neden önemlidir?",
            Options = ["Büyüme ve dinlenme için", "Sadece gece yemek için", "Sadece televizyon için", "Hiç önemli değildir"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 108,
            Text = "Elleri yemekten önce yıkamak neyi azaltmaya yardımcı olur?",
            Options = ["Mikrop bulaşmasını", "Sadece su israfını artırmayı", "Sadece zaman kaybını", "Hiçbir şeyi"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 109,
            Text = "Trafikte yaya geçidinde yayalara öncelik vermek neden önemlidir?",
            Options = ["Güvenlik ve kural gereği", "Sadece tavsiye", "Sadece pazar günü", "Hiç gerekmez"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 110,
            Text = "Kitap okumak genelde neyi geliştirir?",
            Options = ["Okuma ve hayal gücünü", "Sadece telefon şarjını", "Sadece mutfak", "Sadece araba kullanmayı"],
            CorrectOptionIndex = 0
        }
    ];

    internal static IReadOnlyList<Question> Bilmece =>
    [
        new Question
        {
            Id = 101,
            Text = "Gündüz gökyüzünde en çok gördüğümüz parlak küreye ne denir?",
            Options = ["Güneş", "Yıldız", "Mars", "Uydu"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 102,
            Text = "İçi boş, üfleyince uçar; çocuklar sever. Nedir?",
            Options = ["Balon", "Kaya", "Sandalye", "Kitap"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 103,
            Text = "Ağzı var konuşmaz, su taşır. Nedir?",
            Options = ["Testi / sürahi", "Kedi", "Radyo", "Saat"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 104,
            Text = "Dört ayağı vardır, yemek yer, miyav der. Nedir?",
            Options = ["Kedi", "Masa", "Araba", "Ağaç"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 105,
            Text = "Kışın elleri sıcak tutar, parmaksızdır. Nedir?",
            Options = ["Eldiven", "Terlik", "Şapka", "Kemer"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 106,
            Text = "Yerden yüksek, üstüne otururuz; bacakları vardır. Nedir?",
            Options = ["Sandalye", "Göl", "Rüzgâr", "Bulut"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 107,
            Text = "Sayıları gösterir, zamanı söyler; kolu döner. Nedir?",
            Options = ["Saat", "Tabak", "Çorap", "Kalem"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 108,
            Text = "Tatlıdır, sarıdır, maymunların sevdiği meyve. Nedir?",
            Options = ["Muz", "Havuç", "Soğan", "Limon"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 109,
            Text = "Gece parlar, gökyüzünde, Ay'dan küçük görünen çoğu şey. Nedir?",
            Options = ["Yıldız", "Güneş", "Bulut", "Uçak her zaman"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 110,
            Text = "Suyun üstünde yüzer, tahtadan yapılır, kürek çekilir. Nedir?",
            Options = ["Sandal / kayık", "Otobüs", "Bisiklet", "Kalem"],
            CorrectOptionIndex = 0
        }
    ];
}
