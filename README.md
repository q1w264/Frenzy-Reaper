# Frenzy Reaper

![Unity](https://img.shields.io/badge/Unity-2D%20LTS-black?logo=unity)
![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp)
![Genre](https://img.shields.io/badge/Genre-Roguelite%20%2F%20Survivors--like-orange)
![Status](https://img.shields.io/badge/Status-Phase%202%20Roguelite%20Loop-orange)
![License](https://img.shields.io/badge/Code%20License-GPL--3.0-success)

[中文文档 (Chinese)](./README.zh-CN.md)

## About the Project

Frenzy Reaper is a 2D pixel-art wasteland roguelite with a Vampire Survivors-like combat loop.  
You survive against massive mutant swarms, collect scrap from ruined zones, and evolve through cybernetic and genetic upgrades.  
Each run is a harsh expedition: stay alive, build your loadout, and bring resources back to the shelter.

## Key Features

- **Auto-fire wasteland weapons**: focus on movement, positioning, and survival choices.
- **Massive on-screen mutant swarms**: high-density combat with Survivors-like pacing.
- **Cybernetic / genetic progression**: run-based upgrades that support multiple builds.
- **Scrap-driven loop**: collect mechanical scrap for in-run and meta progression.
- **Shelter development (planned)**: convert run rewards into long-term growth.

## Roadmap

- [x] Phase 0: Project initialization and whitebox environment setup
- [x] Phase 1: Core combat prototype (movement, auto-fire, enemy chase, basic HP)
- [ ] **Phase 2: Roguelite loop (scrap pickup, upgrade UI, dynamic enemy waves)** ← *in progress*
- [ ] Phase 3: Architecture refactor (Object Pool, ScriptableObject data-driven config)
- [ ] Phase 4: Content and feedback pass (more wasteland weapons, hit flash, screen shake)
- [ ] Phase 5: Meta progression (shelter building, local JSON save/load)

## Tech Stack & Architecture

- **Engine**: Unity 2D (LTS line), C#
- **Code quality**: modular structure and clean-code practices
- **Performance**: planned/ongoing use of **Object Pooling** for large entity counts
- **Data-driven design**: **ScriptableObject** for config/logic separation
- **Pixel rendering**: **2D Pixel Perfect** to preserve crisp visuals

> Current repository status: **Phase 2 — Roguelite Loop** (active development).

## Changelog

### Phase 1 — Core Combat Prototype ✅
- Player 8-directional movement via Unity Input System.
- Auto-targeting: `Attack.cs` uses `Physics2D.OverlapCircle` to lock onto the nearest live enemy each frame.
- Bullet object pool (`BulletPool.cs` + `Bullet.cs`) recycles projectiles with trigger-based hit detection.
- Enemy chase and melee AI powered by **A\* Pathfinding Project** (AIPath + RVO) with a configurable decision interval to reduce CPU cost.
- Component-level HP system (`Health.cs`) with `OnDamaged` / `OnDeath` C# events; UI data-binding via Unity UI Toolkit runtime data source.
- Death screen with scene-reload via `DeathController.cs`.

## How to Play / Build

1. Clone the repository:
   ```bash
   git clone https://github.com/q1w264/Frenzy-Reaper.git
   ```
2. Open the project with Unity Hub (use the project's Unity LTS version).
3. Open scene `Assets/Scenes/Gaming.unity`.
4. Enter Play Mode.

> Note: `Assets/Art Resources` and related art files are not included in this repository for compliance reasons.  
> You must provide your own legally licensed replacement assets for full visual testing.

> Note: this project also uses a proprietary paid module (`A* Pathfinding Project`) that is not bundled with the public repository.  
> If you need that functionality, install it locally under your own valid license.

## License

This repository follows a split licensing model:

- **Code (Scripts / Config / Project Settings)**: `GPL-3.0` (see `LICENSE`)
- **Art and audio assets (Sprites / Audio)**: copyright reserved by their respective owners; no unauthorized commercial use or redistribution.

For third-party notices and Unity-related package references, see `ThirdParty/THIRD_PARTY_LICENSES.md`.
