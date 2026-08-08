# 2D Runner

Unity 2D 기반의 Endless Runner 게임

플레이어가 장애물을 피하며 최대한 높은 점수를 획득하는
2D 러닝 게임입니다.

## 🎮 Gameplay

[게임 플레이 GIF]

## 🛠 Tech Stack

- Unity
- C#
- Unity 2D Physics
- ScriptableObject
- Git / GitHub

## ✨ Features

- 2D Player Movement
- Double Jump
- Player Health System
- Dynamic Obstacle Spawning
- Character Selection
- Character Data Management
- Score System
- Game Over / Lobby Transition
- Sound System

## 🏗 Architecture

```text
GameManager
 ├── PlayerSetup
 ├── PlayerHealth
 ├── ScoreManager
 └── CharacterData

ObstacleSpawner
 └── ObstacleData

LobbyManager
 └── CharacterData
