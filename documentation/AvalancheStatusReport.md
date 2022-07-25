# **`AvalancheStatusReport` Doku**
## Konstruktoren
`public AvalancheStatusReport()` Füllt den Lawinenlagebericht durch die manuelle Eingabe der Daten durch die Skitourenden. Fragt als erstes nach der Existenz von höhenabhängigen Gefahrenstufen. Danach wird für diese Stufen die Gefahr und die Gefahrenstufe eingelesen. Die Daten werden gespeichert und für die nächsten Höhenstufen überprüft. Abschließend erfolgt das Einlesen der Schneeprobleme, je nach Richtung.
`public AvalancheStatusReport(object test)` Füllung zum Testen mit den Daten aus dem unter "Entwurf" gespeicherten Lawinenlagebericht.

## Felder
`private List<AvalancheLevel_Height> AvalancheLevel_ac2Height` für die höhenabhängige Lawinengefahr.
`private Dictionary<Direction, List<SnowProblem>> SnowProblem_Direction` für die richtungsabhängigen Schneeprobleme.

## Methoden
`public void printReport()` gibt den kompletten Lawinenlagebericht an die Console aus.
`public void printReport(string filepath)` speichert den Lawinenlagebericht in den Dateipfad.
`public AvalancheLevel getAvalancheLevel_Height(int height)` überprüft, in welcher höhenabhängigen Lawinenwarnstufe man sich aktuell befindet. Das Mamimum sind hier 9000m, da es ja keine höheren Berge gibt.
`public List<SnowProblem> getSnowProblem_Direction(Direction direction)` läd die in der aktuell übergebenen Richtung vorherschenden Schneeprobleme ins System.
