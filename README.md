# TurboJumper

Fast application switcher for Windows. Instead of Alt+Tab cycling, TurboJumper shows your configured apps as buttons, click one or press its shortcut key to activate it.

## Why this app?

Switching between apps in Windows has always been a bit awkward. Alt+Tab cycles through all opened apps. So in order to open a specific app you have to keep pressing Alt+Tab until you land on the target app. Win+Tab shows a visual overview, but when you have many windows open the previews shrink and start looking nearly identical, especially for text editors, ides, terminals and consoles.

TurboJumper takes a different approach: each app gets a dedicated (configurable) key. Press it once and you're there, no cycling, no hunting through thumbnails.

## Who is it for?

Mainly developers and anyone who works with a fixed set of apps and switches between them constantly, especially on a single monitor or a laptop. The recommended workflow:

- Pin TurboJumper to the **first position** on the taskbar
- Press **Win+1** from anywhere to open it
- Press the key assigned to the app you want

Two keystrokes, zero hunting. The app only shows the processes you care about, so the list stays short and the keys are easy to remember.

## How it works

On launch, TurboJumper shows buttons only for the apps you've whitelisted. Each button gets a keyboard shortcut (1–9, Q–P, A–L, Z–M). Shortcuts are active only while TurboJumper is focused, so they don't interfere with anything else.

**Space** refreshes the list when you open new apps.

![TurboJumper preview](preview.png)

## Configure

Click **Configure** to manage which apps appear and in what order. You can set a custom key per app, enable/disable entries individually, or pick from currently running processes to add them quickly.

Config is saved to `%APPDATA%\TurboJumper\process-config.json`. On first run a default list is created (Chrome, Edge, Firefox, VS Code, Slack, Teams, Spotify, Visual Studio, Discord, Explorer, Notepad).

## Build & run

```
git clone https://github.com/mlsvd/turbojumper
```

Open `TurboJumper.sln` in Visual Studio and press F5, or:

```
dotnet run --project TurboJumper/TurboJumper.csproj
```

## License

MIT — see [LICENSE](LICENSE.md).
