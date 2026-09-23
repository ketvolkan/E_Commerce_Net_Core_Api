# 🛒 E-Commerce Micro-Monolith API (.NET 10 & PostgreSQL) (Halen Geliştirme Aşamasında)

Bu proje, **Trendyol** benzeri çok satıcılı (Multi-Vendor) pazaryeri e-ticaret platformları için geliştirilmiş, yüksek performanslı, ölçeklenebilir ve güvenli bir RESTful Web API servisidir.

Katmanlı Mimarı (N-Layer Architecture), **Aspect-Oriented Programming (AOP)** ve **Autofac** altyapısı kullanılarak nesne yönelimli yazılım prensiplerine (SOLID) uygun olarak tasarlanmıştır.

---

## 🏛️ Proje Mimari Yapısı

Proje 5 ana katmandan oluşmaktadır:

* **Core**: Projeden bağımsız, tüm katmanlarda kullanılabilen evrensel altyapı nesneleri (Aspects, Cross-Cutting Concerns, Utilities, Security/JWT, Base Entities & Repositories).
* **Entities**: Veritabanı tablolarına karşılık gelen somut sınıflar (POCO) ve Data Transfer Object (DTO) tanımları.
* **DataAccess**: Entity Framework Core ile PostgreSQL veritabanı erişim katmanı, ORM mapping ve Fluent API konfigürasyonları.
* **Business**: İş kuralları, FluentValidation doğrulamaları, Autofac interceptor yönetimi ve servis yönlendirmeleri.
* **WebAPI**: Dış dünyaya açılan RESTful Controller endpoint'leri ve middleware yapılandırmaları.

---

## 🗺️ Veritabanı Mimarısı (ER Diagram)

Sistemdeki Satıcı (Store), Ürün-Varyant (Product-SKU), Sipariş-Alt Sipariş (Order-SubOrder) ve Kullanıcı ilişkilerini gösteren veritabanı şeması aşağıda yer almaktadır:

<!-- Veritabanı diagram görselinizi projenizin içine atıp yolunu (örneğin: docs/er-diagram.png) buraya ekleyin -->
<img width="813" height="824" alt="image" src="https://github.com/user-attachments/assets/e0e6e85b-ee94-4934-a344-6188890bcf1b" />

---

## 🛠️ Kullanılan Teknolojiler ve Kütüphaneler

* **Framework:** .NET 10 Web API
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core (Code-First)
* **IoC & AOP Container:** Autofac, Autofac.Extras.DynamicProxy
* **Validation:** FluentValidation
* **Security:** JWT (JSON Web Tokens), Hashing & Salting (SHA-512)
* **Logging & Caching:** MemoryCache, Log4Net
* **Documentation:** Swagger / OpenAPI

---

## 🚀 Öne Çıkan Özellikler

- **Multi-Vendor Sipariş Yönetimi:** Müşteri tek bir sepet onayladığında, sepetteki farklı satıcılara ait ürünler için arka planda otomatik olarak bağımsız `SubOrder` (Alt Sipariş) kayıtları oluşturulur.
- **Dinamik Varyant (SKU) Yapısı:** Ürünler; renk, beden, stok ve satıcı bazlı fiyatlandırmalar ile `ProductVariant` seviyesinde yönetilir.
- **AOP Aspect Altyapısı:** Method bazlı `[ValidationAspect]`, `[CacheAspect]`, `[TransactionAspect]` ve `[LogAspect]` desteği.
- **JWT Tabanlı Kimlik Doğrulama:** Rol tabanlı yetkilendirme (Claim-Based Authorization) altyapısı.

---

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
* [.NET 10 SDK](https://dotnet.microsoft.com/)
* [PostgreSQL](https://www.postgresql.org/)




