# **`Map` Klasse Doku**
## Konstruktor
* `public Map()`
liest die Karte aus dem direkt im Konstruktor gespeicherten Pfad `const string fileaddress="data/map.txt"` ein und füllt die Felder der Klasse. Mehr im Abschnitt "Zu-Grunde-liegende Karte"
## Felder
* `private const float Grid=250.0f;` //Gitter in m  
* `private Position NWReference;` //Referenz Punkt am Nord-West Ende der Karte  
* `private Position SEReference;` //Referenzpunkt gegenüber  
* `private string UTMZoneReference;` //UTM Zonen Referenz, nicht benötigt im Weiteren  
* `private double[][] MapData;` //Höhenrasterdaten
## Methoden
* `public bool PositionOnMap(Position position)` //gibt true zurück, wenn die übergebene Position sich auf der Karte befindet  
* `public int getGradient(Position position)` //Berechnet den Gradienten an der Stelle der übergebenen Position auf Grundlage der Karte (`MapData`), prüft vorher `PositionOnMap`  
* `public Direction? getDirection(Position position)` //Berechnet die Exposition/Himmelsrichtung an der übergebenen Position, prüft vorher `PositionOnMap`  
* `public int getHeightAboveSL(Position position)` //Berechnet die Höhe über NN an der übergebenen Position auf Basis von bilinearer Interpolation, prüft vorher `PositionOnMap`  

## Zu-Grunde-liegende Karte
Die Karte wird durch den Konstruktor unter dem Pfad `data/map.txt` geladen. Sie stellt ein Array dar, was aus der Textdatei geladen wird. Die Textdatei muss daher einem strengen Aufbau folgen, um vom Programm akzeptiert zu werden:

### Aufbau
1. Nord-West-Referenz (nord-westlichster Punkt) der Karte, entspricht den Koordinaten des ersten Eintrages in der Liste der Höhenwerte: `Schlüsselwort North-West-Reference:` Leerzeichen `Kartenausschnitt` Leerzeichen `Längenwert(x)` Leerzeichen `Breitenwert(y)`  
2. mit Komma getrennt die Dimensionen (Anzahl der Einträge) `xdim,ydim`    
3. Spalten-(y) und Zeilenweise getrennt die `Höhenwerte(z)` durch Tabstopps `\t` getrennt (Zeilen=x-Richtung, ein y-Wert)  

#### Beispiel:  
`North-West-Reference: 32T 649740 5253860`  
`49,49`  
`0.0333564014835872\t0.0632480721672403 ...`  
`...`  

### Karten-Daten
Die zur Zeit zu Grunde liegenden Daten sind künstlich erzeugt und entsprechen keinen realen Bergen.
Es sind die die Beträge der Werte folgender, auch als Peaks-function bekannten Funktion:  
z =  3*(1-x)^2*exp(-(x^2) - (y+1)^2) - 10*(x/5 - x^3 - y^5)*exp(-x^2-y^2) - 1/3*exp(-(x+1)^2 - y^2)
![Softwareentwicklung_map](https://user-images.githubusercontent.com/102985804/179988180-dade45e4-84e5-4c11-8e81-e761cbe5bfa9.jpg)


