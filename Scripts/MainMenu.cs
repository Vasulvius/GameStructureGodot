using Godot;
using System;

/// <summary>
/// Class <c>MainMenu</c> defines the behavior of the Main Menu scene
/// Last modified : 14/11/2023
/// Author : Vasulvius
/// </summary>

public partial class MainMenu : Control
{

	[Export] private PackedScene startingLevel;

	void OnStartButtonPressed()
	{
		if(startingLevel == null){throw new ArgumentException("No level selected for startingLevel !");}
		GetTree().ChangeSceneToPacked(startingLevel);
	}

	void OnLevelsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/SpecialLevel/LevelSelector.tscn");
	}

	void OnTutoButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/SpecialLevel/Tuto.tscn");
	}

	void OnSettingsButtonPressed()
	{
		GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://Scene/UI/SettingsMenu.tscn").Instantiate());
	}

	void OnCreditsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scene/SpecialLevel/CreditScene.tscn");
	}

	void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
