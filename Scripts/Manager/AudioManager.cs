using Godot;
using System;
using System.Linq;

/// <summary>
/// Class <c>AudioManager</c> defines the behavior
/// of the AudioManager
/// Play ephemeral sound effects
/// Play music
/// Set volume
/// Last modified : 14/11/2023
/// Author : Vasulvius
/// </summary>

public partial class AudioManager : Node
{
	[Export] private Godot.Collections.Array<AudioStream> mainThemeList;
	[Export] private AudioStream gameOverMusic;
	[Export] private AudioStream gameWinMusic;
	private Int16 mainThemePosition;
	private AudioStreamPlayer musicPlayer;
	private AudioStreamPlayer uiPlayer;
	private Boolean isMusicStopped = false;
	private Int16 count;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		musicPlayer = (AudioStreamPlayer)FindChild("MusicPlayer");
		uiPlayer = (AudioStreamPlayer)FindChild("UI_JinglePlayer");
		PlayMainTheme();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!isMusicStopped & !musicPlayer.Playing)
		{
			PlayMainTheme();
		}
	}

	/// <summary>
	/// Method <c>PlayMainTheme</c> loop through a Godot array
	/// of AudioStreams to play main music
	/// </summary>
	public void PlayMainTheme()
	{
		musicPlayer.Stream = mainThemeList[mainThemePosition];
		mainThemePosition ++;
		mainThemePosition = (Int16)(mainThemePosition % mainThemeList.Count());
		musicPlayer.Play();
	}

	/// <summary>
	/// Method <c>PlayGameOverMusic</c> reduce the main music volume and
	/// play the game over jingle on a different bus to avoid
	/// overlap
	/// </summary>
	public void PlayGameOverMusic()
	{
		musicPlayer.VolumeDb = -10f;
		uiPlayer.Stream = gameOverMusic;
		uiPlayer.Play();
	}

	/// <summary>
	/// Method <c>PlayGameWinMusic</c> same as
	/// PlayGameOverMusic()
	/// </summary>
	public void PlayGameWinMusic()
	{
		musicPlayer.VolumeDb = -10f;
		uiPlayer.Stream = gameWinMusic;
		uiPlayer.Play();
	}

	public void StopMusic()
	{
		musicPlayer.Stop();
	}

	/// <summary>
	/// Method <c>PlaySoundEffects</c> create a ephemeral AudioStreamPlayer
	/// to play a sound effect. After the play finished the AudioStreamPlayer
	/// is deleted.
	/// The bus to play is Effects.
	/// <paramref name="mySound"/>The sound effect to play</param>
	/// <param name="vol">A volume param [-80, 20] to do sound equilibrium</param>
	/// </summary>
	public async void PlaySoundEffect(AudioStream mySound, float vol = 0f)
	{
		AudioStreamPlayer soundPlayer = new AudioStreamPlayer{};
		AddChild(soundPlayer);
		soundPlayer.Bus = "Effects";
		soundPlayer.VolumeDb = vol;
		soundPlayer.Stream = mySound;
		soundPlayer.Play();
		await ToSignal(soundPlayer, "finished");
		RemoveChild(soundPlayer);
	}

	public void SetMusicVolume(float volume)
	{
		musicPlayer.VolumeDb = volume;
	}
}
