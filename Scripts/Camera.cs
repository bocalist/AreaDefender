using Godot;
using System;

public partial class Camera : Camera3D
{
	[Export] private NodePath targetPath;
	private Node3D target;

	private Vector3 targetPos;

	public override void _Ready()
	{
		SetNodes();
		targetPos = GlobalPosition;
	}
	
	public override void _Process(double delta)
	{
		targetPos = targetPos.Lerp(target.GlobalPosition, (float)delta);
		targetPos.Z = GlobalPosition.Z;
		GlobalPosition = targetPos;
		LookAt(target.Position);
	}

	private void SetNodes()
	{
		target = (Node3D)GetNode(targetPath);
	}
}
