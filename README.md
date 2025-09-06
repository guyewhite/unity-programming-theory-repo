# unity-programming-theory-repo
submission 2 for jr programmer pathway

## Object-Oriented Programming Concepts Demonstrated

### Abstraction
The game uses the `ICollectable` interface and `CollectableItem` abstract class to define what collectables must do without specifying how. Power-ups and balls implement these abstract contracts, hiding their complex internal logic behind simple interfaces. The abstract methods like `PerformCollection()` and `GetCollectionDifficulty()` force each collectable to provide its own implementation while maintaining a consistent interface.

### Encapsulation
Each ball type keeps its internal state private or protected, such as `fallSpeed`, `pointValue`, and `ballColor`, exposing them only through controlled methods. The `GameManager` class encapsulates game logic, score management, and spawning behavior, preventing direct access to these systems from outside. Player input is encapsulated within the `PlayerController`, separating movement logic from other game systems.

### Inheritance
The `BallController` base class provides common functionality that `GoldenBall`, `SpeedBall`, and `BonusBall` inherit and extend. Each child class inherits protected fields and virtual methods from the parent, reusing code while adding unique behaviors. The inheritance hierarchy allows specialized balls to override base methods like `Move()` and `OnCaught()` to create distinct behaviors.

### Polymorphism
The game demonstrates method overloading through multiple versions of `Initialize()` and `ApplyForce()` that accept different parameters. Virtual methods like `Move()` behave differently for each ball type despite being called through the same interface. All balls are treated as `BallController` objects but execute their own unique implementations at runtime. The `SpawnBall()` method in GameManager has multiple overloaded versions, allowing different ways to spawn objects with various parameters.
