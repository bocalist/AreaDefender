using Godot;
using System.Runtime.CompilerServices;

public static class InputManager
{
	private const string LEFT_GO_RIGHT = "leftGoRight";
	private const string LEFT_GO_DOWN = "leftGoDown";
	private const string LEFT_GO_LEFT = "leftGoLeft";
	private const string LEFT_GO_UP = "leftGoUp";
	private const string RIGHT_GO_RIGHT = "rightGoRight";
	private const string RIGHT_GO_DOWN = "rightGoDown";
	private const string RIGHT_GO_LEFT = "rightGoLeft";
	private const string RIGHT_GO_UP = "rightGoUp";

	public static Vector3 leftDirection => GetDirection(LEFT_GO_RIGHT, LEFT_GO_DOWN, LEFT_GO_LEFT, LEFT_GO_UP);
	public static Vector3 rightDirection => GetDirection(RIGHT_GO_RIGHT, RIGHT_GO_DOWN, RIGHT_GO_LEFT, RIGHT_GO_UP);

	private static Vector3 GetDirection(string right, string down, string left, string up)
	{
		Vector3 direction = Vector3.Zero;
		direction.X = Input.GetActionStrength(left) - Input.GetActionStrength(right);
		direction.Y = Input.GetActionStrength(up) - Input.GetActionStrength(down);
		return direction;
	}
}
