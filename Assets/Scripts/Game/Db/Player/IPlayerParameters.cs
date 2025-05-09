namespace Game.Db.Player
{
    public interface IPlayerParameters
    {
        float Speed { get; }
        float SprintSpeed { get; }
        float SprintAcceleration { get; }
        float Acceleration { get; }
        float GroundedDrag { get; }
        float AirborneDrag { get; }
        float GroundCheckSphereRadius { get; }
        float JumpForce { get; }
        float JumpCooldown { get; }
        float LookVerticalClamp { get; }
    }
}