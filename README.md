# Multikill Server (for Ultrakill 0.9.0)

---

# Why even make this when Jaket exists?
- **Jaket does not Support dedicated 24/7 lobbies** since it uses the Steamworks API.
- **Multikill aims to be as customizable as possible** meaning that the Server might be able to give the client instructions for custom Physics.
- **Map Streaming** I want Multikill to be able to Stream maps from the Server / download them when joining a game, so you don't have to worry about not having the right map installed.
- **Educational Purposes** Multikill is a project for Educational Purposes.
- **i don't even know what i'm yapping about**

---
The **Multikill Server** is a dedicated server for the **Multikill** mod for **Ultrakill version 0.9.0**, allowing players to connect and engage in **PvP combat** across different maps. The server is responsible for handling client connections, managing player synchronization, and broadcasting data to all connected players.

## Features
- **Dedicated Server**: Host multiplayer games with multiple players.
- **Basic Movement Sync**: Sync player positions and basic movements across all connected clients.
- **Map Syncing**: The server sets the map, and it is replicated across all connected clients.
- **LiteNetLib Networking**: Uses **LiteNetLib** for reliable and efficient network communication.

> **Note**: The current version only includes **basic movement syncing** and **map syncing**. Additional features like advanced combat mechanics, PvP functionality, and team-based modes are planned for future updates.

---

## Requirements
- **.NET Framework 4.7.2 or higher** (for the server application)
- **LiteNetLib** (for handling client connections)

---

## Installation

### 1. Download and Build the Server
1. Clone or download the **Multikill Server** repository.
2. Download the **latest stable version** of the **Multikill Server** executable from the [server release page](https://github.com/ApfelTeeSaft/Multikill-Server/releases).
3. Open the project in **Visual Studio 2022** or any compatible IDE if building from source.
4. Ensure **LiteNetLib** is installed via NuGet:
   - Right-click the project > **Manage NuGet Packages** > Search for **LiteNetLib** and install the latest version.

### 2. Run the Server
1. After compiling or downloading the server, run the server executable.
2. The server will listen on port `9050` by default. You can change this by modifying the code or providing a different port when launching.
3. The server console will log connected clients, disconnected clients, and data being sent/received.

---

## Server Configuration

The server settings can be configured via code or by adding configuration options in future updates:
- **Port**: The default port is `9050`, but this can be changed in the source code.
- **Max Players**: You can set the maximum number of players in the server code.
- **Map Management**: Load different maps for PvP combat (future feature).

---

## Server Commands

The server is controlled via the console:
- **ESC**: Press ESC to safely shut down the server.
- **Logs**: Monitor connections, disconnections, and game events via the server logs.

---

## Contributing
The **Multikill Server** project is open for contributions! You can help by reporting bugs, suggesting features, or submitting code improvements.

To contribute:
1. Fork the **server repository**.
2. Make your changes and submit a pull request.

---


# Contact and Support

For any questions, feedback, or support:
- **Issues**: Use the GitHub issue tracker to report bugs and request features for both the **client** and **server**.
- **Community**: Join our community (Discord server to be provided) for matchmaking, support, and discussions.

---

# Future Roadmap

The **Multikill** project is actively being developed to bring additional features:
- **Team Modes**: Play with others in team-based game modes like **Team Deathmatch**.
- **Map Customization**: Allow for custom maps and rotations during gameplay.
- **Advanced Synchronization**: Improve latency handling and player action synchronization for a seamless experience.
- **Leaderboards and Stats**: Track player performance across games and show competitive leaderboards.
