# Maze Game Backend

This project is the backend logic for the Maze Game, implemented in C#.  
It is fully modular, frontend-agnostic, and manages maze generation, game state, player logic, item pickups, sound mapping, and session persistence.

---

## Project Structure

```
API/
├── DTO/
│   ├── GameLoadDTO.cs
│   ├── GameSessionDTO.cs
│   ├── ItemDTO.cs
│   ├── MazeAlgorithmListDTO.cs
│   ├── MusicPlaylistDTO.cs
│   └── SoundEffectMapDTO.cs
├── GameState.cs
├── MazeGameCore.cs
└── MazeGameService.cs

GameEngine/
├── GeneratingAlgorithms/
│   ├── MazeAlgorithmBSP.cs
│   ├── MazeAlgorithmDrunkardsWalk.cs
│   ├── MazeAlgorithmPrims.cs
│   ├── MazeAlgorithmType.cs
│   └── RecursiveBacktrackingMaze.cs
├── Models/
│   ├── Item/
│   │   ├── Item.cs
│   │   ├── ItemEffect.cs
│   │   ├── ItemEffectResult.cs
│   │   ├── ItemGrid.cs
│   │   ├── ItemName.cs
│   │   └── ItemSpriteResolver.cs
│   ├── Maze/
│   │   ├── Maze.cs
│   │   ├── MazeRendererType.cs
│   │   ├── TileSpriteResolver.cs
│   │   └── TileType.cs
│   ├── Others/
│   │   ├── AudioTracks.cs
│   │   ├── FogOfWar.cs
│   │   └── LightSource.cs
│   └── Player/
│       ├── Player.cs
│       └── PlayerSpriteResolver.cs
├── Services/
│   ├── MazeGenerator.cs
│   ├── MazeItemSpawner.cs
│   ├── MusicPlaylistService.cs
│   ├── PlayerInteractionService.cs
│   ├── PlayerMovementService.cs
│   ├── SoundEffectService.cs
│   └── TileProcessor.cs
├── Utils/
│   ├── ItemPlacementUtils.cs
│   ├── MazeAnalysis.cs
│   └── MazeBuilderUtils.cs
└── MazeConfig.cs
```

---

## Data Transfer Objects (DTOs)

- `GameLoadDTO`: Full maze, items, player animations, and startup configuration
- `GameSessionDTO`: Partial live session state for saving and restoring mid-game
- `ItemDTO`: Flat representation of placed items
- `MazeAlgorithmListDTO`: Available maze generation algorithms
- `MusicPlaylistDTO`: List of music tracks available for in-game background
- `SoundEffectMapDTO`: Mapping of game events to sound effect audio paths

---

## Core Logic

- `MazeGameCore`: Manages maze generation, player movement, item pickups, win/lose conditions
- `MazeGameService`: API layer for exposing clean DTOs to the frontend
- `GameState`: The in-memory persistent structure for all session data

---

## Gameplay Flow

1. Maze Generation: Randomized maze built server-side
2. Player Spawn: Start position initialized
3. Player Movement: Movement validation, walkability check, item pickups
4. Game Progression: Pickup effects applied (heal, damage, unlock goal)
5. Session Save/Restore: Game state can be serialized and restored by frontend periodically

---

## Maze System

- Multiple procedural algorithms available
- Tile types mapped directly to frontend-renderable sprite paths
- Walkability handled at tile level
- Dynamic light radius for player visibility

---

## Audio System

- `MusicPlaylistService` provides shuffled music playlists
- `SoundEffectService` provides sound mappings for heal, damage, teleport, and other events

---

## Highlights

- Clean modular structure
- Optimized session handling for smooth reloading and recovery
- Minimal data transmission (full maze sent once, session states afterward)
- Frontend-agnostic API design, can support WebGL, Canvas2D, or Unity frontends
