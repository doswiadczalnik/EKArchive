# EK Archive

**EK Archive** to aplikacja desktopowa napisana w C# w technologii Windows Forms, umożliwiająca pobieranie danych o zapotrzebowaniu energetycznym z publicznego API. Program obsługuje mechanizm cache, który przechowuje dane w skompresowanym pliku na dysku, co przyspiesza ponowne zapytania.

## Funkcjonalności

- Pobieranie danych o zapotrzebowaniu energetycznym dla wybranego dnia.
- Mechanizm cache oparty na lazy-inicjalizacji
- Obsługa alertów energetycznych z odpowiednimi kolorami w tabeli:
  - **Zalecane użytkowanie** (kolor: ciemnozielony)
  - **Normalne użytkowanie** (kolor: jasnozielony)
  - **Zalecane oszczędzanie** (kolor: żółty)
  - **Wymagane ograniczanie** (kolor: czerwony)
- Mechanizm cache:
  - Dane zapisywane są w skompresowanym pliku `cache.dat`.
  - Cache jest automatycznie sprawdzany przed wykonaniem zapytania do API.
  - Dzień bieżący zawsze odświeżany.
- Asynchroniczne zapisywanie cache w tle.

## Technologie

- **C#** - Główny język programowania.
- **Windows Forms** - Tworzenie interfejsu użytkownika.
- **Newtonsoft.Json** - Przetwarzanie danych JSON.
- **System.IO.Compression** - Kompresja i dekompresja pliku cache.

## Wymagania

- **System operacyjny**: Windows 7 lub nowszy.
- **Framework**: .NET Framework 4.8 lub wyższy.
- **Visual Studio**: Wersja 2022 (zalecana).

## Instalacja i uruchomienie

1. **Sklonuj repozytorium**:
   
   ```bash
   git clone https://github.com/doswiadczalnik/EKArchive
   ```

2. **Otwórz projekt w Visual Studio**.

3. **Przygotowanie środowiska**:
   
   - Upewnij się, że masz zainstalowany pakiet NuGet **Newtonsoft.Json**.
   
   - Jeśli nie, zainstaluj go:
     
     ```bash
     Install-Package Newtonsoft.Json
     ```

4. **Zbuduj i uruchom projekt**:
   
   - Naciśnij `Ctrl+Shift+B`, aby zbudować projekt.
   - Uruchom aplikację (`F5`).

## Użycie

1. Wybierz datę za pomocą kalendarza w aplikacji.
2. Kliknij **"Pobierz dane"**.
3. Aplikacja wyświetli:
   - Tabelę z godzinowym zapotrzebowaniem i alertami.
   - Dodatkowe informacje: `Doba handlowa` i `Data publikacji`.
4. Dane są automatycznie zapisywane do cache.

## Plik cache

- **Nazwa pliku**: `cache.dat`.
- **Lokalizacja**: Katalog roboczy aplikacji.
- **Format**: Skompresowany JSON (GZip).

## Struktura JSON w cache:

```json
{
  "2025-01-01": [
    {
      "udtczas": "2025-01-01 00:00:00",
      "znacznik": 0,
      "business_date": "2025-01-01",
      "source_datetime": "2025-01-01 22:25:25.722"
    }
  ]
}
```

## Wsparcie

Jeśli podoba Ci się ten projekt, możesz mnie wesprzeć, kupując wirtualną kawę:

<a href="https://buycoffee.to/doswiadczalnik" target="_blank"><img src="https://buycoffee.to/img/share-button-primary.png" style="width: 351px; height: 92px" alt="Postaw mi kawę na buycoffee.to"></a>

## Licencja

Projekt jest udostępniany na licencji MIT. Szczegóły w pliku `LICENSE`.

---

**Autor**: "Doświadczalnik" Marcin Gołembiewski
