# Healio 🩺✨

**Egészségügy, ami összeköt – orvosok és betegek egy platformon!**

Üdvözlünk a Healio-ban, egy modern egészségügyi platformon, ahol az orvosok és betegek közötti kommunikáció egyszerűbb, mint valaha! Időpontfoglalás, élő csevegés, orvosi tanácsadás – mindez egy színes, felhasználóbarát felületen, API-kkal turbózva. 🚀

## Miért Healio? 🌟
- **Időpontfoglalás villámgyorsan:** Betegek pár kattintással foglalhatnak időpontot orvosukhoz.
- **Orvos-beteg csevegés:** Valós idejű kommunikáció, hogy a kérdéseid ne maradjanak válasz nélkül.
- **Személyre szabott élmény:** Színes, intuitív dizájn, ami mindenki számára könnyen használható.
- **Biztonságos és megbízható:** Az adataid nálunk jó kezekben vannak.

## Főbb Funkciók ⚙️

| Funkció               | Leírás                                              |
|-----------------------|-----------------------------------------------------|
| 🕒 **Időpontfoglalás**  | Válaszd ki az orvosodat és a neked megfelelő időpontot! |
| 💬 **Chat**            | Beszélj közvetlenül az orvosoddal, akár otthonról is. |
| 📋 **Egészségügyi profil** | Kövesd nyomon az adataidat és korábbi konzultációidat. |
| 🌐 **API-k**           | Külső rendszerekkel is integrálható, rugalmas megoldások. |
| 🎨 **Színes UI**       | Vidám, modern dizájn, ami feldobja az egészségügyi élményt. |

## Hogyan Indítsd El? 🚀

1. **Klónozd a repót:**
    ```bash
    git clone https://github.com/username/healio.git
    ```

2. **Nyisd meg a Visual Studioban:**
    Használd a `.sln` fájlt a projekt betöltéséhez.

3. **Állítsd be az adatbázist:**
    Frissítsd a `appsettings.json` fájlban a MySQL connection stringet:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=healio_db;User=root;Password=yourpassword;"
    }
    ```

4. **Futtasd a projektet:**
    - Nyomj F5-öt, vagy futtasd a parancssorból:
      ```bash
      dotnet run
      ```

5. **Nyisd meg a böngészőben:**
    [https://localhost:5001](https://localhost:5001) – és kész is!

## Tech Stack 🛠️
- **Frontend:** Razor Pages – dinamikus, szerveroldali renderelés modern köntösben
- **Backend:** ASP.NET Core – robusztus és gyors keretrendszer
- **Adatbázis:** MySQL – megbízható és hatékony adattárolás
- **API-k:** RESTful endpointok – integráció más rendszerekkel

## API Endpointok (Példa) 🌐

| Metódus | Útvonal              | Leírás                          |
|---------|----------------------|---------------------------------|
| GET     | `/api/appointments`   | Lekéri az összes időpontot     |
| POST    | `/api/appointments`   | Új időpont foglalása           |
| GET     | `/api/users/{id}`     | Felhasználói adatok lekérése   |

Részletes dokumentáció hamarosan a `/docs` mappában!

## Hozzájárulás 🤝

Szeretnél részt venni a Healio fejlesztésében? Örömmel várjuk a pull requesteket!

1. Forkold a repót.
2. Hozz létre egy új branch-et:
    ```bash
    git checkout -b feature/uj-funkcio
    ```
3. Commitold a változtatásaidat:
    ```bash
    git commit -m "Új funkció: XY"
    ```
4. Pushold fel:
    ```bash
    git push origin feature/uj-funkcio
    ```
5. Nyiss egy Pull Requestet, és mesélj róla! 😊

## Képernyőképek 📸
- **Főoldal:**
![image](https://github.com/user-attachments/assets/312b036a-d9f0-45b5-915e-09a97d0a54fb)

- **Időpontfoglalás:** [Kép itt]

## Csatlakozz a Közösséghez! 🌐
- Twitter: [@HealioHealth](https://twitter.com/HealioHealth)
- Discord: Healio Community
- Email: [hello@healio.com](mailto:hello@healio.com)

_"Az egészségügy nem kell, hogy szürke legyen – a Healio színt visz az életedbe!"_

– A Healio Csapat
