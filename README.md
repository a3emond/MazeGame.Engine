# Maze Game Engine API

This API provides a lightweight backend to manage a Maze Game session, including maze generation, player movement, item pickup, sound effects, music, and session state management.

---

## Base URL

```
https://localhost:7223/
```

All endpoints below assume this base URL.

---

## Endpoints Overview

| Method | URL | Purpose |
|:---|:---|:---|
| `POST` | `/api/game/load` | Initialize maze generation, start the game, and return full GameLoadDTO |
| `GET` | `/api/game/state` | Get current game session state (GameSessionDTO) |
| `POST` | `/api/game/state/save` | Save current frontend session (GameSessionDTO) |
| `GET` | `/api/game/algorithms` | Get available maze generation algorithms |
| `GET` | `/api/game/soundeffects` | Get available sound effect mappings |
| `GET` | `/api/game/music` | Get available music playlist |

---

## Testing Procedure

You can test using ThunderClient, Postman, Swagger UI, or `curl`.

---

### 1. Load Game

**Endpoint:**
```
POST /api/game/load
```

**Body (JSON) Optional:**
```json
{
  "algorithm": "RecursiveBacktracking"
}
```
If omitted, defaults to `RecursiveBacktracking`.

**Example curl:**
```bash
curl.exe -X POST https://localhost:7223/api/game/load -H "Content-Type: application/json" -d "{\"algorithm\":\"Prim\"}" -k
```

---

### 2. Get Current Session State

**Endpoint:**
```
GET /api/game/state
```

**Returns:**
- Game started / running / over flags
- Player position
- Health, inventory, goal status
- Timer seconds left

**Example curl:**
```bash
curl.exe -X GET https://localhost:7223/api/game/state -k
```

---

### 3. Save Updated Session State

**Endpoint:**
```
POST /api/game/state/save
```

**Body (JSON):** (example after player moves)
```json
{
  "gameStarted": true,
  "gameRunning": true,
  "gameOver": false,
  "mazeInitialized": true,
  "goalUnlocked": false,
  "maxHearts": 5,
  "currentHearts": 2.5,
  "inventorySlots": ["", "", ""],
  "statusEffect": null,
  "playerX": 5,
  "playerY": 3,
  "lastMoveDirection": "down",
  "animationFrame": 2,
  "timerSecondsLeft": 180,
  "lastItemEffect": null
}
```

**Example curl:**
```bash
curl.exe -X POST https://localhost:7223/api/game/state/save -H "Content-Type: application/json" -d "{...}" -k
```

---

### 4. Get Available Maze Algorithms

**Endpoint:**
```
GET /api/game/algorithms
```

**Returns:**
```json
{
  "availableAlgorithms": ["RecursiveBacktracking", "Prims", "BSP", "DrunkardsWalk"]
}
```

---

### 5. Get Sound Effects Mapping

**Endpoint:**
```
GET /api/game/soundeffects
```

**Returns:**
```json
{
  "soundEffects": {
    "Heal": "/assets/audio/potion_pickup.wav",
    "PowerUp": "/assets/audio/powerup.wav",
    "Teleport": "/assets/audio/teleport.wav",
    "TrapDamage": "/assets/audio/trap_damage.wav",
    "Win": "/assets/audio/win.wav",
    "GameOver": "/assets/audio/gameover.wav"
  }
}
```

---

### 6. Get Music Playlist

**Endpoint:**
```
GET /api/game/music
```

**Returns:**
```json
{
  "tracks": [
    "bright-electronic-loop-251871.mp3",
    "cool-hip-hop-loop-275527.mp3",
    "epic-hybrid-logo-157092.mp3"
  ]
}
```

---

## Notes

- Full maze grid and items are sent once during `/load`.
- No database storage: session is held in server memory.
- Timer logic is handled by frontend after load.
- Audio paths returned are ready for frontend preloading.

---

## Local Testing Tip

Use the `-k` option in `curl` to ignore SSL warnings when testing on localhost:
```bash
-k
```

---

## Quick Frontend Tips

- Maze tiles are 1D: Access with `index = (y * width) + x`.
- Items are a flat list: Filter or match by `item.x` and `item.y`.
- Player sprite frames rotate on movement client-side.
- Light radius, fog-of-war and minimap are handled client-side.

---

Ready to connect to WebGL or Canvas2D based rendering engines!
