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


## Co zainstalować:
1. **.NET 10 SDK** - `sudo dnf install dotnet-sdk-10.0` (to jest instalka na fedore, ale znajdziecie jak to zrobić u siebie)
2. Docker i Docker Compose - do bazy danych testowej

## Jak uruchomić:
1. Baza danych stawiana jest lokalnie, wystarczy ją odpalić `docker compose up -d` w katalogu repozytorium. Login do bazy należy dodać do pliku `.env` w katalogu repozytorium o zawartości:
```
POSTGRES_DB=konkursy
POSTGRES_USER=konkursy_admin
POSTGRES_PASSWORD=admin1234
```
(rzecz jasna nazwy, hasła itp można sobie zmieniać, to jest jedynie lokalne)

2. Start backendu: 
```
cd Backend/App
dotnet restore
```
