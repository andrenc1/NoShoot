# NoShoot

A 2D survivor-like game made in Unity, inspired by *Vampire Survivors*. You don't aim or shoot: your weapons attack on their own. Move to dodge the slimes that surround you, collect XP, level up and pick upgrades. Last 10 minutes to win.

<!-- Add a gameplay GIF here: ![Gameplay](docs/gameplay.gif) -->

## Gameplay

- **Auto-attacking weapons**: a sword and a thrown axe, each with its own damage and cooldown.
- **Endless enemies**: slimes spawn around the player and chase them, faster as time goes on.
- **XP and level-ups**: defeated enemies drop XP orbs that fly to the player when close.
- **Upgrades**: each level-up pauses the game and offers 3 random upgrades (weapon damage, weapon cooldown, max HP, healing, XP gain).
- **Win or lose**: survive the 10-minute timer to win, or run out of HP and it's game over. Both show a results screen.

### Score

```
score = enemies defeated × 10 + seconds survived × 1
```

Hovering the **?** icon on the results screen shows the rule and the breakdown for the current run.

## Controls

| Action | Keyboard | Gamepad |
|---|---|---|
| Move | WASD / Arrow keys | Left stick |

Attacks are automatic.

## Tech

- **Unity 6** (6000.3.9f1) with **URP 2D**
- **C#**
- **Input System** for movement
- **Unity Behavior** (behavior graph) for enemy AI
- **ScriptableObjects** for weapons, upgrades and spawn settings, so new content is data, not code
- **uGUI + TextMeshPro** for the HUD and menus

## How to run

1. Install **Unity 6000.3.9f1** through Unity Hub.
2. Clone the repository. It uses Git LFS, so install [Git LFS](https://git-lfs.com) first:
   ```bash
   git lfs install
   git clone https://github.com/andrenc1/NoShoot.git
   ```
3. In Unity Hub, click **Add → Add project from disk** and select the `NoShoot` folder.
4. Open `Assets/Scenes/SampleScene.unity` and press **Play**.

## Project structure

```
Assets/
├── Behavior/        # Enemy behavior graph and the custom "Seek Player" action
├── Prefabs/         # Player, enemies, XP orb, effects, UI prefabs
├── Scenes/          # SampleScene (the game)
└── Scripts/
    ├── GameManager.cs   # Match timer, kill count, score, win/lose, restart
    ├── Enemy/           # Health, contact damage, spawner, spawn zone
    ├── Itens/           # XP orb
    ├── Player/          # Movement, health, XP/levels, weapon controller
    ├── UI/              # Damage numbers, score tooltip
    ├── Upgrade/         # Upgrade data (ScriptableObject), cards, upgrade system
    └── Weapons/         # Weapon data, attack area, axe projectile, damage dealer
```

### How the main systems connect

```
WeaponController ──(every cooldown)──► attack prefab ──hit──► EnemyHealth
                                                                  │ dies
                                                                  ▼
UpgradeSystem ◄──(level up)── PlayerXP ◄──── XP orb ◄──── drops XP orb
     │                                                    + GameManager kill count
     └─ applies the chosen upgrade to weapons, health or XP gain

EnemyDamage ──(contact)──► PlayerHealth ──(HP = 0)──► GameManager.GameOver()
GameManager ──(timer = 10:00)──► win
```

## Roadmap

- [ ] Online leaderboard: ASP.NET Core API + PostgreSQL, with server-side checks against impossible scores
- [ ] WebGL build playable on itch.io
- [ ] More weapons and enemy types
- [ ] Difficulty that scales the number of enemies per wave over time

## About this project

The project started from the **"Crie seus Jogos"** course by **CSJ Digital**. The course provided the art assets and the base of the game: camera, map, player movement, enemy AI with a behavior tree, the modular combat system, enemy spawning, health, XP and level-ups, the match timer, the upgrade system and a basic game over.

Everything below was built on my own after the course:

- **Bug fixes**: upgrade cards froze the game when clicked; the enemy hit flash restarted incorrectly on fast consecutive hits; percentage upgrades were configured as whole numbers (x11 damage instead of +10%).
- **Score system**: `kills × 10 + seconds`, with the weights configurable in the Inspector.
- **Victory**: surviving the full timer now ends the match with a win screen. Before, it only logged to the console and could block the game over screen.
- **Single end-of-match flow** shared by win and loss, guarded against running twice.
- **Results screen** with time, level, kills and score.
- **Score tooltip**: hovering the **?** icon explains the rule and shows the calculation for the current run.
- **Responsive UI** that scales with screen size.

## Credits

- Base course and art assets: **"Crie seus Jogos"** by CSJ Digital
- Additional code and features: [André Noro Crivellenti](https://github.com/andrenc1)
