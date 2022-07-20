# **Warnings Doku**
Hier sind die verschiedenen Warnungen an die Skitourenden programiert.
Es wird jeweils der Aufruf mit dem Event der Gefahr ausgelöst. 
Dann liefert die `PushMessage` eine Nachricht gefüllt mit dem Wortlaut der Warnung, der aktuellen Zeit und Position sowie der Risikostufe für Lawinenabgang und dem zugehörigen Schneeproblem.
Eine Nachricht `Sound` mit Ton würde einen Piepton mit einem Intervall von 10 auslösen. Eine Nachrricht mit Ton und Text wäre folglich eine Kombination aus beidem.
Eine Blinknachricht mit Text `MessagewithFlashingLight` würde fünf Mal abwechselnd rot (Intervall von 100) und schwarz (Intervall von 50) leuchten.