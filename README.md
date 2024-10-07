# PolisProReminder

## Wprowadzenie
Projekt PolisProReminder to backend aplikacji webowej, kt�ry zosta� zbudowany przy u�yciu technologii .NET Web API. Celem projektu jest dostarczenie efektywnego API do komunikacji mi�dzy aplikacj� internetow�. Aplikacja obs�uguje zarz�dzanie danymi, autoryzacj� u�ytkownik�w oraz udost�pnia zestaw funkcji poprzez API.

Aplikacja zosta�a zaprojektowana z my�l� o agentach ubezpieczeniowych. Celem projektu jest umo�liwienie sprawnego zarz�dzania polisami klient�w, usprawniaj�c procesy obs�ugi ubezpiecze�.

## Uruchomienie
W pliku konfiguracyjnym nale�y uzupe�ni� �cie�ke do przechowywania plik�w.
```json
{
  "StoragePath": "XXXX"
}
```

## Technologie
Projekt wykorzystuje nast�puj�ce technologie:
- **ASP.NET Core**: Framework umo�liwiaj�cy budow� aplikacji internetowych w j�zyku C#.
- **Entity Framework Core**: Narz�dzie do efektywnego zarz�dzania danymi zwi�zanymi z polisami ubezpieczeniowymi w bazie danych.
- **Swagger**: Generuje czyteln� dokumentacj� API, u�atwiaj�c� korzystanie z funkcji systemu.

## Funkcjonalno�ci
### 1. Zarz�dzanie Polisami
- Dodawanie, edycja i usuwanie polis ubezpieczeniowych klient�w.
- Dodawanie, edycja i usuwanie klient�w.
- Dodawanie, edycja i usuwanie towarzystw ubezpieczeniowych.

### 2. Autoryzacja i Bezpiecze�stwo
- Autentykacja agent�w poprzez tokeny JWT, zabezpieczaj�c dost�p do poufnych danych.
- Hierarchia r�l, umo�liwiaj�ca r�nicowanie uprawnie� w zale�no�ci od roli agenta.

## Kontrolery
![alt text](swagger.png "Swagger")
### 1. AccountController
`AccountController` jest dedykowany procesowi logowania do aplikacji. Zapewnia interfejs umo�liwiaj�cy agentom ubezpieczeniowym autoryzacj� poprzez dostarczenie danych uwierzytelniaj�cych. Kontroler ten nie oferuje funkcji rejestracji, a jedynie umo�liwia zalogowanie si� do systemu.

### 2. InsuranceCompanyController
Kontroler `InsuranceCompany` obs�uguje operacje zwi�zane z zarz�dzaniem firmami ubezpieczeniowymi. Zapewnia funkcje CRUD (Create, Read, Update, Delete) umo�liwiaj�ce dodawanie, edycj�, usuwanie oraz przegl�d informacji o poszczeg�lnych firmach ubezpieczeniowych.

### 3. InsuranceTypeController
Kontroler `InsuranceType` odpowiada za operacje zwi�zane z rodzajami polis ubezpieczeniowych. Umo�liwia dodawanie nowych typ�w polis, edycj� istniej�cych oraz przegl�d dost�pnych rodzaj�w ubezpiecze�.

### 4. InsurerController
`InsurerController` zajmuje si� zarz�dzaniem informacjami na temat ubezpieczaj�cych. Dostarcza interfejs do dodawania, edycji, usuwania oraz przegl�dania danych o klientach posiadaj�cych polisy ubezpieczeniowe.

### 5. PolicyController
Kontroler `Policy` obs�uguje operacje zwi�zane z polisami ubezpieczeniowymi. Zapewnia funkcje CRUD do zarz�dzania polisami, umo�liwiaj�c agentom ubezpieczeniowym dodawanie, edycj�, usuwanie oraz przegl�d szczeg��w dotycz�cych polis ich klient�w.

## Middleware
Aplikacja wykorzystuje middleware do przechwytywania wyj�tk�w, co pozwala na kontrolowane i jednolite zarz�dzanie b��dami w systemie. Middleware ten gwarantuje, �e nawet w przypadku wyst�pienia nieoczekiwanego wyj�tku, u�ytkownicy otrzymaj� odpowiedzi b��d�w z odpowiednimi komunikatami, a jednocze�nie logi aplikacji zawieraj� pe�ne informacje diagnostyczne.

## Diagram Bazy Danych
![alt text](database-diagram.png "Diagram bazy danych")
