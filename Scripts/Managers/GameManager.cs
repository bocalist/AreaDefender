using Godot;
using System;

public partial class GameManager : Node3D
{
	public override void _Ready()
	{
		CustomSignal.Emeter.EmitSignal(nameof(CustomSignal.Emeter.Init));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
