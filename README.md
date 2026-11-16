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


## Co zainstalować:
1. **.NET 10 SDK** - na fedorze można przy pomocy: `sudo dnf install dotnet-sdk-10.0` (ale wierzę, że znajdziecie jak to zrobić u siebie;) )
Mint / Ubuntu pewnie też `udo apt install dotnet-sdk-10.0`
2. **Docker i Docker Compose** - do bazy danych testowej

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
```
cd Backend/App
dotnet restore
dotnet run
```
