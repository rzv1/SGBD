# L01 SGBD 

Aplicatie desktop dezvoltata in C# cu framework-ul Avalonia UI, utilizand arhitectura MVVM si baza de date PostgreSQL.

## 1. Configurarea Bazei de Date

Pentru a rula aplicatia, trebuie sa aveti instalat un server PostgreSQL local. 

* **Crearea bazei de date si a tabelelor:** Rulati fisierul schema.sql in cadrul terminalului PostgreSQL pentru a crea baza de date si structura tabelelor pe serverul local. 

## 2. Configurarea Conexiunii

Aplicatia utilizeaza un Connection String pentru a comunica cu serverul PostgreSQL. Acesta se configureaza in codul sursa in App.axaml.cs:

1. Localizati variabila de configurare a conexiunii.
2. Modificati parametrii conform setarilor locale ale serverului dumneavoastra:

   string connectionString = "Host=localhost;Database=gym;Username=postgres;Password=[PAROLA]";

    * Host: Adresa serverului (implicit localhost).
    * Database: Numele bazei de date (gym).
    * Username: Utilizatorul de Postgres (implicit postgres).
    * Password: Parola setata la instalarea PostgreSQL.

## 3. Rularea Aplicatiei

### Cerinte de sistem:
* .NET SDK (versiunea 8.0 sau mai noua).
* Un IDE compatibil: JetBrains Rider, Visual Studio sau VS Code.

### Instructiuni de pornire:

#### Varianta A: Din IDE (Recomandat)
1. Deschideti fisierul solutie (.sln) in Rider sau Visual Studio.
2. Asteptati ca IDE-ul sa restaureze automat pachetele NuGet (Npgsql, Avalonia.Controls.DataGrid).
3. Apasati butonul Run.

## 4. Utilizarea Interfetei
* Selectare: Selectati un client din tabelul din stanga pentru a vedea platile sale in tabelul din dreapta.
* Adaugare: Completati campurile de input de sub tabelul de plati si apasati butonul Add.
* Update: Modificati datele direct in celulele tabelului de plati, apoi apasati butonul Update (sau Save).
* Delete: Selectati o plata si apasati butonul Delete.