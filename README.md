# WinSecurityGuard 🛡️

**WinSecurityGuard** è un'applicazione desktop leggera per Windows che monitora in tempo reale i log di sicurezza del sistema per rilevare potenziali minacce e anomalie.

L'applicazione risiede nella System Tray e avvisa l'utente tramite popup visivi e sonori quando viene rilevato un evento sospetto.

## 🎯 Scopo del Progetto
L'obiettivo è fornire uno strumento semplice ed immediato per aumentare la consapevolezza sulla sicurezza del proprio PC, rendendo comprensibili anche ai non esperti gli eventi criptici di Windows.

Anomalie rilevate attualmente:
- **Tentativi di Accesso Falliti** (Event ID 4625): Possibili attacchi brute-force.
- **Creazione di Nuovi Utenti** (Event ID 4720): Possibile backdoor installata.
- **Cancellazione dei Log** (Event ID 1102): Tentativo di coprire le tracce.
- **Modifica Gruppo Admin** (Event ID 4732): Scalata di privilegi.

## 🏗️ Architettura
Il progetto è realizzato in **C# .NET 8** utilizzando **WPF (Windows Presentation Foundation)**.

### Componenti Principali:
1.  **SecurityMonitor Service**:
    - Un servizio in background che si aggancia al `System.Diagnostics.EventLog("Security")`.
    - Filtra gli eventi in arrivo in base agli ID specificati.
    - Traduce i messaggi tecnici in un linguaggio naturale e semplificato (Italiano).

2.  **System Tray Integrator**:
    - L'applicazione non ha una finestra principale sempre visibile.
    - Utilizza `NotifyIcon` (WinForms bridge) per gestire l'icona nell'area di notifica e il menu contestuale.

3.  **Sistema di Notifica**:
    - **Popup**: Finestre WPF "TopMost" personalizzate che appaiono in basso a destra.
    - **Suono**: Alert di sistema critico.
    - **Storico**: Una finestra dedicata che raccoglie la lista degli eventi rilevati durante la sessione.

## 🚀 Come Funziona
1.  **Avvio**: L'applicazione deve essere eseguita come **Amministratore** per poter leggere i Security Log di Windows.
2.  **Monitoraggio**: Una volta avviata, l'icona a scudo 🛡️ appare nella tray. Il monitoraggio è attivo e silenzioso.
3.  **Rilevamento**:
    - Quando Windows registra un evento "sospetto" (es. sbagli la password di login), l'app lo intercetta istantaneamente.
    - Viene mostrato un popup rosso con la descrizione dell'accaduto.
4.  **Verifica**:
    - Cliccando l'icona o usando il menu, si apre lo "Storico Eventi" per rivedere le segnalazioni passate.

### 🧪 Test (Simulazione)
Per verificare il funzionamento senza dover causare veri problemi di sicurezza:
1.  Clicca col **Tasto Destro** sull'icona nella barra delle applicazioni.
2.  Seleziona **"Simula Attacco (Test)"**.
3.  L'applicazione genererà un evento falso per testare il sistema di allerta.

## 🔧 Requisiti
- Windows 10 o 11.
- .NET Desktop Runtime 8.0.
- Privilegi di Amministratore.

---
*Developed with ❤️ by Perix & Antigravity AI*
