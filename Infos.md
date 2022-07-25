## Erklärungen zum Plant UML Diagram

Direction (N,NO,O,SO,S,SW,W,NW)
SnowProblem (Neuschnee, Triebschnee, Nassschnee, Altschnee, Gleitschnee)
AvalancheLevel (gering, mäßig, erheblich, groß, sehr_groß)
Position (x, y Wert)

AvalancheLevel_Height
nötig um Liste mit den Lawinenwarnstufen in Abhänigkeit der Höhe anzulegen, immer mit oberer Grenze angegeben

#### class AvalancheStatusReport
- Liste der Warnstufen in Abh. der Höhe
- Dictionary mit allen Himmelsrichtungen und beliebig vielen zugehörigen Schneeproblemen
- "+ PrintReport()" Methode zum Ausgeben des gesamten Lageberichts
- "+ getAvalancheLevel_Height" um die Lawinenwarnstufe zur Höhe zu erhalten
- "+ getSnowProblem_Dircetion" um die Hauptschneeprobleme zur Himmelsrichtung zu erhalten
- Konstruktor - muss sich um einlesen aller Felder von der Konsole und erstellen der zugehörigen Felder kümmern

#### class Map
- MapData DatenFeld, dass die Karte beeinhaltet
- "+ getGradient(Position:Position):int" Methode um Gradienten zu erhalten
- "+ getDirection(Position:Position):Direction" Methode um Himmelsrichtung zu erhalten
- "+ getHeightAboveSL(Position:Position):int" Methode um Höhe über Meereshöhe (SL=Sea Level) zu erhalten
- "- MapFromTxt():void" Method um Karte aus Text Datei zu erzeugen, vom Konstruktor benötigt
-Konstruktor "Map()" 


#### class Warnings
enthält alle Warnarten
#### class Logging
fehlt noch

#### class AvalancheWarnSystem
+ Map: thisMap - Feld, dass die Karte enthält
+ RiskMatrix: int[][] - Feld, dass die Gefährdungsmatrix enthält
+ Position:Position << get >>,  << set >> - liest die x- und y-Werte ein
+ EvaluatePosition():void //wichtigste Methode, zur Auswertung
- ConfigurateWarnings():void //liest gewünschte Warnungen ein und verknüpft Delegaten, vom Konstruktor aufgerufen
- ConfigurateMap():void //liest Dateipfad zur Map ein und erzeugt Map, vom Konstruktor aufgerufen
- RiskMatrixFromTxt(fp FilePointer): int[][] //liest Risiko Matrix zur Bewertung ein, vom Konstruktor aufgerufen
+ AvalancheWarnSystem()

#### struct WorkingPosition
- OffsetTime:int - Zeit, die sich das System an dieser Postion befinden wird, bevor es die Position wechselt 
- Postion:Position    

#### class SimulationSystem
- "Route:Queue<WorkingPosition>"
- "ReadinTestRoute()" Hilfsfunktion gibt Queue mit WorkingPosition(s) zurück
- "PositionSimulation(Route Queue<WorkingPosition>)" Funktion für die Routen Simulation, bekommt das eingelesene Array übergeben
- "Main()" kümmert sich um Ausführung des WarnSystems und dem Ändern der Position 


@enduml
