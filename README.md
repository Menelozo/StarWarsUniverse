# StarWarsUniverse

To see the live project, please go to: [Star Wars Universe](https://www.starwars.nomadsearch.net/)

## Instrukcja uruchomienia

### Klonowanie repozytorium 

1. Otwórz wiersz poleceń i wprowadź komendę: git clone https://github.com/Menelozo/StarWarsUniverse.git
   
### Uruchamianie aplikacji z linii poleceń

1. Przejdź do katalogu projektu: cd StarWarsUniverse/StarWarsUniverseApp

2. Stwórz foldery bin i Debug w projekcie SharpTrooper-master jeśli nie istnieją:
   mkdir SharpTrooper-master\bin
   mkdir SharpTrooper-master\bin\Debug

3. Skopiuj plik Newtonsoft.Json.dll do folderu Debug: copy packages\Newtonsoft.Json.13.0.1\lib\net40\Newtonsoft.Json.dll SharpTrooper-master\bin\Debug\

4. Przejdź do katalogu projektu: cd StarWarsUniverseApp
   
5. Zainstaluj zależności: dotnet restore

7. Uruchom aplikację: dotnet run

8. Otwórz przeglądarkę i przejdź do `http://localhost:5271/` (lub odpowiedniego portu wskazanego przez aplikację).

### Otwieranie projektu w Visual Studio

1. Otwórz Visual Studio.

2. Wybierz `Plik` > `Otwórz` > `Projekt` z menu głównego.

3. Przejdź do katalogu, w którym sklonowałeś repozytorium, i wybierz plik `.sln` (rozszerzenie rozwiązania Visual Studio).

4. Kliknij `Otwórz`.

5. Po otwarciu projektu w Visual Studio, możesz zbudować i uruchomić aplikację klikając `Uruchom` (zielony przycisk) lub naciskając `F5`.
