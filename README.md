# IoT Platform Dashboard (iot-platform-dashboard)

Aplikacja webowa do monitorowania, zarządzania i wstępnej analizy danych urządzeń Internetu Rzeczy (IoT). Projekt powstaje w ramach pracy inżynierskiej poświęconej analizie wpływu mechanizmów bezpieczeństwa na wydajność platformy IoT opartej na protokole MQTT.

---

## Technologie

### Backend (.NET)

* ASP.NET Core Web API (.NET 8/9) - RESTowe API obsługujące logikę biznesową i komunikację z frontendem.
* Entity Framework Core - ORM do mapowania obiektowo-relacyjnego.
* MS SQL Server - Relacyjna baza danych (uruchamiana w kontenerze Docker).

### Frontend (React)

* React z Vite - Szybkie budowanie interfejsu użytkownika.
* TypeScript - Bezpieczeństwo typów.
* Tailwind CSS - Nowoczesny i responsywny design panelu administracyjnego.

---

## Architektura Projektu (Clean Architecture)

Rozwiązanie .NET Solution zostało podzielone na warstwy zgodnie z zasadą Separation of Concerns:

```text
IoTPlatform/
├── src/
│   ├── IoTPlatform.Api/             # Główny projekt Web API (Kontrolery, Middleware, DI)
│   ├── IoTPlatform.Application/     # Logika biznesowa, DTO, interfejsy serwisów
│   ├── IoTPlatform.Domain/          # Czyste encje domenowe i modele danych
│   └── IoTPlatform.Infrastructure/  # Persystencja danych (EF Core, kontekst bazy MS SQL)
├── frontend/                        # Aplikacja kliencka w React (TypeScript + Tailwind)
└── docker-compose.yml               # Konfiguracja środowiska bazodanowego

```

---

## Modele Danych (Encje)

System operuje na czterech głównych strukturach danych:

1. Users - Użytkownicy systemu, uwierzytelnianie oraz role (wariant z rolami użytkowników).
2. Devices - Zarejestrowane urządzenia IoT (identyfikator MQTT, status, nazwa).
3. Measurements - Historia pomiarów napływających z urządzeń (temperatura, wilgotność itp.).
4. Alerts - Alerty systemowe i powiadomienia o zdarzeniach krytycznych.

---

## Szybki start (Instalacja i Uruchomienie)

### Wymagania wstępne

* .NET SDK (wersja 8.0 lub nowsza)
* Node.js & npm
* Docker & Docker Compose

### 1. Klonowanie repozytorium

```bash
git clone https://github.com/TwojaNazwaUzytkownika/iot-platform-dashboard.git
cd iot-platform-dashboard

```

### 2. Uruchomienie bazy danych (MS SQL Server)

W głównym katalogu projektu uruchom kontener bazy danych:

```bash
docker-compose up -d

```

### 3. Uruchomienie Backendu (.NET Web API)

Przejdź do projektu API i uruchom serwer:

```bash
cd src/IoTPlatform.Api
dotnet run

```

API domyślnie nasłuchuje na adresie https://localhost:5001 (lub wskazanym porcie HTTP).

### 4. Uruchomienie Frontendu (React)

Otwórz nowe okno terminala, przejdź do folderu frontendu, zainstaluj paczki i uruchom aplikację deweloperską:

```bash
cd frontend
npm install
npm run dev

```

Aplikacja będzie dostępna pod adresem podanym w terminalu (zazwyczaj http://localhost:5173).

---

## Zakres prac badawczych (Cel projektu)

Aplikacja webowa stanowi warstwę wizualizacyjno-zarządczą dla szerszego eksperymentu naukowego, w ramach którego testowane będą trzy warianty bezpieczeństwa komunikacji MQTT:

1. Wariant 1: Brak szyfrowania + logowanie hasłem.
2. Wariant 2: TLS + tokeny JWT.
3. Wariant 3: TLS + tokeny JWT + zaawansowane role użytkowników (RBAC).
