using Godot;
using System;

/// <summary>
/// Class <c>SettingMenu</c> defines setting menu behavior
/// Last modified : 14/11/2023
/// Author : Vasulvius
/// </summary>

public partial class SettingsMenu : CanvasLayer
{
	private SceneManager sceneManager;
	/// <summary>
	/// Method <c>_Ready</c> get the scene manager to
	/// call its methodes for buttons
	/// </summary>
	public override void _Ready()
	{
		sceneManager = GetParent<SceneManager>();
		if (sceneManager == null)
		{
			GD.PrintErr("Missing SceneManager");
		}
	}

	void OnResumeButtonPressed()
	{
		sceneManager.ResumeGame();
	}

	void OnRestartButtonPressed()
	{
		sceneManager.Restart();
	}

	void OnMainMenuButtonPressed()
	{
		sceneManager.MainMenu();
	}

	void OnQuitButtonPressed()
	{
		sceneManager.Quit();
	}
}
