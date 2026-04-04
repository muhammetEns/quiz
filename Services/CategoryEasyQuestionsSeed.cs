using quizApi.Models;

namespace quizApi.Services;

/// <summary>Kolay seviye kategori soruları (Film & Dizi, Spor, Coğrafya, Genel Kültür).</summary>
internal static class CategoryEasyQuestionsSeed
{
    internal static IReadOnlyList<Question> FilmDizi =>
    [
        new Question
        {
            Id = 1,
            Text = "Sinema salonunda genelde ne izlenir?",
            Options = ["Film", "Yemek programı", "Haber bülteni", "Maç tekrarı"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 2,
            Text = "Televizyonda bölümler hâlinde anlatılan uzun hikâyelere ne denir?",
            Options = ["Dizi", "Reklam", "Klip", "Maraton"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 3,
            Text = "Filmi yöneten kişiye ne ad verilir?",
            Options = ["Yönetmen", "Seslendiren", "Işıkçı", "Kameraman"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 4,
            Text = "Çizgi filmde karakterler genelde nasıl canlandırılır?",
            Options = ["Animasyon ile", "Sadece tiyatro ile", "Radyo ile", "Gazete ile"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 5,
            Text = "Filmin kısa tanıtım videosuna ne denir?",
            Options = ["Fragman", "Jenerik", "Fatura", "Künye"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 6,
            Text = "Konuşmaları ekranda yazı olarak göstermeye ne denir?",
            Options = ["Altyazı", "Jenerik", "Dublaj (seslendirme)", "Künye"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 7,
            Text = "Oscar ödülleri hangi alanla en çok ilişkilendirilir?",
            Options = ["Sinema", "Futbol", "Mutfak", "Astronomi"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 8,
            Text = "Dizide rol alan kişiye genelde ne denir?",
            Options = ["Oyuncu (aktör)", "Hakem", "Pilot", "Berber"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 9,
            Text = "Netflix gibi platformlar ne sunar?",
            Options = ["İnternet üzerinden film ve dizi izleme", "Sadece gazete", "Sadece radyo", "Banka hizmeti"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 10,
            Text = "Film müziği izleyicide genelde neyi güçlendirir?",
            Options = ["Duygu ve atmosferi", "Sadece altyazıyı", "Sadece reklamı", "Sadece fiyatı"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 11,
            Text = "Türk yapımı bir filme ne denir?",
            Options = ["Yerli film", "Yabancı dizi", "Belgesel (her zaman)", "Sadece kısa haber"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 12,
            Text = "Sinema perdesi genelde hangi renktedir?",
            Options = ["Beyaz veya açık renk", "Sadece siyah", "Sadece kırmızı", "Sadece yeşil"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 13,
            Text = "Kısa film uzun metraj filme göre nasıldır?",
            Options = ["Genelde daha kısadır", "Her zaman daha uzundur", "Süre fark etmez", "Sadece radyoda oynar"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 14,
            Text = "Televizyon kumandası ne için kullanılır?",
            Options = ["Kanal ve ses gibi ayarları değiştirmek", "Sadece yemek pişirmek", "Sadece dikiş", "Sadece yüzme"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 15,
            Text = "Haberleri sunan kişiye televizyonda genelde ne denir?",
            Options = ["Sunucu / spiker", "Kaleci", "Hakem", "Berber"],
            CorrectOptionIndex = 0
        }
    ];

    internal static IReadOnlyList<Question> Spor =>
    [
        new Question
        {
            Id = 1,
            Text = "Futbol topu hangi şekle yakındır?",
            Options = ["Küre (yuvarlak)", "Küp", "Üçgen", "Düz levha"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 2,
            Text = "Standart bir futbol takımında sahada aynı anda kaç oyuncu oynar?",
            Options = ["11", "5", "7", "22"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 3,
            Text = "Yüzme genelde nerede yapılır?",
            Options = ["Su içinde (havuz, deniz vb.)", "Karda", "Kumda", "Havada"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 4,
            Text = "Olimpiyat halkaları kaç tanedir?",
            Options = ["5", "3", "4", "10"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 5,
            Text = "Futbolda ciddi faullerde hakem hangi renk kartla oyuncuyu oyundan atabilir?",
            Options = ["Kırmızı", "Yeşil", "Mor", "Turuncu"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 6,
            Text = "Maraton koşusu yaklaşık kaç kilometredir?",
            Options = ["42", "5", "100", "1"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 7,
            Text = "Basketbolda topu potadan geçirmeye ne denir?",
            Options = ["Sayı / basket", "Gol", "Asist (tek başına)", "Servis"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 8,
            Text = "Bisiklet sürerken güvenlik için en çok önerilen baş ekipman nedir?",
            Options = ["Kask", "Şapka (her türlü)", "Eldiven (zorunlu)", "Gözlük"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 9,
            Text = "Futbol maçı normal sürede kaç yarıdan oluşur?",
            Options = ["İki yarı", "Bir yarı", "Dört yarı", "On yarı"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 10,
            Text = "Voleybolda iki takımı ayıran yüksek şeye ne denir?",
            Options = ["File", "Kale direği", "Pot", "Çizgi"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 11,
            Text = "Koşu ve yürüyüş hangi sistemini çalıştırmaya yardımcı olur?",
            Options = ["Kalp ve solunum", "Sadece saç", "Sadece tırnak", "Sadece gözlük"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 12,
            Text = "Halterde sporcular ne kaldırır?",
            Options = ["Ağırlık (halter)", "Tahta parçası", "Su şişesi (spor aleti sayılır)", "Kitap"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 13,
            Text = "Yüzücüler genelde hangi kıyafeti giyer?",
            Options = ["Mayo", "Palto", "Smokin", "Çizme"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 14,
            Text = "Teniste skorda sık duyulan “15, 30, 40” hangi sporla ilgilidir?",
            Options = ["Tenis", "Futbol", "Satranç", "Yüzme"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 15,
            Text = "“Fair play” spor dilinde ne anlama gelir?",
            Options = ["Dürüst ve kurallara uygun oynamak", "Sadece yenilmemek", "Sadece seyirci olmak", "Sahayı terk etmemek"],
            CorrectOptionIndex = 0
        }
    ];

    internal static IReadOnlyList<Question> Cografya =>
    [
        new Question
        {
            Id = 1,
            Text = "Türkiye'nin başkenti neresidir?",
            Options = ["Ankara", "İstanbul", "İzmir", "Bursa"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 2,
            Text = "Dünya yüzeyinin büyük kısmını ne kaplar?",
            Options = ["Su (okyanuslar, denizler)", "Sadece çöl", "Sadece buz", "Sadece orman"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 3,
            Text = "Türkiye toprakları hangi iki kıtada yer alır?",
            Options = ["Avrupa ve Asya", "Sadece Avrupa", "Sadece Afrika", "Avustralya ve Asya"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 4,
            Text = "Türkiye'nin güneyinde hangi deniz yer alır?",
            Options = ["Akdeniz", "Baltık Denizi", "Kızıldeniz (Mısır'a komşu)", "Hazar Denizi"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 5,
            Text = "Aşağıdakilerden hangisi Karadeniz kıyısındaki illerden biridir?",
            Options = ["Trabzon", "Konya", "Kayseri", "Muğla"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 6,
            Text = "Haritada deniz ve göller genelde hangi renkle gösterilir?",
            Options = ["Mavi", "Kahverengi", "Siyah", "Pembe"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 7,
            Text = "Kutuplara yakın bölgelerin iklimi genelde nasıldır?",
            Options = ["Soğuk", "Çok sıcak ve kurak", "Her zaman tropik", "Sadece ılıman"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 8,
            Text = "Komşu ülke ne demektir?",
            Options = ["Sınırı paylaşan ülke", "Uzak ada ülkesi", "Sadece aynı dil", "Sadece aynı para birimi"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 9,
            Text = "Ekvator çizgisi Dünya'nın hangi bölgesindedir?",
            Options = ["Orta (ekvatoral) bölgeye yakın", "Sadece kuzey kutbunda", "Sadece güney kutbunda", "Ay'da"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 10,
            Text = "Çöl iklimi genelde nasıl tanımlanır?",
            Options = ["Kurak ve az yağışlı", "Çok yağışlı", "Sürekli karlı", "Sadece göllü"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 11,
            Text = "Türkiye'de kışın kar görme ihtimali dağlık bölgelere göre genelde nasıldır?",
            Options = ["Dağlık yerlerde daha olasıdır", "Hiç kar yağmaz", "Sadece sahillerde", "Sadece yazın"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 12,
            Text = "Volkan patlamaları hangi yapı ile ilişkilidir?",
            Options = ["Yer kabuğu hareketleri", "Sadece rüzgâr", "Sadece güneşlenme", "Sadece trafik"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 13,
            Text = "Kuzey yarımkürede yaz ayları genelde hangileridir?",
            Options = ["Haziran, Temmuz, Ağustos", "Aralık, Ocak, Şubat", "Mart, Nisan (kış)", "Sadece Ekim"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 14,
            Text = "Dünya'nın uydusu hangisidir?",
            Options = ["Ay", "Mars", "Güneş", "Venüs"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 15,
            Text = "İç Anadolu Bölgesi Türkiye'nin yaklaşık neresindedir?",
            Options = ["Orta kısımlarda", "Sadece en kuzey uç", "Sadece en güney uç", "Sadece batı deniz dışı"],
            CorrectOptionIndex = 0
        }
    ];

    internal static IReadOnlyList<Question> GenelKultur =>
    [
        new Question
        {
            Id = 1,
            Text = "Bir haftada kaç gün vardır?",
            Options = ["7", "5", "10", "12"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 2,
            Text = "Bir düzine kaç tanedir?",
            Options = ["12", "10", "6", "20"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 3,
            Text = "Bir günde kaç saat vardır?",
            Options = ["24", "12", "48", "60"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 4,
            Text = "Gökkuşağında genelde kaç ana renk sayılır?",
            Options = ["7", "3", "2", "12"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 5,
            Text = "Saf su deniz seviyesinde yaklaşık kaç °C'de donmaya başlar?",
            Options = ["0", "100", "50", "-50"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 6,
            Text = "Trafikte “DUR” yazısı genelde hangi renkte olur?",
            Options = ["Beyaz üzerine kırmızı levha veya kırmızı ışık", "Sadece yeşil", "Sadece mavi", "Sadece turuncu"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 7,
            Text = "Okuma yazma öğrenmek için en yaygın kurum hangisidir?",
            Options = ["Okul", "Fırın", "Otogar", "Sinema"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 8,
            Text = "Sağlıklı beslenmede meyve ve sebze tüketimi önemli midir?",
            Options = ["Evet, önemlidir", "Hayır, hiç gerekmez", "Sadece tatlı yerine", "Sadece gece"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 9,
            Text = "Güneş sisteminde gezegenler çoğunlukla hangi etrafında döner?",
            Options = ["Güneş", "Ay", "Dünya uydusu", "Plüton (cüce gezegen)"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 10,
            Text = "Kitap ödünç alıp okunabilecek kamusal yer nedir?",
            Options = ["Kütüphane", "Stadyum", "Hastane acil", "Benzin istasyonu"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 11,
            Text = "2 + 2 işleminin sonucu kaçtır?",
            Options = ["4", "3", "5", "22"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 12,
            Text = "Türk bayrağında hangi renkler bulunur?",
            Options = ["Kırmızı ve beyaz", "Sadece mavi", "Sadece yeşil", "Sadece sarı"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 13,
            Text = "Elektrik prizlerine özellikle çocuklar ...",
            Options = ["parmak sokmamalıdır", "serbestçe oynamalıdır", "su dökmelidir", "metal sokmalıdır"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 14,
            Text = "Takvimde bir yılda kaç ay vardır?",
            Options = ["12", "10", "7", "24"],
            CorrectOptionIndex = 0
        },
        new Question
        {
            Id = 15,
            Text = "Gece gökyüzünde çoğunlukla parlayan küçük noktalar genelde nedir?",
            Options = ["Yıldızlar (uzaktaki güneşler)", "Sadece uçak her zaman", "Sadece bulut", "Sadece balon"],
            CorrectOptionIndex = 0
        }
    ];

    internal static bool IsPlaceholderQuestion(string text) =>
        text.Contains("yakında eklenecek", StringComparison.OrdinalIgnoreCase);
}
