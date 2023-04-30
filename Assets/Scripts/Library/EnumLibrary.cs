 namespace EnumLibrary
{
    public enum CorridorDirection
    {
        BackToFront,
        FrontToBack,
        LeftToRight,
        RightToLeft
    }
    public enum LevelOfStrees
    {
        High = 0,
        Medium = 1,
        Low = 2
    }
    public enum EnemyType
    {
        Aerial, Tank, Shooter, Zombielike, Null
    }
    public enum WeaponType
    {
        Distance,
        Melee
    }

    public enum WeaponAnimType
    {
        PunchWeapon,
        SwordMeleeWeapon,
        OneHandedFireWeapon,
        TwoHandedFireWeapon,
        NoAnmation
    }

    public enum AnimationActions
    {
        Idle,
        Jump,
        Attack,
        Countdown,
        Walk,
        Crouch
    }

    public enum UsableItemType
    {
        Key,
        Button
    }
    public enum Escenas
    {
        StartScreen,
        GameScreen,
    }
}
