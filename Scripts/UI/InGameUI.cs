using Godot;
using System;

/// <summary>
/// Class <c>In Game UI</c>
/// defines access to in game ui display,
/// such as score, progress and stopwatch.
/// Last Modified : 13/11/2023
/// Author: Nebre 38-169
/// </summary>
public partial class InGameUI : CanvasLayer
{
	private Label scoreLabel;
	private Label remainingLabel;
	private Label progressLabel;
	private Label timeLabel;

	public override void _Ready()
	{
		scoreLabel = (Label)FindChild("ScoreLabel");
		remainingLabel = (Label)FindChild("RemainingLabel");
		progressLabel = (Label)FindChild("ProgressLabel");
		timeLabel = (Label)FindChild("TimeLabel");
	}

	/// <summary>
	/// Method <c>Set Score</c>
	/// updates the score display by updating
	/// the collected and all bubbles of the level
	/// </summary>
	/// <param name="score">Collected bubbles</param>
	/// <param name="missingScore">Remaining bubbles</param>
	public void SetScore(int score, int missingScore)
	{
		string scoreStr = score.ToString();
		string remainingStr = (score + missingScore).ToString();
		while (scoreStr.Length < remainingStr.Length) //To fix the lenght of the display, one had 0 in front until it matchs
		{
			scoreStr = "0" + scoreStr;
		}
		scoreLabel.Text = scoreStr;
		remainingLabel.Text = remainingStr;
	}

	/// <summary>
	/// Method <c>Set Progress</c>
	/// updates the progress display by updating the 
	/// percentage
	/// </summary>
	/// <param name="progress">Progress of the soap in the player from 0 to 1</param>
	public void SetProgress(float progress)
	{
		string progressStr = MathF.Round(progress * 100).ToString();
		while (progressStr.Length < 3)
		{
			progressStr = "0" + progressStr;
		}
		progressLabel.Text = progressStr + "%";
	}

	/// <summary>
	/// Method <c>Set Time</c>
	/// updates the stopwatch display
	/// </summary>
	/// <param name="currentTime">Time in second from the start of the level</param>
	public void SetTime(double currentTime)
	{
		string currentTimeStr = MathF.Round((float)currentTime, 3).ToString();
		if (currentTimeStr.Length == 1)
		{
			currentTimeStr = currentTimeStr + ",000";
		}
		else
		{
			while (currentTimeStr.Length < 5)
			{
				currentTimeStr = currentTimeStr + "0";
			}
		}
		timeLabel.Text = currentTimeStr + "s";
	}
}
