using Godot;
using System;

/// <summary>
/// Class <c>SceneManager</c>
/// Manage the scene for reproducible behaviors
/// Defines state of the game : editor or player mode
/// Defines usefull methods for UI
/// Know how to trigger game over, win and pause
/// Allow to travel trought scenes
/// Last modified : 14/11/2023
/// Author : Vasulvius + Nebre 38-169
/// </summary>
public partial class SceneManager : Node
{
	/// <summary>
	/// Variable <c>editorModer</c> bool
	/// True to be in editor mode
	/// False to be in player mode
	/// Editor mode allow to tests functions as
	/// trigger audio, game over, win with
	/// a simple input
	/// </summary>
	[Export] private bool editorMode = false;
	[Export] private AudioStream testSound;
	[Export] private bool settingsWithRestartButton = true;
	[Export] private PackedScene nextLevel;
	[Export] private bool isFinalLevel = false;
	private string gameOverPath = "res://Scene/UI/GameOver.tscn";
	private string gameWinPath = "res://Scene/UI/WinScene.tscn";
	private string gameFinalWinPath = "res://Scene/UI/FinalWinScene.tscn";
	private string pausePath = "res://Scene/UI/SettingsMenu.tscn";
	private string pausePathNoRestart = "res://Scene/UI/SettingsMenuNoRestart.tscn";
	private bool isGameOverLoaded = false;
	private bool isWinSceneLoaded = false;
	// private StatsManager statsManager;
	private AudioManager audioManager;
	private GameOver gameOverMenu;
	private WinScene winScene;
	private InGameUI inGameUI;

	public override void _Ready()
	{
		GetTree().Paused = false;
		// statsManager = (StatsManager)GetParent().FindChild("StatsManager");
		audioManager = GetNode<AudioManager>("/root/AudioManager");
		inGameUI = (InGameUI)GetParent().FindChild("InGameUI");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("debugPlaySound") & editorMode)
		{
			audioManager.PlaySoundEffect(testSound, 10f);
		}
		if (editorMode & Input.IsKeyPressed(Key.A) & !isGameOverLoaded)
		{
			GameOver();
		}
		if (editorMode & Input.IsKeyPressed(Key.Z) & !isWinSceneLoaded)
		{
			WinGame();
		}
		if (Input.IsActionJustPressed("pauseGame") & !GetTree().Paused & !isGameOverLoaded & !isWinSceneLoaded)
		{
			Pause();
		}
		else if (Input.IsActionJustPressed("pauseGame") & GetTree().Paused & !isGameOverLoaded & !isWinSceneLoaded)
		{
			ResumeGame();
		}
		// inGameUI.SetScore(statsManager.score, statsManager.GetMissingScore());
		// inGameUI.SetProgress(statsManager.progress);
		// inGameUI.SetTime(statsManager.currentTime);
	}

	/// <summary>
	/// Method <c>Pause</c>
	/// Pause the game + add the pause panel
	/// Two pause panel possible with a restart button
	/// or not. It depends on if it's
	/// a level or a menu like credits
	/// </summary>
	private void Pause()
	{
		if (settingsWithRestartButton)
		{
			AddChild(ResourceLoader.Load<PackedScene>(pausePath).Instantiate());
		}
		else
		{
			AddChild(ResourceLoader.Load<PackedScene>(pausePathNoRestart).Instantiate());
		}

		GetTree().Paused = true;
	}

	/// <summary>
	/// Method <c>GameOver</c>
	/// Can process only if the game is not already game over
	/// </summary>
	public void GameOver()
	{
		if (!isGameOverLoaded)
		{
			audioManager.PlayGameOverMusic();
			AddChild(ResourceLoader.Load<PackedScene>(gameOverPath).Instantiate());

			// Get the game over menu. Don't know why but GetChild doesn't work.
			foreach (Node scene in GetChildren())
			{
				if (scene.Name == "GameOver")
				{
					gameOverMenu = (GameOver)scene;
				}
			}

			// gameOverMenu.SetStats(statsManager.score, statsManager.progress * 100, statsManager.score + statsManager.GetMissingScore());
			isGameOverLoaded = true;
			GetTree().Paused = true;
		}
	}

	/// <summary>
	/// Method <c>WinGame</c>
	/// Can process only if the game is not already won
	/// Two type of win panel, with a restart button or
	/// with credit button. It depends on if it is the 
	/// level
	/// </summary>
	public void WinGame()
	{
		audioManager.PlayGameWinMusic();

		if (!isFinalLevel)
		{
			AddChild(ResourceLoader.Load<PackedScene>(gameWinPath).Instantiate());

			// Get the game win menu
			foreach (Node scene in GetChildren())
			{
				if (scene.Name == "WinScene")
				{
					winScene = (WinScene)scene;
				}
			}
		}
		else
		{
			AddChild(ResourceLoader.Load<PackedScene>(gameFinalWinPath).Instantiate());

			// Get the game win menu
			foreach (Node scene in GetChildren())
			{
				if (scene.Name == "WinScene")
				{
					winScene = (WinScene)scene;
					GD.Print(winScene.Name);
				}
			}
		}

		isWinSceneLoaded = true;
		GetTree().Paused = true;
	}

	public void LoadNextLevel()
	{
		GetTree().ChangeSceneToPacked(nextLevel);
	}

	/// <summary>
	/// Method <c>SayHello</c>
	/// Debug function to see if you can call SceneManager's methods
	/// </summary>
	public void SayHello()
	{
		GD.Print("SceneManager say Hello.");
	}

	public void Restart()
	{
		GetTree().ReloadCurrentScene();
		audioManager.SetMusicVolume(0f);
	}

	public void MainMenu()
	{
		GetTree().ChangeSceneToFile("res://Scene/SpecialLevel/MainMenu.tscn");
	}

	/// <summary>
	/// Method <c>ResumeGame</c>
	/// Remove the setting panel to avoid
	/// Unpause the game
	/// </summary>
	public void ResumeGame()
	{
		// Remove the child SettingsMenu
		// I don't know why but FindChild("SettingsMenu") return null 
		foreach (Node scene in GetChildren())
		{
			if (scene.Name == "SettingsMenu")
			{
				RemoveChild(scene);
			}
		}
		GetTree().Paused = false;
	}

	public void Quit()
	{
		GetTree().Quit();
	}
}
