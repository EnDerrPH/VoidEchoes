public enum GeyserPhase
{
    One,
    Two,
    Three,
    Four,
    Five,
    Done,
    Default
}

public enum ParticleType
{
    Player,
    Monster,
    Trap,
    Default
}

public enum GameScene
{
    MainMenu,
    CharacterSelection,
    Home,
    Game,
    Default
}

public enum InteractableType
{
    Projector,
    Default
}

public enum CharacterState
{
    PlayerControl,
    OnMapDevice,
    Default
}

public enum ShipState
{
    Maneuver,
    MovingFoward,
    ResetTransform,
    TurnRight,
    TurnLeft,
    AttackMode,
    Idle,
    Default
}

public enum MonsterState
{
    Alert,
    Chasing,
    Attacking,
    Patrol,
    OnHit,
    OnDeath,
    Default
}
