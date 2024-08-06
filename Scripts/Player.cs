using Godot;
using System;

public partial class Player : Area3D
{
	// instance
	private static Player instance = null;

	// Input
	private Vector3 inputDirection = Vector3.Zero;

	// Motion Properties
	private Vector3 forward = Vector3.Zero;
	private Vector3 towardCamera = -Vector3.Forward;
	private Vector3 baseForward = Vector3.Up;

	// Movements
	private float accelerationSpeed = 30f;
	private float friction = .25f;
	private Vector3 acceleration = Vector3.Zero;
	private Vector3 velocity = Vector3.Zero;
	public Vector3 Velocity {get => velocity; private set => velocity = value;}

	// Rotations
	private float angularAccelerationSpeed = Mathf.Pi * 3f;
	private float angularFriction = 5f;
	private float lastRotationInput = 0f;
	private Vector3 angularAcceleration = Vector3.Zero;
	private Vector3 angularVelocity = Vector3.Zero;

	// Renderer
	[Export] private NodePath rendererPath;
	private Node3D renderer;

	[Export] private NodePath testPath;
	private Node3D test;


	public override void _Ready()
	{
		SetInstance();
		SetNodes();
		test = (Node3D)GetNode(testPath);
	}

	public override void _Process(double delta)
	{
		Move((float)delta);
	}

    public override void _Input(InputEvent @event)
    {
        inputDirection = InputManager.leftDirection;
    }

	private void SetInstance() => instance = this;

    private void Move(float delta)
	{
		UpDateForward();
		MoveDisplacement(delta);
		MoveRotation(delta);

		Vector3 oui = (-GlobalPosition).Normalized() * 3f;
		test.GlobalPosition = GlobalPosition + oui;;

	}

	private void MoveDisplacement(float delta)
	{
		acceleration = (InputManager.shoot ? 0f : InputManager.speedBoost ? 50f : accelerationSpeed) * forward * inputDirection.Length();
		velocity += acceleration * delta;
		Position = Position.Lerp(Position + velocity, delta);
		if (InputManager.shoot) velocity *= Mathf.Pow(.1f, delta);
		else velocity *= Mathf.Pow(friction, delta);

		renderer.Scale = new Vector3(1, 1 + velocity.Length() * .02f, 1);
	}

	private void MoveRotation(float delta)
	{
		lastRotationInput = inputDirection.Length() > 0 ? -Mathf.Atan2(inputDirection.X, inputDirection.Y) : lastRotationInput;
		Rotation = towardCamera * Mathf.LerpAngle(Rotation.Z, lastRotationInput, angularFriction * delta);

		renderer.Rotation = Vector3.Up * Mathf.LerpAngle(renderer.Rotation.Y, Mathf.AngleDifference(lastRotationInput, Rotation.Z) * 1.5f, angularFriction * delta);
	}

	private void UpDateForward() => forward = baseForward.Rotated(towardCamera, Rotation.Z);

	private void SetNodes() { renderer = (Node3D)GetNode(rendererPath); }

	public static Player GetCurrentInstance() => instance;
}