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
    Loot,
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

public enum ObjectType
{
    MapDevice,
    Step,
    Default
}

public enum CharacterState
{
    PlayerControl,
    Interact,
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
    Death,
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
