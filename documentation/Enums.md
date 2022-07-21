# **`Enums.cs`**
In den Enums stehen die Konstanten, die für den jeweiligen Werttyp relevant sind. Ihre Konstantenwerte werden von Null an hochgezählt.

## Richtung
Die Himmelsrichtungen gehören alle zu den Directions, weshalb sie sich im Selben Enum befinden. Den vier Himmelsrichtungen wurden zur Differenzierung und damit genaueren Bestimmung der Lawinenlage Unterrichtungen zugeordnet. Diese liegen jeweils mittig zwischen den beiden äußeren Richtungen.

`enum Direction
{
    N,NO,O,SO,S,SW,W,NW
}`

## Schneeform
Die seperaten Schneeprobleme wurden einem Enum SnowProblem zugeordnet. Im folgenden sind die Probleme durch die Schneeformen mit Hilfe der [Wikipedia](https://de.wikipedia.org/wiki/Lawine#Entstehung) erklärt.

`enum SnowProblem
{
    Neuschnee, Triebschnee, Nassschnee, Altschnee, Gleitschnee
}`

### Neuschnee
Das Problem mit Neuschnee ist die Menge. Je nach Situation erhöht sich die Lawinengefahr ab 50 oder schon ab 10 cm Neuschnee.

### Triebschnee
Triebschnee wird durch den Wind transportiert. So werden die Eiskristalle abgeschliffen und können sich nicht so gut wie bei Neuschnee verzahnen, das verringert den Zusammenhalt deutlich. Triebschnee lagert sich Aufgrund seiner Entstehung an windabgewendeten und unten an windzugewandten Hängen ab. Rinnen und Mulden sind auch gefährdet. Triebschnee bildet Schneewehen, die hart und weich sein können. Dadurch entsteht ein großes Gefahrenpotential für sogenannte Schneebrettlawinen, die auch durch Neuschnee nicht verhindert werden können
![Schneebrettlawine](https://upload.wikimedia.org/wikipedia/commons/a/ab/Schneebrett.jpg)

### Nassschnee
Kommt es zu einem plötzlichen und starken Temperaturanstieg feuchtet der Schnee bis auf den Boden durch und die Schneekristalle verändern ihre Struktur. Dadurch steigt die Gefahr für sogenannte Nassschneelawinen. Diese bilden sich auch an Südhängen oder solchen mit nahezu rechtwinkliger Sonneneinstrahlung.

### Altschnee
Ist die Schneeschicht schon alt, besteht sie aus verschiedenen Schneetypen, die untereinander verschiedene Festigkeiten aufweisen. Unter dem über die Zeit wachsenden Druck reicht dann ein kleines Zusatzgewicht (z.B. ein Skitourer) oder das erste Durchfeuchten der Schicht um den Altschnee als Lawine abgehen zu lassen.

### Gleitschnee
Die durch Gleitschichten ausgelösten Schneebrettlawinen (s.o.) stellen die klassische Gefahr für Skifahrende dar. Eine Gleitschicht entsteht durch den [Nigg-Effekt](https://de.wikipedia.org/wiki/Nigg-Effekt). Demnach bildet sich auf der Schneeoberfläche Reif und die darurch entstehende Gefahrenlage ist schwer einzusehen. Diese Form tritt häufig zu Beginn und am Ende des Winters auf. Ein anderer Grund für Gleitschneelawinen ist glatter Untergrund wie z.B. Grashänge.

## Gefahrenlevel
In diesem Enum stehen die fünf [europäischen Gefahrenstufen](https://de.wikipedia.org/wiki/Europ%C3%A4ische_Gefahrenskala_f%C3%BCr_Lawinen). Die Skala existiert seit 1933 und richtet sich hauptsächlich an Wintersportler ausherhalb der Pisten.

`enum AvalancheLevel
{
    gering, mäßig, erheblich, groß, sehr_groß
}`

## Risiko
Hier sind die verschiedenen Risiken für Skitourende aufgelistet.

`enum RiskLevel
{
    niedrig,mittel,hoch
}`
