using Godot;
using System;
using System.IO;

/// <summary>
/// Class <c>CreditScene</c>
/// deprecated class
/// Author : Vasulvius
/// </summary>

public partial class CreditScene : Control
{

	[Export] private RichTextLabel textLabel;
	private bool isPauseGameEnable = false;
	// private String text = File.ReadAllText("credits.txt");
	// Called when the node enters the scene tree for the first time.
	// public override void _Ready()
	// {
	// 	textLabel.Clear();
	// 	textLabel.AppendText(text);
	// }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if(Input.IsActionJustPressed("pauseGame") & !isPauseGameEnable){
		// 	GetTree().Root.AddChild(ResourceLoader.Load<PackedScene>("res://Scene/SettingsMenu.tscn").Instantiate());
		// 	isPauseGameEnable = true;
		// }
		// else if(Input.IsActionJustPressed("pauseGame") & isPauseGameEnable){
		// 	foreach(Node scene in GetTree().Root.GetChildren())
		// 	{
		// 		if(scene.Name == "SettingsMenu")
		// 		{
		// 			GetTree().Root.RemoveChild(scene);
		// 		}
		// 	}
		// 	isPauseGameEnable = false;
		// }
	}
}
