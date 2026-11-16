# Frontend – Dokumentacja struktury projektu

## 1. Wprowadzenie

Aplikacja frontendowa została zbudowana z wykorzystaniem następujących technologii:

- **React** – biblioteka JavaScript służąca do tworzenia interfejsów użytkownika.
- **TypeScript** – rozszerzenie JavaScript zapewniające statyczne typowanie.
- **Vite** – narzędzie do uruchamiania serwera deweloperskiego i budowania aplikacji.
- **React Router** – biblioteka umożliwiająca nawigację pomiędzy stronami aplikacji.
- **TanStack Query** – biblioteka do zarządzania danymi pobieranymi z serwera i obsługi zapytań API.

Frontend komunikuje się z backendem zbudowanym w technologii ASP.NET Core.

---

## 2. Struktura projektu

```text
Frontend/
│
├── public/
│   └── favicon.ico
│
├── src/
│   │
│   ├── assets/
│   │   └── ...
│   │
│   ├── components/
│   │   └── ...
│   │
│   ├── hooks/
│   │   └── ...
│   │
│   ├── layouts/
│   │   └── ...
│   │
│   ├── pages/
│   │   └── ...
│   │
│   ├── routes/
│   │   └── ...
│   │
│   ├── services/
│   │   └── ...
│   │
│   ├── types/
│   │   └── ...
│   │
│   ├── utils/
│   │   └── ...
│   │
│   ├── App.tsx
│   ├── config.ts
│   ├── main.css
│   └── main.tsx
│
├── package.json
├── package-lock.json
├── tsconfig.json
└── vite.config.ts
```

---

## 3. Przeznaczenie folderów

### `public/`

Zawiera pliki statyczne, które są udostępniane bezpośrednio przez Vite i nie są przetwarzane przez bundler aplikacji.

Przykłady:
- Favicon (ikona strony).
- Obrazy statyczne.
- Inne publicznie dostępne pliki.

Pliki znajdujące się w tym folderze są dostępne bezpośrednio z głównego adresu aplikacji.

Przykład:

```tsx
<img src="/favicon.ico" alt="Favicon" />
```

W przeciwieństwie do plików z `src/assets/`, pliki z `public/` nie są importowane do TypeScriptu ani przetwarzane przez Vite.

### `src/assets/`

Zawiera zasoby aplikacji importowane do komponentów React lub innych plików źródłowych.

Przykłady:
- Obrazy.
- Ikony.
- Ilustracje.
- Czcionki.

Przykład:

```tsx
import blankImage from "../assets/blank.png";

function Example() {
  return <img src={blankImage} alt="Przykład" />;
}
```

### `src/components/`

Zawiera wielokrotnego użytku komponenty interfejsu użytkownika, które mogą być wykorzystywane na różnych stronach i w różnych layoutach.

Przykłady:
- Navbar – pasek nawigacyjny.
- Buttons – przyciski.
- Cards – karty.
- Modals – okna modalne.
- Form components – komponenty formularzy.

Komponenty powinny być w miarę możliwości niezależne od konkretnej strony aplikacji.

### `src/hooks/`

Zawiera własne hooki, które pozwalają wydzielać i ponownie wykorzystywać logikę aplikacji.

Przykłady:
- Pobieranie danych z API.
- Zarządzanie stanem.
- Obsługa efektów.
- Logika specyficzna dla aplikacji.

Przykład:

```tsx
const { data, isLoading, isError } = useContest();
```

### `src/layouts/`

Zawiera komponenty definiujące wspólną strukturę stron aplikacji.

Przykłady:
- DefaultLayout.
- DashboardLayout.
- AdminLayout.

Layouty mogą zawierać wspólne elementy interfejsu, takie jak paski nawigacyjne, panele boczne czy stopki.

Wykorzystują komponent React Router `<Outlet />`, który wyświetla aktualnie wybraną stronę podrzędną.

### `src/pages/`

Zawiera poszczególne strony aplikacji.

Każda strona reprezentuje konkretną trasę lub widok.

Przykłady:
- HomePage.
- ScrollPage.
- LoginPage.
- DashboardPage.

Strony łączą komponenty wielokrotnego użytku, layouty i logikę aplikacji, tworząc kompletne widoki.

### `src/routes/`

Zawiera konfigurację routingu aplikacji.

Plik `AppRouter.tsx` definiuje dostępne trasy i przypisuje im odpowiednie strony oraz layouty.

Przykład:

```tsx
<Routes>
  <Route element={<DefaultLayout />}>
    <Route path="/" element={<HomePage />} />
    <Route path="/scroll" element={<ScrollPage />} />
  </Route>
</Routes>
```

### `src/services/`

Zawiera moduły odpowiedzialne za komunikację z zewnętrznymi usługami, przede wszystkim z API backendu.

**`api.ts`**

- Centralna konfiguracja komunikacji HTTP.
- Adres bazowy API.
- Wspólna obsługa żądań.
- Wspólne nagłówki i obsługa błędów.

**`ContestService.ts`**

- Operacje API związane z konkursami.
- Pobieranie danych konkursowych.
- Tworzenie i aktualizowanie konkursów.
- Inne żądania związane z obsługą konkursów.

Warstwa serwisów powinna odpowiadać za komunikację z backendem, zamiast umieszczać logikę zapytań HTTP bezpośrednio w komponentach interfejsu użytkownika.

### `src/types/`

Zawiera współdzielone typy i interfejsy TypeScript.

Przykłady:
- Contest.
- Config.
- Typy odpowiedzi API.
- Modele żądań i odpowiedzi.

Folder ten pomaga zachować spójność struktur danych w całej aplikacji.

### `src/utils/`

Zawiera funkcje pomocnicze wielokrotnego użytku, które nie są bezpośrednio powiązane z komponentami React ani operacjami API specyficznymi dla aplikacji.

Przykłady:
- Formatowanie dat.
- Operacje na tekstach.
- Przekształcanie danych.
- Funkcje walidujące.

---

## 4. Przepływ danych w aplikacji

Typowy przepływ danych podczas pobierania informacji o konkursie wygląda następująco:

```text
ScrollPage.tsx
     |
     v
useContest.ts
     |
     v
ContestService.ts
     |
     v
api.ts
     |
     v
Backend ASP.NET Core
     |
     v
Baza danych
```

### Odpowiedzialność poszczególnych elementów

1. **ScrollPage.tsx** – wyświetla informacje związane z konkursem.
2. **useContest.ts** – zarządza zapytaniem i udostępnia komponentom React jego stan oraz pobrane dane.
3. **ContestService.ts** – definiuje operacje API związane z konkursami.
4. **api.ts** – odpowiada za komunikację HTTP.
5. **Backend ASP.NET Core** – przetwarza żądania i komunikuje się z bazą danych.

Pobrane dane wracają przez te same warstwy i ostatecznie są wyświetlane w interfejsie użytkownika.

---

## 5. Uruchamianie frontendu

Instalacja zależności projektu:

```bash
npm install
```

Uruchomienie serwera deweloperskiego:

```bash
npm run dev
```

Vite wyświetli lokalny adres aplikacji, zazwyczaj:

```text
http://localhost:5173/
```

Budowanie aplikacji w wersji produkcyjnej:

```bash
npm run build
```

Lokalne podglądanie wersji produkcyjnej:

```bash
npm run preview
```