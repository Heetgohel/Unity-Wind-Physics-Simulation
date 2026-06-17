# Unity Wind Physics Simulation
### Realistic 3D Outdoor Environment with Runtime Wind Control

> Built in Unity 6 (URP) · C# · June 2026

---

## Overview

A realistic outdoor 3D environment in Unity featuring terrain, trees, grass, rocks, and a complete runtime wind simulation system. The wind affects trees, grass, falling leaves, and physics objects — and can be adjusted live during gameplay through an in-game UI panel without pausing or restarting the scene.

---

## Demo

> Screenshot of scene with wind settings UI open

![Scene Preview](screenshots/scene_preview.png)

---

## Features

- **Terrain** — sculpted terrain with multiple painted texture layers (grass, soil, mud, sand), rolling hills, and a dirt path
- **Trees & Grass** — SpeedTree-compatible assets from Unity's official Terrain Sample Assets, responding natively to WindZone
- **Runtime Wind Control UI** — in-game panel with input fields for wind speed, strength, direction, and turbulence — all applied instantly without scene restart
- **Falling Leaves** — 32 particle systems distributed across the forest at canopy height, drifting in the wind direction using world-space velocity
- **Physics Objects** — lightweight sphere responds to directional wind force using Rigidbody and continuous ForceMode
- **First-Person Explorer** — WASD movement with mouse look; ESC to unlock cursor for UI interaction, right-click to re-lock

---

## Scripts

| Script | Purpose |
|---|---|
| `DynamicWindController.cs` | Core wind manager — controls WindZone, terrain grass via TerrainData, and leaf particle systems simultaneously |
| `WindSettingsUI.cs` | UI panel manager — connects TMP input fields to wind controller at runtime |
| `WindPhysicsController.cs` | Applies directional wind force to Rigidbody objects every FixedUpdate |
| `PlayerMovement.cs` | First-person movement and camera with cursor lock toggle |

---

## How the Wind System Works

Wind is managed through three layers simultaneously:

**1. Unity WindZone**
The `WindZone` component is driven by `DynamicWindController` — its `windMain`, `windTurbulence`, `windPulseMagnitude`, and `windPulseFrequency` properties are set at runtime. The transform rotation is updated every frame to reflect the current wind direction angle.

**2. Terrain Grass**
Unity's terrain grass has its own internal wind system separate from WindZone. It is controlled directly through `TerrainData` properties:
```csharp
TerrainData td         = Terrain.activeTerrain.terrainData;
td.wavingGrassSpeed    = windSpeed;
td.wavingGrassStrength = windStrength * 0.3f;
td.wavingGrassAmount   = turbulence   * 0.5f;
```

**3. Leaf Particles**
Leaf drift direction is calculated from the wind direction angle and applied to the `velocityOverLifetime` module of each particle system:
```csharp
float dirRad   = windDirection * Mathf.Deg2Rad;
velocity.x     = new ParticleSystem.MinMaxCurve(Mathf.Cos(dirRad) * windStrength * 0.5f);
velocity.z     = new ParticleSystem.MinMaxCurve(Mathf.Sin(dirRad) * windStrength * 0.5f);
```

---

## Runtime UI

The Wind Settings panel is opened via a button in the top-right corner. All changes apply instantly:

| Field | Range | Effect |
|---|---|---|
| Speed | 0.1 – 5 | Controls sway frequency and grass wave speed |
| Strength | 0 – 10 | Controls sway intensity, WindZone force, and leaf drift |
| Direction | 0 – 360° | Rotates WindZone and shifts leaf drift vector |
| Turbulence | 0 – 2 | Adds irregular wobble to wind movement |

---

## Controls

| Input | Action |
|---|---|
| W A S D | Move |
| Mouse | Look around (when locked) |
| Escape | Unlock cursor / access UI |
| Right Click | Re-lock cursor / resume camera |
| Wind Settings button | Open / close wind panel |
| Input field + Enter | Apply wind value instantly |

---

## Assets Used

| Asset | Source |
|---|---|
| Terrain Sample Assets (trees, grass, rocks, textures) | Unity Technologies — Asset Store (Free) |
| TextMeshPro | Unity Built-in Package |
| Unity Input System | Unity Built-in Package |
| All C# scripts | Written from scratch |

---

## Technical Details

- **Engine:** Unity 6 (6000.x) — Universal Render Pipeline (URP)
- **Platform:** Windows (PC)
- **Input:** Unity Input System (new)
- **Physics:** Unity 3D Physics (Rigidbody, CharacterController)
- **Hardware tested on:** MacBook Air M4

---

## Author

**Heet Gohel**
B.Tech AI & Data Science Graduate — ADIT, 2026

[![LinkedIn](https://img.shields.io/badge/LinkedIn-heet--gohel-blue)](https://www.linkedin.com/in/heet-gohel-531670251)
[![itch.io](https://img.shields.io/badge/itch.io-qu1x0try-red)](https://itch.io/profile/qu1x0try)
[![GitHub](https://img.shields.io/badge/GitHub-HeetGohel-black)](https://github.com/HeetGohel)
