[README.md](https://github.com/user-attachments/files/33178295/README.md)
# InsureYouAI

Sigorta sektörü için geliştirilmiş, yapay zeka destekli bir ASP.NET Core MVC web uygulaması. Projede hem ziyaretçilere açık bir tanıtım sitesi hem de poliçe, kullanıcı, içerik ve mesaj yönetimi yapılan bir yönetim paneli bulunur. Metin üretimi, poliçe analizi, mesaj sınıflandırma, satış tahmini ve sesli yanıt gibi alanlarda farklı yapay zeka servisleri kullanılır.

## Özellikler

**Tanıtım sitesi**
- Ana sayfa, hakkımızda, hizmetler, fiyat planları, referanslar ve iletişim bölümleri
- Kategori ve etiket destekli blog, yorum sistemi
- Tanıtım videosu ve slider yönetimi

**Yönetim paneli**
- Dashboard: gelir/gider grafikleri, poliçe türü dağılımı, radyal ve alt grafikler
- Poliçe satış tahmini (ML.NET zaman serisi)
- Makale, kategori, hizmet, hakkında, fiyat planı, referans ve slider yönetimi
- Kullanıcı listesi, yorum ve mesaj yönetimi
- SignalR ile gerçek zamanlı sohbet

**Yapay zeka entegrasyonları**
- **OpenAI:** sohbet, makale/blog içeriği, görsel üretimi ve kullanıcı açıklaması üretimi
- **Claude:** PDF poliçe analizi, hizmet ve referans içeriği üretimi
- **Gemini:** gelen mesajları sigorta kategorilerine ayırma, hakkında içeriği üretimi
- **ElevenLabs:** metinden sese çeviri ile sesli sigorta asistanı
- **Tavily:** web araması ile desteklenen yanıtlar

## Kullanılan teknolojiler

- .NET 9, ASP.NET Core MVC, SignalR
- Entity Framework Core 9 ve SQL Server
- ASP.NET Core Identity
- ML.NET (`Microsoft.ML.TimeSeries`)
- PdfPig (PDF metin okuma), MailKit (e-posta)

## Kurulum

### Gereksinimler
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB veya yerel bir SQL Server kurulumu)
- `dotnet-ef` aracı: `dotnet tool install --global dotnet-ef`

### Adımlar

1. Projeyi klonla:
   ```bash
   git clone https://github.com/halilkakut/InsureYouAI.git
   cd InsureYouAI/InsureYouAINew
   ```

2. Veritabanı bağlantısını kontrol et. Bağlantı dizesi `Context/InsureContext.cs` içinde tanımlıdır ve varsayılan olarak yerel SQL Server üzerindeki `InsureDb` veritabanını kullanır. Farklı bir sunucu kullanacaksan bu dosyayı düzenle.

3. Veritabanını oluştur:
   ```bash
   dotnet ef database update
   ```

4. API anahtarlarını User Secrets'a ekle (aşağıdaki bölüme bak).

5. Uygulamayı çalıştır:
   ```bash
   dotnet run
   ```

## API anahtarları

Anahtarlar kaynak koda yazılmaz, [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) ile yerelde saklanır. `InsureYouAINew` klasöründe şu komutları çalıştır:

```bash
dotnet user-secrets set "OpenAI:ApiKey" "<openai-anahtarin>"
dotnet user-secrets set "ElevenLabs:ApiKey" "<elevenlabs-anahtarin>"
dotnet user-secrets set "Gemini:ApiKey" "<gemini-anahtarin>"
```

| Ayar | Kullanıldığı yer |
|---|---|
| `OpenAI:ApiKey` | SignalR sohbet (`ChatHub`) |
| `ElevenLabs:ApiKey` | Sesli asistan (`ElevenLabsAIController`) |
| `Gemini:ApiKey` | Mesaj sınıflandırma (`AIService`) |

Diğer bazı controller'larda (Claude, OpenAI, Tavily çağrıları) `apiKey` değişkenleri bilerek boş bırakılmıştır. Bu özellikleri denemek için kendi anahtarını yukarıdaki yöntemle `IConfiguration` üzerinden okuyacak şekilde bağlayabilirsin.

> **Uyarı:** Gerçek anahtarları hiçbir zaman commit'leme. Bir anahtar yanlışlıkla repoya girerse onu silmek yetmez, ilgili servisin panelinden iptal edip yenisini oluştur.

## Proje yapısı

```
InsureYouAINew/
├── Context/           EF Core DbContext
├── Controllers/       MVC controller'ları (blog, poliçe, yapay zeka vb.)
├── Dtos/              Veri aktarım nesneleri
├── Entities/          Veritabanı varlıkları
├── Migrations/        EF Core migration dosyaları
├── Models/            ViewModel'ler ve SignalR ChatHub
├── Requirements/      Geliştirme sırasında kullanılan prompt ve kod notları
├── Services/          AIService, ForecastService
├── ViewComponents/    Dashboard, blog ve admin layout bileşenleri
├── Views/             Razor görünümleri
└── wwwroot/           Statik dosyalar (CSS, JS, görseller, şablonlar)
```

## Notlar

- Sesli asistanın ürettiği mp3 dosyaları `wwwroot/voices/` klasörüne yazılır. Bu klasör `.gitignore` içindedir.
- `Requirements/` klasöründeki `.txt` dosyaları uygulama kodu değildir, geliştirme sürecinde kullanılan örnek kod ve prompt notlarıdır.
