# Änderungshistorie
Auf dieser Seite finden Sie die Änderungen zu den einzelnen Versionen ab ebInterface 4.1.
# Word PlugIn für ebInterface 4.1 Beta Release
## Change Set CS581
* Umgehung für einen Bug in Word 2007 implementiert
## Change Set CS557
* Alle Texte für eRechnung an den Bund -> öffentliche Verwaltung
* Prüfung für Auftragsreferenz angepasst, andere öffentl. Verwaltung
* Bug-FIx bei Skonto
* **ACHTUNG:** Es wird empfohlen Rechnung die mit dieser Vorlage erstellt werden als PDF anstatt docx zu speichern, da die Feldeingaben in Docx nicht mitgespeichert werden
## Change Set CS522
* Div. Textfehler korrigiert
* Handbuch ergänzt
* kleinere Bug-Fixes
* Beta Status
## Change Set CS450
* Fehler in der Datenprüfung für Empfänger eMail korrigiert
* Fehler bei fehlender Empfänger USt ID korrigiert
* Probleme mit Word 2007 behoben
* **Beim ersten Start mit Word 2007 MÜSSEN die Einstellungen sofort ausgefüllt werden!** Bei den Word Versionen 2010 und 2013 ist dies nicht unbedingt erforderlich aber zweckmäßig.
## Change Set CS444
**ACHTUNG: Das ist eine ALPHA Release. Es wird nicht empfohlen diese Version Produktiv einzusetzen**
Mit dieser neuen Release wurde die Version 4.1 des ebInterface Standards umgesetzt. So wie bisher wurden - in Absprache mit AUSTRIAPRO - jene Teile aus dem ebInterface Standard ausgewählt, die die Gundbestandteile darstellen. Darüber hinaus wurde  das Formular auch für die eRechnung an den Bund ausgelegt.
## Änderungen und Neuerungen
**Generelle Neuerungen**
* **Neu:** Komplett neue Software Architektur (Bisher: Word CustomXML jetzt MVVM Development Pattern)
* **Neu:** Automatisierte Unit Tests für die meisten Funktionen (insg. mehr als 130 Testfälle wurde implementiert)
**Änderungen im Ribbon**
* **Neu:** Eigener Button für "eRechnung prüfen"
* **Neu:** Die Einstellungen wurden überarbeitet
* **Neu:** Die Integration eines Zustelldienstes für eRechnung Wirtschaft ist jetzt über die Einstellungen konfigurierbar
* Die Anordnung der Buttons wurde neu gruppiert
* Die Handy-Signatur ist derzeit nicht enthalten
**Änderungen im Formular**
* **Neu:** eRechnung für die Wirtschaft und eRechnung an den Bund sind in einem Formular zusammengefasst
* **Neu:** Der Wechsel zwischen eRechnung für die Wirtschaft und eRechnung an den Bund erfolgt über ein Auswahlfeld im Formular
* **Neu:** Die Ausgabe der Fehlernachrichten und Meldungen erfolgt in einem Panel "Dokumentaktionen" 
* **Neu:** Die Meldungen können per Mausklick ausgewählt, in die Zwischenablage kopiert werden und in weiterer Folge in ein neues Word Dokument oder eine E-Mail eingefügt werden
* **Neu:** Es können jetzt Hinweise und Anmerkungen zu einer Rechnung mehrzeilig erfasst werden. 
* **Neu:** Es ist möglich einen Verweis auf ein früheres Dokument (z.B. auf vorherige Rechnung) zu erfassen
* **Neu:** Es ist möglich, ein Storno eines früheren Rechnungs- oder Gutschriftsdokumentes zu erstellen
* **Neu:** Alle Feldprüfungen wurden komplett neu implementiert
* **Neu:** In allen Textfeldern können jetzt auch Sonderzeichen wie z.B. Ampersand (&), Hochkomma (' oder "), Schrägstrich (\, /), Bindestrich etc. eingegeben werden. 
* Die Datenprüfung für den Bund erfolgt gemäß dem Stand per 10.07.2014
**Vorlagen**
* **Neu:** Vorlagen werden jetzt als .xmlt Datei gespeichert.
* **Neu:** In den Vorlagen wird ein eigenes Kennzeichen gespeichert, das verhindert, dass Vorlagen beim Bund versehentlich als eRechnung eingebracht werden können
* Es können sowohl Vorlagen als auch ebInterface Rechnungen der Versionen 4.1, 4.0 und 3.02 als Vorlagen geladen werden. Allfällige Unterschiede werden dabei auf ebInterface 4.1 abgebildet.
## Installation
# Erstellen Sie einen Ordner mit dem Namen C:\OfficeApps\eRechnung4p1
# Laden Sie diese Version in dieses Verzeichnis herunter
# Starten Sie die heruntergeladene EXE Datei und entpacken Sie die Dateien
# Führen Sie die Installation mit Setup.exe durch
# Starten sie das Word PlugIn durch Doppelclick auf eRechnung4p1.dotx
## Bekannte Themen und Probleme
* Es ist aus technische Gründen nicht möglich, die erfassten Werte in einem Word Dokument zu speichern und wieder zu laden. Wenn Sie die Rechnung zu Archiverungszwecken speichern möchten, speichern Sie Rechnung stattdessen als PDF
* Das PlugIn für ebInterface 4.1 kann parallel zu ebInterface 4.0 in einem separaten Verzeichnis installiert und getestet werden
* Es wird immer eine eMail Adresses des Rechnungsempfängers verlangt