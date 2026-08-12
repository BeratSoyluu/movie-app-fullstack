# 🎬 Movie App — Full Stack Film Değerlendirme Uygulaması

Netvar Elektronik A.Ş.'de gerçekleştirdiğim staj kapsamında geliştirdiğim, TMDB API entegrasyonlu, rol bazlı erişime sahip bir film değerlendirme uygulaması.

Kullanıcılar giriş yaptıktan sonra popüler filmleri ve oyuncuları görüntüleyebilir, filmleri arayabilir, filmlere puan ve not ekleyebilir, film önerisinde bulunabilir. Erişim kullanıcı rolüne göre sınırlandırılmıştır.

## 🛠️ Kullanılan Teknolojiler

**Backend (.NET 9)**
- ASP.NET Core Web API
- Clean Architecture (Domain, Application, Infrastructure, API katmanları)
- CQRS + MediatR
- Entity Framework Core + SQLite
- JWT tabanlı kimlik doğrulama & rol bazlı yetkilendirme
- FluentValidation (pipeline behavior ile)
- Global exception handling
- TMDB API entegrasyonu (HttpClient)

**Frontend (React)**
- React + Vite
- React Router (korumalı sayfalar)
- Axios (interceptor ile otomatik token yönetimi)
- Rol bazlı dinamik menü

## 📐 Mimari

Backend, Clean Architecture prensiplerine göre 4 katmandan oluşur:

- **Domain** — Entity'ler (Review, Suggestion, User), hiçbir katmana bağımlı değil
- **Application** — İş mantığı (CQRS komut/sorguları, handler'lar, interface'ler, DTO'lar, validation)
- **Infrastructure** — Dış dünya (EF Core repository'leri, JWT üretimi, TMDB servisi)
- **API** — Controller'lar, middleware, dependency injection

Bağımlılıklar her zaman içe doğrudur; iş mantığı dış detaylardan (veritabanı, web) bağımsızdır.

## 👥 Kullanıcılar ve Roller

Uygulamada 3 sabit kullanıcı bulunur (şifreler `1234`):

| Kullanıcı | Rol | Yetki |
|-----------|-----|-------|
| user1 | Movie | Sadece film listeleme, arama, puan/not, öneri |
| user2 | Actor | Sadece oyuncu listeleme ve arama |
| user3 | Admin | Tüm özellikler (film + oyuncu) |

Menü ve erişim kullanıcının rolüne göre değişir. Yetkisiz erişim hem frontend (yönlendirme) hem backend (`[Authorize(Roles=...)]`) tarafından engellenir.

## ✨ Özellikler

- 🔐 JWT ile giriş, korumalı sayfalar
- 🎞️ Popüler filmleri listeleme (TMDB'den, max 100 film)
- 🔍 Film ve oyuncu arama
- ⭐ Filme puan (1-10) ve not ekleme, yıldızlı gösterim
- 📊 Film detayı: TMDB puanı, ortalama kullanıcı puanı, kullanıcının puanı ve notlar
- 👤 Popüler oyuncuları listeleme ve arama
- 💡 Film önerme ve önerilen filmleri listeleme

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- .NET 9 SDK
- Node.js (v18+)
- TMDB API anahtarı ([themoviedb.org](https://www.themoviedb.org/) üzerinden ücretsiz)

### Backend

```bash
cd backend
```

Güvenlik nedeniyle API anahtarı ve JWT anahtarı repository'ye dahil edilmemiştir. Bunlar .NET User Secrets ile yönetilir. Çalıştırmadan önce kendi değerlerinizi girin:

```bash
dotnet user-secrets set "Tmdb:ApiKey" "TMDB_API_ANAHTARINIZ" --project src/StajProje.API
dotnet user-secrets set "Jwt:Key" "EN_AZ_32_KARAKTERLIK_GIZLI_ANAHTAR" --project src/StajProje.API
```

Ardından uygulamayı başlatın:

```bash
dotnet run --project src/StajProje.API
```

Backend `http://localhost:5206` adresinde çalışır. Swagger arayüzü: `http://localhost:5206/swagger`

### Frontend

```bash
cd frontend
npm install
npm run dev
```

Frontend `http://localhost:5173` adresinde çalışır.

## 🔒 Güvenlik Notları

- Gizli anahtarlar (TMDB API key, JWT key) User Secrets ile yönetilir, repository'ye dahil edilmez.
- Kullanıcı kimliği (UserId) istemciden alınmaz; JWT token'dan çıkarılarak sunucu tarafında belirlenir.
- Tüm veri erişimi backend'de rol bazlı olarak korunur.

## 📝 Notlar

Bu proje eğitim/staj amaçlı geliştirilmiştir. Şifreler basitlik için düz metin olarak saklanmaktadır; üretim ortamında hash'lenmelidir.