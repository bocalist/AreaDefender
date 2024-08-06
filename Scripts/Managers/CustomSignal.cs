using System;
using Godot;

public partial class CustomSignal : Node
{
	// Emeter
	private static CustomSignal emeter = new CustomSignal();
	public static CustomSignal Emeter {get => emeter; private set => emeter = value;}

	// In Game Signal
	[Signal] public delegate void InitEventHandler();
}