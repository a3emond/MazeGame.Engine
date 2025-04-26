# Maze Game Engine API

This API provides a lightweight backend to manage a Maze Game session, including maze generation, player movement, item pickup, sound effects, and session state management.

---

## Base URL

```
https://localhost:7223/
```

All endpoints below assume this base.

---

## Endpoints Overview

| Method | URL | Purpose |
|:---|:---|:---|
| `POST` | `/api/game/init` | Initialize maze generation with a selected algorithm |
| `POST` | `/api/game/start` | Start the game timer and session |
| `POST` | `/api/game/move` | Move player in a given direction |
| `GET` | `/api/game/state` | Get current game session state |
| `GET` | `/api/game/maze` | Get the maze grid (flattened array) |
| `GET` | `/api/game/items` | Get all placed items |
| `GET` | `/api/game/player` | Get player information |
| `GET` | `/api/game/algorithms` | Get available maze generation algorithms |
| `GET` | `/api/game/soundeffects` | Get available sound effect mappings |
| `GET` | `/api/game/music` | Get available music playlist |

---

## Testing Procedure

You can test using ThunderClient, Postman, or `curl`.

---

### 1. Initialize Maze

**Endpoint:**  
```
POST /api/game/init
```

**Body (JSON):**
```json
{
  "algorithm": 0
}
```

Where `algorithm` is the enum index:
- `0` = RecursiveBacktracking
- `1` = Prim
- `2` = Kruskal (if available)

**Example curl:**
```bash
curl.exe -X POST https://localhost:7223/api/game/init -H "Content-Type: application/json" -d "{\"algorithm\":0}" -k
```

---

### 2. Start Game

**Endpoint:**  
```
POST /api/game/start
```

**No body required.**

**Example curl:**
```bash
curl.exe -X POST https://localhost:7223/api/game/start -k
```

---

### 3. Move Player

**Endpoint:**  
```
POST /api/game/move
```

**Body (JSON):**
```json
{
  "direction": "up"
}
```
Allowed directions: `"up"`, `"down"`, `"left"`, `"right"`

**Example curl:**
```bash
curl.exe -X POST https://localhost:7223/api/game/move -H "Content-Type: application/json" -d "{\"direction\":\"up\"}" -k
```

---

### 4. Get Game Session State

**Endpoint:**  
```
GET /api/game/state
```

**Returns:**  
- Game started/over flags
- Current hearts
- Timer seconds left
- Last item effects

---

### 5. Get Maze Grid

**Endpoint:**  
```
GET /api/game/maze
```

**Returns:**
- `width`, `height`
- Flattened `grid` array (1D list of tiles)
- (Optional) Sprite map

Tiles are reconstructed on frontend with:
```javascript
const index = (y * width) + x;
const tileType = grid[index];
```

---

### 6. Get Items

**Endpoint:**  
```
GET /api/game/items
```

**Returns:**
- List of all collectible items
- Each item has `x`, `y`, `name`, and interaction flags
- (Optional) Item sprite map

Frontend should check items by matching `(x, y)` coordinates.

---

### 7. Get Player Information

**Endpoint:**  
```
GET /api/game/player
```

**Returns:**
- Player `x`, `y`
- Facing direction
- Light radius
- (Optional) Animation sprite mappings

---

### 8. Get Available Maze Algorithms

**Endpoint:**  
```
GET /api/game/algorithms
```

**Returns:**
```json
{
  "availableAlgorithms": ["RecursiveBacktracking", "Prim", "Kruskal"]
}
```

Use this list to populate algorithm selection in frontend.

---

### 9. Get Sound Effects

**Endpoint:**  
```
GET /api/game/soundeffects
```

**Returns:**
```json
{
  "soundEffects": {
    "Pickup": "pickup.wav",
    "TrapDamage": "trap_damage.wav"
  }
}
```

---

### 10. Get Music Playlist

**Endpoint:**  
```
GET /api/game/music
```

**Returns:**
```json
{
  "tracks": [
    "bright-loop.mp3",
    "mysterious-theme.mp3"
  ]
}
```

---

## Notes

- The API uses **in-memory session management** (no persistent database).
- Maze grid is **flattened into a 1D array** for JSON serialization.
- Items are sent as a **flat list** with positions.
- Session state is lost on server restart unless persistence is added.

---

## Important for Local Testing

For local testing with `curl` or ThunderClient, use the `-k` option to bypass SSL verification:
```bash
-k
```
This skips HTTPS certificate trust warnings when running locally with a development certificate.

---

