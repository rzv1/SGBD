# Proiect Management Plati (L01)

Aplicatie desktop dezvoltata in C# cu framework-ul Avalonia UI, utilizand arhitectura MVVM si baza de date PostgreSQL.

## 1. Configurarea Bazei de Date

Pentru a rula aplicatia, trebuie sa aveti instalat un server PostgreSQL local. Urmati acesti pasi:

* **Crearea bazei de date:** Deschideti un terminal SQL (psql) sau un instrument grafic precum pgAdmin si rulati:
  CREATE DATABASE gym;

* **Crearea tabelelor:** Executati urmatoarele scripturi pentru a genera structura necesara in baza de date gym:

  CREATE TABLE customer (
  id SERIAL PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  phone VARCHAR(20)
  );

  CREATE TABLE payment (
  id SERIAL PRIMARY KEY,
  amount DECIMAL(10, 2) NOT NULL,
  type VARCHAR(50) NOT NULL,
  bank_name VARCHAR(100) DEFAULT 'N/A',
  id_customer INT REFERENCES customer(id) ON DELETE CASCADE
  );

## 2. Configurarea Conexiunii

Aplicatia utilizeaza un Connection String pentru a comunica cu serverul PostgreSQL. Acesta se configureaza in codul sursa (de regula in App.axaml.cs sau in clasa DatabaseManager):

1. Localizati variabila de configurare a conexiunii.
2. Modificati parametrii conform setarilor locale ale serverului dumneavoastra:

   string connString = "Host=localhost;Database=gym;Username=postgres;Password=PAROLA_TA";

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
3. Apasati butonul Run sau tasta F5.

#### Varianta B: Din Terminal
Navigati in folderul radacina al proiectului (unde se afla fisierul .csproj) si rulati urmatoarele comenzi:
dotnet restore
dotnet run

## 4. Utilizarea Interfetei
* Selectare: Selectati un client din tabelul din stanga pentru a vedea platile sale in tabelul din dreapta.
* Adaugare: Completati campurile de input de sub tabelul de plati si apasati butonul Add.
* Update: Modificati datele direct in celulele tabelului de plati, apoi apasati butonul Update (sau Save).
* Delete: Selectati o plata si apasati butonul Delete.