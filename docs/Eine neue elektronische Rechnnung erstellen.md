# Eine neue elektronische Rechnnung erstellen
## Rechnungsart wählen
Der Standardwert für die Rechnungsart ist RECHNUNG. Klick auf Pfeil neben Rechnung öffnet eine Auswahlliste mit weiteren Rechnungsarten. Ggf. andere Rechnungsart wählen.
## Rechnungsausteller eingeben 
Beim Aufruf des Formulars werden die in den Einstellungen erfassten Werte eingefügt. Diese können im Formular temporär geändert werden. Mit <TAB> wird der Cursor von Feld zu Feld bewegt.
## Rechnungsempfänger eingeben
**ACHTUNG**: Alle im Formular mit ![](Eine neue elektronische Rechnnung erstellen_http://www.ebinterface.at/gallery/codeplex/pfeil.jpg) gekennzeichneten Felder sind Pflichtfelder und müssen ausgefüllt werden!

Adress- und Kontaktdaten des Rechnungsempfängers eingeben. Mit <TAB> wird der Cursor von Feld zu Feld bewegt. Die eingegebenen Werte werden nur für die erstellte Rechnung gespeichert. Bei erneutem Aufruf des Formulars sind diese Werte wieder zu erfassen. 

Formale Daten der Rechnung eingeben. Die Rechnungsnummer kann aus den Zeichen 0-9, a-z, A-Z, -_ ÄÖÜäöüß bestehen. Alle Datumsangaben können aus dem eingeblendeten Kalender gewählt werden. Bei der Eingabe der formalen Daten erfolgt keine Plausibilitätsprüfung. Eine Prüfung gemäß ebInterface Standard erfolgt erst nach Aufruf von „eRechnung prüfen“ aus dem Ribbon oder mit dem Speichern des Dokumentes als XML! Dabei festgestellte Fehler werden in einer Liste im rechten Bildschirmbereich unter **Dokumentaktionen Hinweise und Fehler** angezeigt. Die Werte können dann korrigiert werden. 
## Rechnungstitel eingeben (optional)
In diesem Feld kann ein Dokumententitel im Freitext eingegeben werden, zB Proforma Rechnung. Dieser Eintrag hat jedoch keine Auswirkung auf die Rechnungsart und dient ausschließlich zur Information.
## Rechnungspositionen erfassen
# Aus dem eingeblendeten Ribbon unter Aktionen "Details bearbeiten" wählen
# Das Fenster "Rechnungspositionen" wird geöffnet![](Eine neue elektronische Rechnnung erstellen_http://www.ebinterface.at/gallery/codeplex/eb_09.jpg)
# „Bearbeiten -> Einfügen“ klicken oder die Taste EINFG drücken![](Eine neue elektronische Rechnnung erstellen_http://www.ebinterface.at/gallery/codeplex/eb_09_1.jpg)
# Bestell-Positionsnummer (falls laut Auftragsreferenz erforderlich), Artikelnummer, Bezeichnung, Menge, Einzelpreis und ggf. Rabatt eingeben, Einheit aus der Auswahlliste wählen und MWST prüfen
# Mit ÜBERNEHMEN die erfassten Daten in die Rechnungspositionsliste übernehmen
# Ggf. weitere Rechnungspositionen erfassen. Dazu die letzten beiden Schritte wiederholen, bis alle Daten erfasst sind.
# Durch Klick auf „Bearbeiten -> Ändern“ oder Doppelklick auf die markierte Zeile kann die erfasste Rechnungsposition geändert werden
# Durch Klick auf „Bearbeiten -> Löschen“ oder die Taste ENTF kann die markierte Zeile aus der Liste gelöscht werden
# Durch Klick auf „Detailpositionen -> Alle löschen“ können alle erfassten Rechnungspositionen entfernt werden
# Mit Klick auf „Detailpositionen ->SPEICHERN und Schliessen“ werden die Werte in das Rechnungsformular übernommen
# Standardwährung ist EUR. Die Währung kann durch Auswahl aus der Auswahlliste neben EUR geändert werden

## Zahlungsbedingung festlegen
# Standardmäßig wird das Rechnungsdatum als Fälligkeitsdatum übernommen
# Durch Klick neben das Datum kann aus dem eingeblendeten Kalender ein anderes Datum als Fälligkeitsdatum festgelegt werden.

## Skonti festlegen (Optional)
# Aus dem eingeblendeten Ribbon unter Bearbeiten > "Skonti bearbeiten" wählen
# Das Fenster "Skonto Zahlungsbedingungen" wird geöffnet
# Der Rechnungsbetrag = Basisbetrag aufgrund der eingegebenen Rechnungspositionen, das Rechnungsdatum und das Fälligkeitsdatum werden angezeigt
# Rechnungsdatum und Fälligkeitsdatum können hier direkt geändert werden und werden beim Speichern in das Formular übernommen. Dazu auf den Pfeil neben dem Datum klicken und aus dem eingeblendeten Kalender das gewünschte Datum auswählen.
# Alternativ kann das Fälligkeitsdatum auch durch Eingabe von Tagen gesetzt werden. Die Berechnung des Datums erfolgt dann sogleich nach der Eingabe

![](Eine neue elektronische Rechnnung erstellen_http://www.ebinterface.at/gallery/codeplex/eb_10.jpg)
# „Bearbeiten -> Einfügen“ klicken oder die Taste EINFG drücken
# Skontodetails nun im Fenster  „Skonto bearbeiten“ erfassen
![](Eine neue elektronische Rechnnung erstellen_http://www.ebinterface.at/gallery/codeplex/eb_10_10.jpg)
# Skontotage oder Skonto Datum, bis zu welchen ein Skonto gewährt wird, eingeben. Liegt das Skontodatum (berechnet aufgrund der Tage) nach dem Fälligkeitsdatum, so erscheint eine Fehlermeldung und die Daten sind zu korrigieren 
# Skontoprozentsatz eingeben
# Skontobetrag wird vom Rechnungsbetrag berechnet und angezeigt
# Mit ÜBERNEHMEN die erfassten Daten in die Skontoliste übernehmen
# Ggf. weitere Skontobedingungen erfassen. Dazu die letzten vier Schritte wiederholen, bis alle Daten erfasst sind.
# Durch Klick auf „Bearbeiten -> Ändern“ oder Doppelklick auf die markierte Zeile kann die erfasste Skontoposition geändert werden
# Durch Klick auf „Bearbeiten -> Löschen“ oder die Taste ENTF kann die markierte Zeile aus der Liste gelöscht werden
# Durch Klick auf „Skonto -> Alle löschen“ können alle erfassten Skontopositionen entfernt werden
# Mit Klick auf „Skonto ->SPEICHERN und Schliessen“ werden die Werte in das Rechnungsformular übernommen
**Hinweis:** Es können maximal zwei Skontobedingungen erfasst werden.

## Kopf- und Fußtext der Rechnung
Durch Klick auf das Feld neben Kopf- bzw. Fußtext wird die Eingabemöglichkeit aktiviert. Gewünschten Text eingeben und den Cursor dann auf eine andere Stelle im Formular positionieren. 

## Hinweise und Anmerkungen zu Rechnung (Optional)
In diesem Feld können Erläuterungen zur Rechnung im Freitext eingegeben werden. 

## Verweise auf eine frühere Rechnung (Optional)
Unter diesem Abschnitt kann eine Verbindung des aktuellen Formulars zu einem früheren Dokument hergestellt werden.
![](Eine neue elektronische Rechnnung erstellen_http://www.ebinterface.at/gallery/codeplex/eb_10_11.jpg)
Bei der Verweisart stehen drei Auswahlmöglichkeiten zur Verfügung:
* Kein Verweis: Dies ist die Standardauswahl beim Erstellen einer neuen Rechnung. Es sind keine weiteren Eingaben in der Zeile nötig.
* Storno: Hierbei muss unter „Rechnungsnr.“ die Nummer derjenigen Rechnung eingegeben werden, die storniert werden soll. Auch das Rechnungsdatum dieser Rechnung muss erfasst werden.
* Verweis: Auch hier muss unter „Rechnungsnr.“ die Nummer derjenigen Rechnung eingegeben werden, auf die verwiesen werden soll. Dies kann zB eine vorangegangene Teilrechnung sein.
