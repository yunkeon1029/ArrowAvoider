using System;
using System.Linq;
using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public HealthManager HealthManager { get; private set; }
    [Export]
    public HitReceiver HitReceiver { get; private set; }
    [Export]
    public PlayerMovement PlayerMovement { get; private set; }

    [Export]
    private float _invincibilityTime;
    [Export]
    private Color _invincibillityMoudulate;

    public Player()
    {
        TreeEntered += () => GlobalInstances.AddInstance(this);
        TreeExited += () => GlobalInstances.RemoveInstance(this);
    }

    public override void _Ready()
    {
        HitReceiver.Damaged += _ => StartInvincibillity();
        RequestReady();
    }

    public override void _PhysicsProcess(double elapsedTime)
    {
        Vector2 moveInput = new(0, 0);

        if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left)) 
            moveInput.X -= 1;

        if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right)) 
            moveInput.X += 1;

        PlayerMovement.ProcessMovement(moveInput, elapsedTime);
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
