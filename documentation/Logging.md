# **`Logging` Doku**
Hier werden die Daten für den späteren Abruf in einer Datei gespeichert.
## Felder
`private static string? Filepath;` Dateipfad unter der die Log-datei geschrieben wird
## Methoden
`public static void LogPosition(object? caller, PositionChangedEventArgs args)` schreibt die Position mit Länge, Breite und aktueller Zeit in die Datei unter `filepath`, wird durch zugehöriges Event OnPositionChanged ausgelöst, wenn verknüpft. Gibt eine Warnung wenn das nicht möglich ist.  
`public static void LogWarning(object? caller,RiskEventArgs args)` schreibt die gesendeten Lawinenwarnungen in die Dateiunter `filepath`. Gibt eine Warnung wenn das nicht möglich ist.  
`public Logging(string filepath)` - Konstruktor der Klasse Warning, notwendig bevor Methoden ausgeführt werden können, da filepath gefüllt sein muss.
