# 🧩 Maze Game Backend

This is the backend logic core for the **Maze Game**, fully implemented in C# and built with modularity and API readiness in mind. It is **renderer-agnostic** and manages all state, rules, item logic, maze generation, and session lifecycle internally.

---

## 🗂️ Project Structure

```
API/
├── DTO/
│   ├── GameSessionDTO.cs
│   ├── GameStateDTO.cs
│   ├── ItemDTO.cs
│   ├── ItemGridDTO.cs
│   ├── MazeAlgorithmListDTO.cs
│   ├── MazeGridDTO.cs
│   ├── MusicPlaylistDTO.cs
│   ├── PlayerDTO.cs
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

## 📤 Data Transfer Objects (DTOs)

- **GameSessionDTO**: Full snapshot of session state
- **GameStateDTO**: Lightweight state for frame sync (position, status, effects)
- **MazeGridDTO**: Sprite-based or raw grid of the maze
- **CompressedMazeGridDTO**: Variant using integer tiles + map
- **ItemGridDTO**: Includes all map item instances + optional sprite mapping
- **PlayerDTO**: Direction, position, vision radius, optional animation map
- **SoundEffectMapDTO** & **MusicPlaylistDTO**: Audio mappings for frontend

---

## 🧠 Core Logic

- `MazeGameCore`: Orchestrates player actions, win/loss logic, and core flow.
- `MazeGameService`: Clean, controller-friendly entry point for exposing core logic.
- `GameState`: The full in-memory game session state.

---

## 🔁 Gameplay Flow

1. **Maze Generation**: Randomized + item spawning
2. **Player Start**: Game begins on `StartGame()`
3. **Movement / Action**: Server-side player logic + item pickup effects
4. **Timer & Status**: Win/loss determined by goal or hearts/time
5. **State Exposure**: DTOs can be sent as-is to frontend

---

## 🧩 Maze System

- Multiple generation algorithms supported via `MazeAlgorithmType`
- Processed tile types for pixel-perfect rendering
- Centralized `TileProcessor` for all structure logic

---

## 🔊 Audio System

- `AudioTracks.cs` lists background tracks and SFX
- Two services (`MusicPlaylistService` and `SoundEffectService`) expose frontend-ready maps

---

## ✨ Highlights

- Clean separation of concerns
- Fully testable backend game engine
- Easily integratable with any rendering frontend (WebGL, Canvas2D, Unity, etc.)
- Minimal state transfer, optimized for realtime gameplay