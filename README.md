# Poorgrammer
Prüfungsleistung für das Modul Softwareentwicklung von Bjarne Jacobsen und Lena Zimmermann (Team "Poorgrammer").

## Thema
Es soll ein Warnsystem erstellt werden, um Skitourengeher zu warnen bzw. an ihre Situation zu erinnern, wenn sie sich in eine Gefahrensituation begeben. Die grobe Idee zur Umsetzung ist im Wiki unter "Entwurf" zu finden.

### Ziel
Die Nutzenden unserer Lawinenwarnung sollen beruhigt Skitouren gehen können, da sie auf ihren Handys eine Warnung ihrer Wahl erhalten, falls sie sich in Gefahr befinden. Am Ende des Tages können sie zusätzlich ihre am Tag zurückgelegte Route anschauen. Dazu werden verschiedene Klassen angelegt, die dem aktuellen Klassendiagramm im Wiki folgen.
Das Lawinenwarnsystem setzt sich zusammen aus dem Lawinenlagebericht AvalanceStatusreport.cs und der Lagekarte Map.cs. Das ganze wird von der Position bestimmt, sodass im Falle einer Gefahrenposition eine Event (RiskEventArgs.cs) ausgelöst wird. Dieses löst dann die vom Nutzer gewählte Warnung aus mit der Warnings.cs Klasse.
