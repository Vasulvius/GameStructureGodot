using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Class <c>Stats Manager</c> handles all statistics such as 
/// progress, score and time from start.
/// Last edited : 11/11/2023
/// Author : Nebre 38-169
/// </summary>
public partial class StatsManager : Node
{
	public float finalTime { get; private set; }
	public float progress { get; private set; }
	public int score { get; private set; }
	public double currentTime { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Node parent = GetParent();
		score = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		currentTime += delta;
	}

	/// <summary>
	/// Method <c>Start Chrono</c> starts the timer
	/// for how much time the player is in the level
	/// </summary>
	public void StartChrono()
	{
		GD.Print("Start time");
		currentTime = 0;
	}

	/// <summary>
	/// Method <c>Stop Chrono</c> stops the timer
	/// by getting the time from start to end and storing it
	/// into the <c>finalTime</c> attribut. Returns the final time
	/// </summary>
	/// <returns>Time in seconds from start to finish</returns>
	public float StopChrono()
	{
		finalTime = (float)currentTime;
		return MathF.Round(finalTime);
	}

	/// <summary>
	/// Method <c>Increment Score</c>
	/// </summary>
	/// <param name="d">Value to add to the score</param>
	public void IncrementScore(int d)
	{
		score += d;
		GD.Print("New score:", score);
	}

	public int GetMissingScore()
	{
		Array<Node> bubbles = GetTree().GetNodesInGroup("Bubbles");
		return bubbles.Count;
	}
}