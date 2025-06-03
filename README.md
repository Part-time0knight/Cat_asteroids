# Cat_asteroids
 
Cat's Asteroids
A clone of the classic Asteroids game. This project was created to explore web development with Unity.

The core gameplay is fully implemented. Currently in development are unique killer features intended to set the game apart from the original and other clones.

## Technology Stack
- **Unity**
- **C#**
- **Zenject**
- **DOTween**
- **UniTask**
- **UniRx**

## Architectural Solutions

### Gameplay
- Built using **Finite State Machine (FSM)**
- Data exchange is encapsulated through DI contexts:
  - Services are defined at scene level
  - Object-specific logic resides in their local contexts

### Game Objects
- Uses **Facade pattern** to manage complex entities (e.g., player ship)
- The facade entry point delegates control to **FSM**, which coordinates internal components
- Key components (e.g., shooting or movement handlers) implement **Strategy pattern via interfaces**, enabling future dynamic behavior changes (e.g., switching bullets to lasers or changing fire modes)
- The strategy approach will be extended to enemies after testing the component-swapping mechanism on the player

### UI
- Implemented using **MVVM (Model-View-ViewModel)** pattern
- Features a dedicated state machine for screen management

### Optimization
- Most game objects (projectiles, asteroids, effects) utilize **object pooling**

## Key Features: Mutators Instead of Classic Mechanics

Some familiar mechanics from the original Asteroids (like asteroid fragmentation or hyperspace jumps) are intentionally absent from the core gameplay. Instead, they're introduced through a **Mutators system** - sets of two interconnected modifiers:

### Player Modifier
Alters player ship behavior:
- Adds new capabilities (e.g., hyperspace jump)
- Replaces existing mechanics (e.g., switching bullets to lasers or burst fire instead of single shots)

### Enemy Modifier
Changes enemy behavior:
- Asteroids begin splitting when destroyed
- Enemies gain new attack patterns (e.g., homing attacks)

### How It Works:
- Mutator combinations are **randomly generated**
- Players can purchase them between levels via a special menu (*Data layer destroyed!*), which unlocks after reaching certain score thresholds

*This system is currently in development and will be expanded*

## Playable Build
- https://github.com/Part-time0knight/Cat_asteroids/releases/tag/alpha

![Cats_asteroids](https://github.com/user-attachments/assets/1f3eeb80-e490-4b29-9997-3d99ae091df0)

