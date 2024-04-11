using Godot;
using System;

/// <summary>
/// Class <c>PlayLevelButton</c> 
/// Defines the behavior of a button to load a level
/// Last modified : 14/11/2023
/// Author : Vasulvius
/// </summary>

public partial class PlayLevelButton : Button
{
	[Export] private PackedScene scene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CheckPressed();
	}

	/// <summary>
	/// Method <c>CheckPressed</c> self observer pattern
	/// The button is waiting for him to be pressed and
	/// trigger an action.
	/// Works only once
	/// </summary>
	private async void CheckPressed()
	{
		await ToSignal(this, "pressed");
		if (scene != null)
		{
			GetTree().ChangeSceneToPacked(scene);
		}
		GD.Print(this.Name);
	}
}
