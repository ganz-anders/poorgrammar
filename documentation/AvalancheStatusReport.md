# **`AvalancheStatusReport` Doku**
## Konstruktoren
* `public AvalancheStatusReport()` Füllt den Lawinenlagebericht durch die manuelle Eingabe der Daten durch den Nutzenden. Fragt als erstes nach der Existenz von Unterteilung(en) in höhenabhängige Gefahrenstufen. Danach wird für diese Stufen die Gefahrenstufe und die Höhenobergrenze, bis zu der diese gilt, eingelesen. Die Daten werden gespeichert und für die nächsten Höhenstufen überprüft. Abschließend erfolgt das Einlesen der Schneeprobleme, für jede Himmelsrichtung.
* `public AvalancheStatusReport(object test)` Füllung mit beispielhaftem Lawinenlagebericht (Lawinenlagebericht, der im Wiki unter Entwurf/Umsetzung zu finden ist)

## Felder
* `private List<AvalancheLevel_Height> AvalancheLevel_ac2Height` Liste aus höhenabhängigen Lawinenlagestufen (siehe `AvalancheLevel_Height`) - enthält damit den Kerninhalt eines Lawinenlageberichts. Die Obergrenze des letzten Eintrags (oberste Stufe) ist immer 9000(m) da es keine höheren Berge gibt.
* `private Dictionary<Direction, List<SnowProblem>> SnowProblem_Direction` Feld, das für jede Himmelsrichtung eine Liste, der dort auftretenden Schneeprobleme enthält. (siehe `SnowProblem`)

## Methoden
* `public void printReport()` gibt den kompletten Lawinenlagebericht an die Konsole aus.
* `public void printReport(string filepath)` schreibt den Lawinenlagebericht in die Protokoll Datei unter dem Dateipfad (`filepath`).
* `public AvalancheLevel getAvalancheLevel_Height(int height)` gibt die Lagestufe wieder, die in der übergebenen Höhe vorherscht. Prüft dafür in welcher höhenabhängigen Lawinenwarnstufe aus `AvalancheLevel_ac2Height` man sich aktuell befindet. Beschränkung auf 9000m (siehe oben)
* `public List<SnowProblem> getSnowProblem_Direction(Direction direction)` gibt Liste der in der übergeben Richtung vorherschenden Schneeprobleme wieder (kann auch leer sein). Durch Auswahl und Rückgabe des passenden Eintrags aus `SnowProblem_Direction` 

