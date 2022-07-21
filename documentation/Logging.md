# **`Logging` Doku**
Hier werden die Daten für den späteren Abruf gespeichert.
## Methoden
`public static void LogPosition(object? caller, PositionChangedEventArgs args)` speichert die Position mit Länge und Breite. Gibt eine Warnung wenn das nicht möglich ist.
`public static void LogWarning(object? caller,RiskEventArgs args)` speichert die gesendeten Lawinenwarnungen. Gibt eine Warnung wenn das nicht möglich ist.
`public Logging(string filepath)` schreibt alles zusammen in eine Datei.