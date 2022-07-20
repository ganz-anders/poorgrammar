# Poorgrammer
Prüfungsleistung von Bjarne Jacobsen und Lena Zimmermann (Team "Poorgrammer") für das Modul Softwareentwicklung im Sommersemester '22 an der TU Bergakademie Freiberg.

#### Die (selbstgewählte) Aufgabenstellung:
Es soll ein Warnsystem erstellt werden, um Skitourengeher zu warnen bzw. an ihre Situation zu erinnern, wenn sie sich in eine Gefahrensituation begeben.
Die grobe Idee zur Umsetzung ist im Wiki unter "Entwurf" zu finden. Ebenfalls im Wiki finden Sie die Dokumentation. 

## Die Motivation
Als Skitourengeher sind Sie oft einer großen Gefahr ausgesetzt. Unser System soll Ihnen helfen sicherer zu bleiben und beruhigt Skitouren gehen zu können. Das System wertet Ihre Position aus und warnt Sie, wenn Sie Acht geben sollten. Sie können eine Warnung ihrer Wahl erhalten, falls Sie sich in Gefahr befinden. Optional können Sie sämtliche Warnungen und Positionsveränderungen in eine Logdatei schreiben. Am Ende des Tages können Sie dort ihre am Tag zurückgelegte Route anschauen und den aktuellen Lawinenlagebericht sehen.

## Das System
Die Position des Systems wird in festen Abständen hinsichtlich den geografischen Gegebenheiten, vorallem Höhe und Hangneigung ausgewertet. Die Bewertung des Risikos erfolgt anhand einer Gefährdungsmatrix. Befinden Sie und das System sich an einer Position mit erhöhtem Gefährdungspotential aufgrund des aktuellen eingegebenen Lawinenlageberichts und der an den aktuellen Position vorherrschenden geografischen Gegebenheiten, sollen die gewählten Warnungen ausgelöst werden.

Die Abhängigkeiten dabei ergeben sich wie folgt: Die Lawinenlagestufe hängt primär von der Höhe ab. Die Gefährdung hängt von Lawinenlagestufe und Hangneigung ab, da ein Lawinenabgang bei stärker geneigten Hängen (v.a. >35°) wahrscheinlicher wird. Die Hauptgefahr, also die Schneelage vor der gewarnt wird (z.B. Triebschnee), hängt von der Exposition (Himmelsrichtung) ab wird ebenfalls einbezogen.

Zur Demonstration der Software läuft unser System zur Zeit in einer Simulation, die das Warn-System ausführt und die Bewegung des Sytems bzw. Änderung der Position entlang einer Testroute simuliert. Es ist ein Test-Lawinenlagebericht implementiert, aber Sie können selbstverständlich auch den tagesaktuellen LLB eingeben.
