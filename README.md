# Super Maria — Superman Platformer

A Mario-style side-scrolling platformer built with C# WinForms for a university CS232 course. You play as Superman fighting Doomsday across a scrolling world with platforms, ladders, and animated sprite sheets.

## Gameplay

- Control Superman through a scrolling platformer world
- Fight Doomsday enemy that follows and attacks the hero
- Collect blue gems scattered across the level
- Batman appears as a background character
- Die animation triggers on enemy hit; Doomsday has his own death sequence

## Controls

| Key | Action |
|-----|--------|
| Arrow Right | Walk / hover right |
| Arrow Left | Walk / hover left |
| Arrow Up | Jump right |
| Arrow Down | Jump left |
| Space | Sonic punch |

## Features

- **Sprite animation** — frame-by-frame animation via indexed PNG sprite sequences for: intro, walk (left/right), hover (left/right), jump, sonic punch (left/right), die, Doomsday walk + die
- **Scrolling world** — viewport scrolls with the hero; world tiles use `rcSrc`/`rcDst` for clipped rendering
- **Enemy AI** — Doomsday spawns on a timer and follows the hero's position
- **Collision detection** — hit checks between hero, enemy punches, sonic punches, and Doomsday
- **Double buffering** — off-screen `Bitmap` (`DrawDubb`) prevents flicker
- **Platforms & ladders** — rendered as game objects with position tracking

## Tech Stack

| | |
|---|---|
| Language | C# |
| Framework | .NET Framework 4.7.2 |
| UI | Windows Forms (WinForms) |
| Rendering | GDI+ (`System.Drawing`) |
| Animation | `Timer` at 1ms tick + frame counters |

## Project Structure

```
SuperMaria/
├── Form1.cs              # All game logic — actors, animation, input, collision
├── Form1.Designer.cs     # WinForms designer-generated layout
├── Program.cs            # Entry point
├── Properties/           # Assembly info, resources
├── SuperMaria.csproj     # Project file
└── SuperMaria.sln        # Solution file
```

Sprite assets (PNG sequences) are loaded at runtime from the executable's directory.

## How to Run

1. Open `SuperMaria.sln` in **Visual Studio** (Windows)
2. Place sprite PNG files in the build output folder (`bin/Debug/`) — see existing folder for reference
3. Build → Run (`F5`)

> Requires Windows — WinForms is not cross-platform.

## Course Context

Built for **CS232 — Object-Oriented Programming** as a semester big project demonstrating OOP principles: class design (`CActorHero`, `CActorDoomsdayEnemy`, `CPlatform`, etc.), encapsulation, and game loop architecture using Windows Forms timers.
