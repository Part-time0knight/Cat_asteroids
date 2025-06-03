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
