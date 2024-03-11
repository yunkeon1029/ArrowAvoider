using System;
using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public HealthManager HealthManager { get; private set; }
    [Export]
    public HitReceiver HitReceiver { get; private set; }

    [Export] 
    private float _maxSpeed;
    [Export] 
    private float _accelerationRate;

    [Export]
    private float _invincibilityTime;
    [Export]
    private Color _invincibillityMoudulate;

    private Vector2 _velocity;

    public Player()
    {
        TreeEntered += () => GlobalInstances.AddInstance(this);
        TreeExited += () => GlobalInstances.RemoveInstance(this);
    }

	public static Vector2 GetMoveDir()
	{
        Vector2 moveDir = new(0, 0);

        if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left)) 
            moveDir.X -= 1;

        if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right)) 
            moveDir.X += 1;

        if (moveDir != Vector2.Zero)
            moveDir = moveDir.Normalized();

        return moveDir;
	}

    public override void _Ready()
    {
        HitReceiver.Damaged += _ => StartInvincibillity();
    }

    public override void _PhysicsProcess(double elapsedTime)
    {
		Vector2 moveDir = GetMoveDir();
        Vector2 targetVelocity = moveDir * _maxSpeed;

        _velocity = _velocity.MoveToward(targetVelocity, _accelerationRate * (float)elapsedTime);

        var collisionResult = MoveAndCollide(_velocity * (float)elapsedTime, false, 0);

        if (collisionResult != null)
            _velocity = Vector2.Zero;
    }

    private void StartInvincibillity()
    {
        SceneTree tree = GetTree();
        SceneTreeTimer invincibillityTimer = tree.CreateTimer(_invincibilityTime);

        Color previousMoudulate = Modulate;
        uint previousHitLayer = HitReceiver.CollisionLayer;

        invincibillityTimer.Timeout += () => Modulate = previousMoudulate;
        invincibillityTimer.Timeout += () => HitReceiver.ModifyDamage -= InvincibillityMod;

        HitReceiver.ModifyDamage += InvincibillityMod;
        Modulate = _invincibillityMoudulate;

        static void InvincibillityMod(ref float damage) => damage = 0;
    }
}