# **`AvalancheWarnSystem` Doku**
## Konstruktoren
`public AvalancheWarnSystem()`  beinhaltet alle Eingaben in das Warnsystem.
`public AvalancheWarnSystem(object test)` Testversion des Warnsystems.

## Felder
`public event EventHandler<PositionChangedEventArgs>? OnPositionChanged` durch Positionsänderung ausgelöstes Event.
`public event EventHandler<RiskEventArgs>? OnRiskmid` durch mittlere Gefahr ausgelöstes Event.
`public event EventHandler<RiskEventArgs>? OnRiskhigh` durch hohe Gefahr ausgelöstes Event.
`private Logging? myLogging` Zugriff aufs die Protokollierung.
`public Map thisMap` die zurgundeliegende Landkarte.
`public AvalancheStatusReport myAVSReport` der aktuelle Lawinenlagebericht.
`public RiskLevel[][] RiskMatrix` die Matrix enthält das Gefahrenlevel an einem Punkt mit bestimmtem Gradienten. die Gefahrenstufen sind in Abhängigkeit des Gradienten folgende: 0 für <30°, 1 für <35°, 2 für <40°, 3 für >=40°.
`private Position CurrentPosition` hier wird die aktuelle Position dargestellt.

## Methoden
`public void manipulatePosition(Position position)` um die Position in diesem Test zu verändern.
`public void EvaluatePosition()` wertet die Postition im Bezug auf die Gefahr aus. Dazu werden der Gradient und das Gefahrenlevel geprüft.
`public void CountinuousEvaluatePosition(object? sleepTime)` wertet in festen Zeitabständen die Position unter Zuhilfenahme  der obenstehenden EvaluatePosition Methode aus.
`private void InitiateLogging()` erstellt Instanz der Loggingklasse und verknüpft die dafür benötigten Events mit den jeweiligen Protokollierungsmethoden.
`private void ConfigurateWarnings()` hier können die Warnungen nach Wunsch verändert werden. Zur Wahl stehen eine Loggdatei und eine der unter `Warnings.cs` genannten Warnarten mit der Auswahl für erhöhtes und hohes Risiko verschiedene Warnungen zu bekommen. Alternativ werden "Werkseinstellungen" verwendet.