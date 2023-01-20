Uruchomienie projektu

1.Utwórz baze danych postgresql

Dane do połączenia bazy(możliwość zmiany w pliku appsettings.json):
Nazwa bazy: inzynier
Identyfikator użytkownika: postgres
Password: password

2.Przeprowadź migrację w konsoli za pomocą polecenia:

dotnet ef database update


3.Uruchom projekt za pomocą przycisku run lub za pomocą komendy:

dotnet run
