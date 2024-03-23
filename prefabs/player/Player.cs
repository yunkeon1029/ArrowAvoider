using Godot;
using System;

internal partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void InvincibillityStartedEventHandler();
    [Signal]
    public delegate void InvincibillityEndedEventHandler();
    
    [Export]
    public HealthManager HealthManager { get; private set; }
    [Export]
    public PlayerMovement PlayerMovement { get; private set; }

    [Export]
    private float _invincibillityTime;
    [Export]
    private Color _invincibillityModulate;

    private float _leftInvincibillityTime;

    public override void _PhysicsProcess(double elapsedTime)
    {
        CalculateMovement(elapsedTime);
        CalculateInvincibillity(elapsedTime);
    }

    public void ApplyDamage(float amount)
    {
        if (_leftInvincibillityTime > 0 || amount <= 0)
            return;

        HealthManager.Health -= amount;
        StartInvincibillity();
    }

    private void CalculateMovement(double elapsedTime)
    {
        Vector2 moveInput = new();

        if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left))
            moveInput += Vector2.Left;

        if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right))
            moveInput += Vector2.Right;

        PlayerMovement.CalculateMovement(moveInput, elapsedTime);
    }

    private void CalculateInvincibillity(double elapsedTime)
    {
        if (_leftInvincibillityTime <= 0)
            return;

        _leftInvincibillityTime -= (float)elapsedTime;
        _leftInvincibillityTime = Mathf.Max(0, _leftInvincibillityTime);

        if (_leftInvincibillityTime <= 0)
            EndInvincibillity();
    }

    private void StartInvincibillity()
    {
        if (_invincibillityTime <= 0)
            return;

        _leftInvincibillityTime = _invincibillityTime;
        Modulate = _invincibillityModulate;

        EmitSignal(SignalName.InvincibillityStarted);
    }

    private void EndInvincibillity()
    {
        Modulate = new Color(1, 1, 1, 1);
        EmitSignal(SignalName.InvincibillityEnded);
    }
}