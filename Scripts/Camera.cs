using Godot;
using System;

public partial class Camera : Camera3D
{
	// Target
	private Node3D target;
	private Vector3 targetPos;

	// Target Player
	private Player player;

	// Camera Behavior
	private Action<float> behavior;

	public override void _Ready()
	{
		SetBehaviorStatic();
		SetInit();
	}
	
	public override void _Process(double delta)
	{
		behavior((float)delta);
	}

	private void SetBehaviorStatic() => behavior = Static;

	private void Static(float delta) {}

	private void SetBehaviorFocusPlayer() 
	{
		player = Player.GetCurrentInstance();
		behavior = FocusPLayer;
	}

	private void FocusPLayer(float delta)
	{
		targetPos = targetPos.Lerp(player.GlobalPosition + player.Velocity * .7f, 2.5f * delta);
		Vector3 newPosition = GlobalPosition.Lerp(player.GlobalPosition, 5 * delta);
		newPosition.Z = GlobalPosition.Z;
		GlobalPosition = newPosition;
		LookAt(targetPos);
	}

	private void SetInit() => CustomSignal.Emeter.Init += Init;

	private void Init() => SetBehaviorFocusPlayer();
}
