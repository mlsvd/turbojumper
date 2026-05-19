# Changelog

All notable changes to TurboJumper are documented here.

---

## [Unreleased]

### Added
- **Process whitelist** — the app now shows only configured processes instead of all running processes. Configuration is stored locally at `%APPDATA%\TurboJumper\process-config.json` (per-user, no network, excluded from version control).
- **First-run defaults** — on first launch with no config file, a default whitelist is created covering the most common apps: Chrome, Edge, Firefox, VS Code, Slack, Teams, Spotify, Visual Studio, Discord, Explorer, Notepad.
- **Ordering** — each configured process has an `Order` field; buttons are rendered in that order.
- **Configure button** — a toolbar at the top of the main window exposes a **Configure** button.
- **Configure dialog** — a grid-based dialog for managing the whitelist:
  - Enable/disable individual entries without removing them.
  - Set display name, hot key (dropdown of all 36 keys), and render order per process.
  - **Add Row** — add a blank entry manually.
  - **Add Running...** — pick from a list of currently running processes not already in the config.
  - **Remove Selected** — delete a row.
  - Hot-key conflict validation on save (two enabled entries cannot share the same key).
- `ProcessKeyboardShortcutProvider.ProvideForHotKey(string)` — looks up display label for a given key code.
- `ProcessKeyboardShortcutProvider.GetKeyMap()` — exposes the key map for external consumers.

### Changed
- `ProcessListPresenter` now targets a `Control` (scrollable `Panel`) instead of the `Form` directly, enabling `AutoScroll` on the process list area.
- `ProcessListPresenter` clears its container before each render, fixing duplicate buttons on Space-refresh.
- `IProcessListPresenter.Present` signature changed from `Form Present(Form, ...)` to `void Present(Control, ...)`.
- `ProcessListKeyCombinationDecorator` uses the configured hot key when one is set on a `ProcessWrapper`; falls back to positional assignment otherwise.
- `DecoratedProcessProvider` decorator chain updated: `MainProcessFilter` → **`WhitelistFilter`** → `IconDecorator` → `KeyCombinationDecorator`.
- `MainForm` rebuilt with a docked toolbar panel and a fill-docked, auto-scrolling process list panel.
- `FormViewCoordinatesFactory` dependency removed from `ProcessListPresenter` (no longer needed).

### Fixed
- Pressing Space to refresh the process list no longer appended duplicate buttons to the form.

---

## [0.2.0] — 2025 (upgrade to .NET 10)

### Changed
- Target framework upgraded from `net8.0-windows` to `net10.0-windows`.
- All `Microsoft.Extensions.*` packages updated to version `10.0.0`.

---

## [0.1.0] — 2025 (initial release)

### Added
- Core window-switching UI: all running processes displayed as clickable `ProcessButton` controls inside `GroupBox` cells.
- Decorator pipeline: `MainProcessFilter` (visible-window processes only) → `IconDecorator` (executable icon extraction) → `KeyCombinationDecorator` (keyboard shortcut assignment and registration).
- Keyboard shortcuts assigned positionally across rows `1–9`, `Q–P`, `A–L`, `Z–M`; active only while TurboJumper has focus.
- Space bar refreshes the process list.
- `SwitchToProcessHandler` — P/Invoke into `user32.dll` (`ShowWindow` + `SetForegroundWindow`) handles both normal and minimised windows.
- `ProcessButton` custom control — paints an icon/text separator line and a secondary hotkey label at the bottom of each button.
- `ShortcutConfig` model — carries `HotKey` (key code string) and `DisplayName` (human-readable label).
- DI wiring via `Microsoft.Extensions.DependencyInjection`; layout configured through `appsettings.json`.
- Application icon (`app.ico`) and settings icon (`settings.png`).
