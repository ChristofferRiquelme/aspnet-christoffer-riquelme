# GymPortal

GymPortal är en webbapplikation byggd med ASP.NET Core MVC där användare kan registrera sig, logga in och hantera sitt medlemskap samt boka träningspass.

---

## Funktioner

- Registrering och inloggning (ASP.NET Identity)
- Hantera profil (uppdatera uppgifter och profilbild)
- Ta bort konto (inklusive all personlig data)
- Se tillgängliga träningspass
- Boka och avboka pass
- Skaffa medlemskap (Standard / Premium)
- Dashboard för användaren (My Account)

---

## Tekniker

- ASP.NET Core MVC
- Entity Framework Core (Code First)
- SQLite
- ASP.NET Identity
- Tailwind CSS

---

## Installation & start

### 1. Klona projektet

git clone https://github.com/ChristofferRiquelme/aspnet-christoffer-riquelme.git
cd GymPortal

### 2. Kör projektet

dotnet run

### 3. Öppna i webbläsaren

https://localhost:xxxx

### 4. Databas

Databasen skapas automatiskt via Entity Framework Core (Code First) när applikationen startas första gången.

⸻

### 5. Tester

Projektet innehåller enhetstester för central domänlogik, till exempel:

- Bokningar (kan ej boka samma pass två gånger)
- Medlemskap (aktivt/inaktivt beroende på datum)

Kör tester med:

dotnet test

### 6. Säkerhet

* Skyddade routes med [Authorize]
* Formulärvalidering via ModelState
* Identity används för autentisering

### 7. Arkitektur

Projektet är uppdelat i flera lager:

* Web – Controllers & Views
* Infrastructure – Databas & Identity
* Domain – Affärslogik och modeller
* Tests – Enhetstester