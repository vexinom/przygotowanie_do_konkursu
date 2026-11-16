# MathScroll
Autorstwa:
- Martyna Plenzler
- Piotr Suchoń
- Franciszek Wiatr
- Łukasz Woźniak

--- 

## Nowości:
* dodano schemat backendu 
* dodano działające API dla problemów matematycznych 
* **dodano działąjące API dla konkursów** oraz lekkie poprawki porzedniego

* dodano schemat frontendu
* skonfigurowano łączenie aplikacji frontendu z API


## Co zainstalować:
1. **.NET 10 SDK** - na fedorze można przy pomocy: `sudo dnf install dotnet-sdk-10.0` (ale wierzę, że znajdziecie jak to zrobić u siebie;) )
Mint / Ubuntu pewnie też `udo apt install dotnet-sdk-10.0`
2. **Docker i Docker Compose** - do bazy danych testowej
3. **Node.js** – środowisko uruchomieniowe JavaScript oraz **npm** - menedżer pakietów Node.js `sudo dnf install nodejs`

## Jak uruchomić:
1. Baza danych stawiana jest lokalnie w kontenerze. Prezd uruchomieniem należy dodać plik `.env` w katalogu repozytorium, zawierający dane do logowania np:
```
POSTGRES_DB=konkursy
POSTGRES_USER=konkursy_admin
POSTGRES_PASSWORD=admin1234
```
Następnie bazę można wystartować za pomocą `docker compose up -d`.
(rzecz jasna nazwy, hasła itp można sobie zmieniać, to jest jedynie lokalne)

2. Aby odpalić backend: 
``` bash
cd Backend/App
dotnet restore
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=konkursy;Username=postgres;Password=postgres"
dotnet run
```

3. Aby odpalić frontend:
``` bash
cd Frontend
npm install
npm run dev
```
Po uruchomieniu aplikacja będzie dostępna pod adresem: `http://localhost:5173`.