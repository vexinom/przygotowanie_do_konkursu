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

* dodano opcję logowania użytkowników po stronie backendu
* **dodano możliwość zmiany i resetu hasła przez email** oraz potwierdzanie adresu email
* zmieniono model wpisów historii
* dodano seeder wstawiający przykładowe dane do bazy przy starcie 


## Co zainstalować:
1. **.NET 10 SDK** 
    - Fedora: `sudo dnf install dotnet-sdk-10.0` 
    - Mint / Ubuntu: `sudo apt install dotnet-sdk-10.0`
2. **Docker i Docker Compose** - do bazy danych testowej
    - Fedora: 
    ```
    sudo dnf -y install dnf-plugins-core
    sudo dnf config-manager addrepo --from-repofile=https://download.docker.com/linux/fedora/docker-ce.repo
    sudo dnf install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
    ```
    - Ubuntu / Mint:
    ``` bash
    sudo apt update
    sudo apt install docker.io docker-compose-v2
    ```

    Po instalacji warto dodać uprawnienia: 
    ``` bash
    sudo systemctl enable --now docker
    sudo usermod -aG docker $USER
    ```

3. **Node.js** – środowisko uruchomieniowe JavaScript oraz **npm** - menedżer pakietów Node.js `sudo dnf install nodejs`
    - Fedora: `sudo dnf install nodejs`
    - Ubuntu / Mint: 
    ``` bash
    sudo apt update
    sudo apt install nodejs npm
    ```

## Jak uruchomić:
1. Baza danych do testów stawiana jest lokalnie w kontenerze docker. W pierwszej kolejności należy stworzyć plik `.env` w katalogu repozytorium, zawierający dane do logowania np:
```
POSTGRES_DB=konkursy
POSTGRES_USER=konkursy_admin
POSTGRES_PASSWORD=admin1234
```
Następnie, bazę można wystartować za pomocą `docker compose up -d`.
(rzecz jasna nazwy, hasła itp można sobie zmieniać, to jest jedynie lokalne)

2. Wymagana jest jednorazowa konfiguracja sekretów dotnet (przechowujących hasło i login do bazy danych): 
``` bash
cd Backend/App
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=konkursy;Username=konkursy_admin;Password=admin1234"
```
**w polach: Database, Username, Password muszą być te same wartości co w pliku `.env`!!!**

Przy każdej kolejnej aktualizacji, by włączyć backend wystarczy:
``` bash
cd Backend/App
dotnet restore
dotnet dotnet run --launch-profile https
```
Po uruchomieniu serwer będzie dostępny pod adresem: `https://localhost:7147` lub `http://localhost:5162` (można połączyć się przez UI Scalar dodając na końcu: `/scalar`). 

***Uwaga!*** - aby przesyłanie ciasteczek działało poprawnie wymagane jest użycie połącznie **https**! 


3. Aby odpalić frontend:
``` bash
cd Frontend
npm install
npm run dev
```
Po uruchomieniu aplikacja będzie dostępna pod adresem: `http://localhost:5173`.