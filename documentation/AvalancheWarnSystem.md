# **`AvalancheWarnSystem` Doku**
## Konstruktoren
* `public AvalancheWarnSystem()` Standard-Konstruktor der Klasse. Erstellt neue Risiko Matrix (`RiskMatrixFromText`), erstellt neuen Lawinenlagebericht ( dessen Konstruktor `new AvalancheStatusReport()`) und neue Karte (`new Map()`) und ruft dann die Methode zum Einstellen der Warnungen auf (`ConfigurateWarnings`).  
* `public AvalancheWarnSystem(object test)` Konstruktor, der bewirkt, dass der vorimplementierte Test-Lawinenlagebericht geladen wird. Wie Standard-Konstruktor nur wird der Lawinenlagebericht mit dem Konstruktor `new AvalancheStatusReport(test)`  

## Felder
* `public event EventHandler<PositionChangedEventArgs>? OnPositionChanged` durch Positionsänderung ausgelöstes Event.  
* `public event EventHandler<RiskEventArgs>? OnRiskmid` durch erhöhte Gefahr (mitlere Stufe) ausgelöstes Event.  
* `public event EventHandler<RiskEventArgs>? OnRiskhigh` durch hohe Gefahr (höchste Stufe) ausgelöstes Event. 
* `private Logging? myLogging` Instanz der Protokollierungs- (Logging-)Klasse. Wenn nicht null, stellt es die Methoden zum Protokollieren bereit, die mit den Events verknüpft werden können.  
* `public Map thisMap` Feld enthält Instanz der `Map` Klasse mit zugrundeliegender Karte.  
* `public AvalancheStatusReport myAVSReport` Instanz der `AvalancheStatusReport` Klasse mit aktuellem Lawinenlagebericht.  
* `public RiskLevel[][] RiskMatrix` Vier kreuz Fünf Matrix (Gradient)(Lawinenlagestufe), enthält ein GefährdungsLevel (`RiskLevel`). Die Einträge für den Gradienten (erster Index) ergeben sich nach folgendem Schema: 0 entspricht <30°, 1 entspricht <35°, 2 entspricht <40°, 3 entspricht >=40°.  
* `private Position CurrentPosition` Repäsentiert die aktuelle Position des Systems. Wird das System nach der Prototypen-Phase im Feld verwended, kann dieser Wert von einem Positionssensor (GPS-Sensor) bezogen werden.

## Methoden
* `public void manipulatePosition(Position position)` Methode um die Position des Systems in dieser Prototypen-Phase zu verändern (Bewegung zu simulieren).   
* `public void EvaluatePosition()` wertet die Postition im Bezug auf die Gefährdung aus. Dazu wird die Position zwischengespeichert, sodass diese konstant ist während der Auswertung. Dann werden der Gradient, die Höhe und die Exposition der aktuellen Position aus der zugrunde-liegenden Karte bezogen und anhand der Risiko Matrix (`RiskMatrix`) ausgewertet. Ergibt sich ein erhöhtes oder gar hohes persönliches Risiko, wird das zugehörige Event ausgelöst.  
* `public void CountinuousEvaluatePosition(object? sleepTime)` führt die Positionsauswertung (`EvaluatePosition` siehe oben) laufend aus. Zwischen zwei Auswetungen wird um die möglicherweise übergebene Zeitspanne `sleepTime` oder ansonsten die vorimlementierte Zeitspanne (`const int StandardSleepTime=500`) pausiert.  
* `private void InitiateLogging()` richtet Protokollierung ein. Dazu erstellt die Methode Instanz der Loggingklasse (`myLogging` siehe oben) mit dem fest implementierten Dateipfad (`const string Logfilepath="data/LogDatei.txt"`) und verknüpft die Events mit den zugehörigen Methoden der Instanz. Wird von der Methode zum Einstellen der Warnungen (`ConfigurateWarnings()`) aufgerufen.  
* `private void ConfigurateWarnings()` Methode zum Einrichten und Einstellen der Warnungen. Wird vom Konstruktor aufgerufen. Fragt ab ob diese Selbst eingestellt oder die Standard-Werte bezogen werden sollen. Zur Wahl stehen ob eine Protokolldatei geschrieben werden soll und welche Warnungen für die beiden Gefährungsstufen (erhöhtes und hohes Risiko) ausgelöst werden sollen. Die Warn-Möglichkeiten sind Push-benachrichtigung,  Push-benachrichtigung mit Warnton oder Push-benachrichtigung mit Warnton und Blinklicht. Wird die Auswahl abgebrochen, werden ebenfalls die Standard-Werte geladen.  
* `private RiskLevel[][] RiskMatrixFromTxt(string path)` liest die unter dem übergebenen Dateipfad (`path`) liegende Datei ein und gibt eine Risiko-Matrix zurück. Wird vom Konstruktor verwendet um das Feld `RiskMatrix` zu füllen. Die Datei muss genau vier zeilen, mit je genau fünf durch Komma getrennten Werten zwischen eins und fünf enthalten.
