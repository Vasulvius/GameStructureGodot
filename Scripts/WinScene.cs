using Godot;
using System;

/// <summary>
/// Class <c>GameOver</c> defines win panel behavior
/// Last modified : 14/11/2023
/// Author : Vasulvius + Nebre 38-169
/// </summary>

public partial class WinScene : CanvasLayer
{

	private Label scoreLabel;
	private Label progressionLabel;
	private Label timeLabel;
	private SceneManager sceneManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetParent<SceneManager>();
		if (sceneManager == null)
		{
			GD.PrintErr("Missing SceneManager");
		}
	}

	/// <summary>
	/// Method <c>SetStats</c> set the labels to
	/// show the statistiques of the game
	/// Called by the scene manager
	/// </summary>
	public void SetStats(float myScore, float myProg, float myTime, float totalScore)
	{
		scoreLabel = (Label)FindChild("ScoreContent"); //Serch recusivly in the TreeNode the element ScoreContent
		progressionLabel = (Label)FindChild("ProgressContent");
		timeLabel = (Label)FindChild("TimeContent");
		scoreLabel.Text = myScore.ToString() + "/" + totalScore.ToString();
		progressionLabel.Text = myProg.ToString() + " %";
		timeLabel.Text = myTime.ToString() + "s";
	}

	private void OnRestartButtonPressed()
	{
		sceneManager.Restart();
	}

	private void OnNextButtonPressed()
	{
		sceneManager.LoadNextLevel();
	}
	private void OnMainMenuButtonPressed()
	{
		sceneManager.MainMenu();
	}

	private void OnQuitButtonPressed()
	{
		sceneManager.Quit();
	}
}
