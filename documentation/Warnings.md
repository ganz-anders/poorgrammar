# **Warnings Doku**
Hier sind die verschiedenen Warnungen an die Skitourenden programiert.
Es wird jeweils der Aufruf mit dem Event der Gefahr ausgelöst. 
## Methoden
`public static void PushMessage(object? caller,RiskEventArgs args)` liefert eine Nachricht gefüllt mit dem Wortlaut der Warnung, der aktuellen Zeit und Position sowie der Risikostufe für Lawinenabgang und dem zugehörigen Schneeproblem.
`public static void Sound(object? caller,RiskEventArgs args)` ist eine Nachricht mit Ton ind löst einen Piepton aus.
Eine Nachricht mit Ton und Text wäre folglich eine Kombination aus beidem.
`public static void MessagewithFlashingLight(object? caller,RiskEventArgs args)` gibt eine Blinknachricht. Sie leuchtet fünf Mal abwechselnd rot (Intervall von 100) und schwarz (Intervall von 50).