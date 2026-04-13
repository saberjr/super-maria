# Super Maria — Superman Platformer

A Mario-style side-scrolling platformer built with C# WinForms for a CS232 university project. You play as Superman navigating a scrolling world, fighting Doomsday enemies, climbing ladders, jumping between platforms, and firing sonic punches — all rendered with frame-by-frame sprite animation using GDI+.

---

## Gameplay

- An intro animation plays when the game starts (Superman flies in over 37 frames)
- Superman hovers and walks across a horizontally-scrolling world
- Doomsday spawns automatically and chases Superman's position in real time
- Doomsday fires enemy punches; Superman retaliates with a sonic punch (`P`)
- A hit on Doomsday triggers his 3-frame death animation and removes him from the scene
- A hit on Superman triggers his die animation
- Platforms can be dropped into the world during play (`L`)
- Ladders let Superman climb vertically; proximity is checked before allowing ladder movement

---

## Controls

| Key | Action |
|-----|--------|
| `→` Arrow Right | Move / hover right (scrolls world left) |
| `←` Arrow Left | Move / hover left (scrolls world right) |
| `K` | Jump right |
| `J` | Jump left |
| `P` | Fire sonic punch |
| `↑` Arrow Up | Climb ladder up (if near a ladder) |
| `↓` Arrow Down | Climb ladder down (if near a ladder) |
| `L` | Spawn a platform at the next position |

---

## Features

### Sprite Animation System
All animations are driven by indexed PNG filenames loaded at runtime:
- `SupermanIntro (0..37).png` — 38-frame intro fly-in sequence
- `SupermanWalkRight (0..N).png` — walk cycle
- `SupermanMoveRight / MoveLeft` — movement frames
- `SupermanHover (0..1).png` — 2-frame hover idle
- `SupermanJumpRight (0..3).png` — jump arc
- `SupermanSonicPunchRight (0..15).png` / `Left` — punch fire animation
- `SupermanDie (0..N).png` — death animation
- `DoomsdayWalkRight / Left` — enemy walk cycle
- `DoomsdayDieLeft (0..3).png` — enemy death sequence

Each animation is driven by a counter (`iintro`, `iwalkright`, `isonicpunch`, `ienemydie`, etc.) incremented in the `Timer` tick. `MakeTransparent()` strips the background color from each frame.

### Scrolling World
- The world is a `CWorld` object with a destination rect (`rcDst`) and source rect (`rcSrc`) for viewport clipping
- `ScrollRight()` / `ScrollLeft()` shift the world's draw position, creating the illusion of movement
- All platforms, ladders, enemies, and projectiles shift position relative to the scroll delta to stay in sync

### Enemy AI — Doomsday
- `CreateDoomsdayEnemy()` spawns Doomsday on a timer tick
- `EnemyFollowHero()` compares Doomsday's X position to Superman's X each tick and calls `DoomsdayMoveLeft()` or `DoomsdayMoveRight()` accordingly
- Enemy fires `CActorEnemyPunch` projectiles that travel across the screen

### Collision Detection
AABB (axis-aligned bounding box) rectangle intersection, checked every tick:
- `CheckIsHit()` — tests each `CActorSonicPunch` against Doomsday's bounding rect; on hit, triggers `DoomsdayDieAnimation()` and removes the punch
- `CheckIsHit()` also tests each `CActorEnemyPunch` against Superman's rect; on hit, triggers Superman's die sequence
- `CheckDeathDoomsday()` checks if Doomsday has walked into Superman directly (melee range)
- `RemoveDeadEnemy()` clears Doomsday from the list after death animation completes

### Physics
- `CheckGravity()` applies downward force when Superman is not on a platform
- `Fall()` handles the falling movement
- `MoveElevator()` handles vertical elevator-style movement for certain platforms
- `MoveOnLadderUp()` / `MoveOnLadderDown()` move Superman along a ladder
- `CheckLadderRadius()` verifies Superman is close enough to a ladder before enabling climb

### Rendering Pipeline
Every tick calls `DrawDubb(Graphics g)`:
1. Clears the off-screen `Bitmap off`
2. Draws world → platforms → ladders → gems → enemies → punches → hero
3. Blits the completed off-screen buffer to the form's graphics context in one call (prevents flicker)

---

## Architecture — OOP Class Design

| Class | Role |
|-------|------|
| `CWorld` | Scrolling background tile with `rcSrc` / `rcDst` viewport rects |
| `CActorHero` | Superman — position (`x`, `y`) + current frame bitmap |
| `CActorDoomsdayEnemy` | Enemy — position + current frame bitmap |
| `CActorSonicPunch` | Hero projectile — position + frame |
| `CActorEnemyPunch` | Enemy projectile — position + frame |
| `CLadder` | Climbable ladder object — position + image |
| `CPlatform` | Landable platform — position + image |

All actors are stored in `List<T>` collections. The tick handler iterates each list every frame.

---

## Project Structure

```
SuperMaria/
├── Form1.cs              # All game logic (~1050 lines)
│   ├── Actor classes     # CWorld, CActorHero, CActorDoomsdayEnemy, etc.
│   ├── Form1 (partial)   # Game loop, input, rendering, AI, physics
│   ├── Create*()         # Factory methods — spawn actors into lists
│   ├── Move*() / Scroll* # Movement and world scroll
│   ├── *Animation()      # Frame-indexed sprite sequencers
│   ├── Check*()          # Collision and gravity checks
│   └── DrawDubb()        # Double-buffered render pipeline
├── Form1.Designer.cs     # WinForms auto-generated layout code
├── Program.cs            # Entry point — Application.Run(new Form1())
├── Properties/           # AssemblyInfo, Resources, Settings
├── SuperMaria.csproj     # .NET 4.7.2 project file
└── SuperMaria.sln        # Visual Studio solution
```

Sprite PNGs are loaded at runtime from the executable's working directory (`bin/Debug/`). The `bin/` folder in this repo contains the full sprite set.

---

## How to Run

1. Open `SuperMaria.sln` in **Visual Studio 2019 or later** (Windows only)
2. Set build target to **Debug | Any CPU**
3. Build → Run (`F5`)

The game window launches maximized. Sprites load from `bin/Debug/` automatically.

> **Platform note:** WinForms and GDI+ are Windows-only. The project targets .NET Framework 4.7.2.

---

## Course Context

Built for **CS232 — Object-Oriented Programming** at MSA University (Semester 4) as the course's big project. Demonstrates: custom class design with encapsulation, dynamic `List<T>` collections for game entities, timer-driven game loops, frame-counter animation without a sprite atlas, AABB collision detection, and viewport scrolling via coordinate offsets.
