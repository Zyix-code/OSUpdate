# 🔄 OSUpdate – Veritabanı Tabanlı Sürüm Kontrol Sistemi

<p align="center">
  <img src="https://media.giphy.com/media/Y4ak9Ki2GZCbJxAnJD/giphy.gif" width="150px">
</p>

<p align="center">
  <b>Masaüstü uygulamaları için geliştirilmiş, SQL tabanlı otomatik güncelleme ve versiyon takip mekanizması.</b><br>
  İstemci (.exe) ve sunucu (SQL) arasındaki sürüm farklarını analiz ederek güncellemeleri yönetir.
</p>

---

## 🚀 Özellikler

- ✔ **Merkezi Yönetim:** Sürümleri tek bir SQL tablosu üzerinden yönetme imkanı.
- ✔ **Sürüm Kontrolü:** İstemci sürümü ile sunucu sürümünü otomatik karşılaştırma.
- ✔ **Otomatik Tetikleme:** Belirlenen versiyon şartları sağlandığında güncellemeyi başlatma.
- ✔ **Esnek Yapı:** Farklı uygulama türlerine entegre edilebilir güncelleme mantığı.

<p align="center">
  <img src="https://img.shields.io/badge/Language-C-00599C?logo=c&logoColor=white&style=flat-square">
  <img src="https://img.shields.io/badge/Database-SQL-CC2927?logo=microsoftsqlserver&logoColor=white&style=flat-square">
  <img src="https://img.shields.io/badge/License-GPLv3-blue.svg?style=flat-square">
</p>

---

## 🧠 Sistem Nasıl Çalışır?

OSUpdate, uygulamanın güncel olup olmadığını anlamak için aşağıdaki mantıksal döngüyü takip eder:

### 1️⃣ Veritabanı Bağlantısı
- Uygulama başlangıcında hedef veritabanı ile güvenli bir bağlantı kurulur.

### 2️⃣ Tablo Yapısı
- Veritabanında güncelleme verilerini tutacak `version` adında bir tablo oluşturulur.
- Bu tablo, uygulamanın en son kararlı sürüm numarasını saklar.

### 3️⃣ Versiyon Kontrolü
- **Senaryo A (Güncel):** Çalışan uygulamanın (`.exe`) versiyonu, veritabanındaki versiyona eşitse veya daha büyükse işlem yapılmaz.
- **Senaryo B (Eski Sürüm):** Çalışan uygulamanın versiyonu, veritabanındaki hedeflenen sürümden düşükse güncelleme tetiklenir.

### 4️⃣ Güncelleme İşlemi
- Versiyon farkı tespit edildiğinde, güncelleme paketi indirilir ve açık olan uygulamalara yama işlemi uygulanır.

---

## 🛠️ Kurulum ve Entegrasyon

### 1️⃣ Veritabanı Hazırlığı
Veritabanınızda versiyon kontrolü için aşağıdaki gibi bir tablo oluşturun:

```sql
CREATE TABLE version (
    id INT PRIMARY KEY IDENTITY,
    version_number VARCHAR(50) NOT NULL,
    release_date DATETIME DEFAULT GETDATE()
);
```

### 2️⃣ Versiyon Girişi
Yayınlamak istediğiniz son sürümü tabloya ekleyin:
```sql
INSERT INTO version (version_number) VALUES ('1.0.5');
```

### 3️⃣ Entegrasyon
Projenizin Main bloğunda veritabanı sorgusunu çalıştırarak yerel sürüm ile sunucu sürümünü karşılaştırın.

⚖️ Lisans
Bu proje GNU General Public License v3.0 ile lisanslanmıştır. Projenin tüm kullanıcıları, lisansın koşullarına uymak kaydıyla projeyi özgürce kullanabilir, değiştirebilir ve paylaşabilir.

🤝 İletişim
<p align="left"> <a href="https://discordapp.com/users/481831692399673375"><img src="https://img.shields.io/badge/Discord-Zyix%231002-7289DA?logo=discord&style=flat-square"></a> <a href="https://www.youtube.com/channel/UC7uBi3y2HOCLde5MYWECynQ?view_as=subscriber"><img src="https://img.shields.io/badge/YouTube-Subscribe-red?logo=youtube&style=flat-square"></a> <a href="https://www.reddit.com/user/_Zyix"><img src="https://img.shields.io/badge/Reddit-Profile-orange?logo=reddit&style=flat-square"></a> <a href="https://open.spotify.com/user/07288iyoa19459y599jutdex6"><img src="https://img.shields.io/badge/Spotify-Follow-green?logo=spotify&style=flat-square"></a> </p>
