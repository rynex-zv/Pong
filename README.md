# Pong

A classic **Pong-style arcade game** created in **Unity 3D** with **C#**.

The game supports both **solo play against the CPU** and **local 2-player matches**, with multiple CPU difficulty levels and gameplay effects that can change the flow of a match.

## Features

- **Solo mode** against a CPU opponent
- **Local 2-player mode**
- Multiple **CPU difficulty levels**
- Classic Pong-style paddle and ball gameplay
- Score tracking
- Gameplay effects / power-up style modifiers
- Unity physics-based movement and collision handling

### Game effects

The project includes support for several player effects, including:

- **Speed Up**
- **Enlarge**
- **Invisibility**
- **Shield**
- **Double Score**

Some effects are implemented as timed gameplay modifiers and the effect system is designed so additional effects can be added.

## Built with

- **Unity 2022.3.12f1**
- **C#**
- Unity physics / Rigidbody components
- TextMesh Pro / Unity UI

## Project structure

```text
Assets/
├── code/          C# gameplay logic
├── Objects/       game objects / assets
├── Scenes/        Unity scenes
└── TextMesh Pro/  UI resources

ProjectSettings/   Unity project configuration
```

## Opening the project

1. Clone the repository:

```bash
git clone https://github.com/rynex-zv/Pong.git
```

2. Open the project folder using **Unity Hub**.
3. Use **Unity 2022.3.12f1** or a compatible Unity 2022 LTS version.
4. Open one of the scenes in `Assets/Scenes/` and press **Play**.

## About

This project was created by **Rynex Akil (`@rynex-zv`)** as a Unity/C# implementation of the classic Pong concept, with additional CPU difficulty and gameplay-effect systems beyond the basic two-paddle game.
