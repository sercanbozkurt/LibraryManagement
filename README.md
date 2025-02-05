# LibraryManagement
 
# Kütüphane Yönetim Sistemi

ASP.NET Core MVC ile geliştirilen bu proje, kütüphane kayıtlarının dijital ortamda yönetilmesini sağlar. Öğrenci ve kitap takibi, rezervasyon işlemleri ve istatistiksel veriler sunar.


## 📋 Proje Amacı
- Kitap ve öğrenci kayıtlarını merkezi bir sistemde yönetmek.
- Rezervasyonlar aracılığıyla öğrenci-kitap ilişkisini takip etmek.
- Anlık istatistiklerle kütüphane aktivitelerini görselleştirmek.

## 🛠️ Kullanılan Teknolojiler
- **Backend:** ASP.NET Core MVC
- **Veritabanı:** SQLite (Entity Framework Core ORM)
- **Frontend:** HTML, CSS, JavaScript, Bootstrap 5, sbadmin2 Template, SweetAlert
- **Diğer Araçlar:** Razor Pages, LINQ, JavaScript

## 📂 Proje Yapısı
LibraryManagementSystem/
├── Controllers/
├── Models/
├── Views/
├── Data/
├── wwwroot/
└── appsettings.json


## 🌟 Özellikler
### Kullanıcı İşlevleri
- **Öğrenci Yönetimi**
  - Öğrenci Ekleme/Güncelleme/Silme/Listeleme
 
- **Kitap Yönetimi**
  - Kitap Ekleme/Güncelleme/Silme/Listeleme

- **Rezervasyon Sistemi**
  - Öğrenci-Kitap eşleştirme
  - Tarih bazlı kayıt takibi

### Anasayfa İstatistikleri
- 📚 Toplam/Aktif Kitap Sayıları
- 👥 Toplam/Aktif Öğrenci Sayıları
- 🏆 En Çok Kitap Okuyan Öğrenciler
- 📊 En Popüler Kitaplar

## 🚀 Kurulum
1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/sercanbozkurt/LibraryManagement.git

2. Bağımlılıkları yükleyin:
dotnet restore
3. Veritabanını oluşturun:
dotnet ef database update
4. Uygulamayı çalıştırın:
dotnet run


NOT: Ekran görüntüleri "wwwroot/img/screenshots" klasöründedir.

