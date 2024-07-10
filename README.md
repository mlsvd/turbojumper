# Turbo Jumper
-------------
Turbo Jumper is a lightweight Windows application designed to optimize the process of switching between multiple applications and to make it quickly and efficiently as possible. Turbo Jumper provides an intuitive interface that enhances productivity for developers and users alike. Its primary goal is to enable seamless switching between open application windows without relying on Alt+Tab or Win+Tab.


## Features
- **Process Button Interface:** Upon launch, Turbo Jumper loads system processes and displays them as buttons in the main window form.
- **Quick Switching:** Clicking on a process button instantly switches focus to the target application.
- **Keyboard Shortcuts:** Each process button is assigned a shortcut key (e.g., 0-9, qwertyasd etc) for rapid switching using the keyboard. Shortcuts will work when Turbo Jumper window is active and focused, and when not, actions for shortcuts will be omitted. This is intended to not interfere with other shortcuts configured by system or other applications.
- **Enhanced Accessibility:** Ideal for developers and users who frequently switch between multiple applications, offering a faster alternative to Alt+Tab or Win+Tab.

## Recommended usage
1. Launch the Application
2. Position Turbo Jumper on the Taskbar
    - Pin Turbo Jumper to the leftmost position on the taskbar for quick access using Win+1 (or Win+{position}).
3. Switching Applications
    - Activate Turbo Jumper using the assigned shortcut (Win+1, etc.).
    - Click on the button representing the target application's process window to switch to it.
    - Alternatively, use the shortcut key displayed at the bottom of the application's button.
4. Refreshing Process List
    - To update the list of processes (e.g., when new apps are opened or closed):
        -- Press the [Space] key when the Turbo Jumper window is active and focused.
        -- Alternatively, reload the Turbo Jumper application.

![Turbo Jumper showcase](https://github.com/mlsvd/turbojumper/releases/download/v0.0.1/turbo_jumper_showcase01.gif)

## Installation
### Build
1. Clone the Repository: git clone https://github.com/mlsvd/turbojumper
2. Build and Run: Open the solution in Visual Studio, build the application, and launch it. Turbo Jumper is now ready to simplify your application-switching experience.

### Download prebuilt version


## Contributing
Contributions to Turbo Jumper are welcome! If you have suggestions, bug reports, or would like to add new features, please feel free to open an issue or submit a pull request on GitHub.

# License
This project is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for details.

