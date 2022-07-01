# Poorgrammer
Prüfungsleistung für das Modul Softwareentwicklung von Bjarne Jacobsen und Lena Zimmermann (Team "Poorgrammer").

## Thema
Es soll ein Warnsystem erstellt werden, um Skitourengeher zu warnen bzw. an ihre Situation zu erinnern, wenn sie sich in eine Gefahrensituation begeben.

## Grob-Entwurf
Es soll selbständig eine Auswahl aus definierten Warnarten getroffen werden. Bsp. eine einfache Nachricht oder eine Nachricht mit Ton. Zusätzlich soll die Möglichkeit bestehen eine Datei mit Log-Daten der Warnungen anzulegen.

Die Position des Systems soll in festen Abständen hinsichtlich den geografischen Gegebenheiten, vorallem Höhe, Hangneigung und Hangexposition (Himmelsrichtung), ausgewertet werden. Die Bewertung erfolgt anhand einer Gefährdungsmatrix. Befindet sich das System an einer Position mit erhöhtem Gefährdungspotential aufgrund des aktuellen eingegebenen Lawinenlageberichts und der an den aktuellen Position vorherrschenden geografischen Gegebenheiten, sollen die gewählten Warnungen ausgelöst werden.

Die Position soll während der Auswertung auf Auswahl des Nutzers in eine Log-Datei geschrieben werden können, um die Route später nachverfolgen zu können, oder bsp. bei wenig Sicht den Weg zurück finden zu können.

Zusätzlich soll eine Simulation erstellt werden, die das Warn-System ausführt und die Bewegung des Sytems bzw. Änderung der Position simuliert. Dazu soll die Positionsauswertung des Warnsystems fortlaufend in einem Hintergrund-Thread ausgeführt werden, während ein Vordergrund-Thread die Position des Systems simuliert.

Eine Benachrichtigungsklasse mit den verschiedenen Methoden zur Benachrichtigung soll erstellt werden, deren methoden mit einem Delegaten verknüpft und durch ein Event ausgelöst werden können.

Es werden Höhenrasterdaten als Eingabe benötigt, über denen verschiedene Interpolationsmethoden ausgeführt werden können. So soll die Hangneigung (Gradient), die aktuelle Höhe und die Hangexposition berechnet werden können.

Weitere Eingaben sollen den Lawinenlagebericht annehmen können, einschließlich aktueller Warnstufe in zugehöriger Höhenlage und Hangexposition.

![Beispielhafter Lawinenlagebericht](https://www.vol.at/2019/02/lawinenlagebericht.jpg)

Dem Beispielhaften Lawinenlagebericht ist zu entnehmen, dass die Gefährdungsstufe am ausgewählten Ort oberhalb von 1,600m 3 beträgt und alle Himmelsrichtungen gefährdet sind. Unterhalb von 1600m beträgt die Stufe 2 und es sind besoders Ost-, Süd- und Westhänge gefährdet.
