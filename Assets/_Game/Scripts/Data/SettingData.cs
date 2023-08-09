using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class SettingData
{
	[JsonIgnore] public Action OnValueChanged;

	private bool _alreadyRate;
	public bool AlreadyRate
	{
		get => _alreadyRate;
		set
		{
			_alreadyRate = value;
			OnValueChanged?.Invoke();
		}
	}
	private bool _sound = true;
	public bool Sound
	{
		get => _sound;
		set
		{
			_sound = value;
			OnValueChanged?.Invoke();
		}
	}

	private bool _music = true;
	public bool Music
	{
		get => _music;
		set
		{
			_music = value;
			OnValueChanged?.Invoke();
		}
	}

	private bool _notification = true;
	public bool Notification
	{
		get => _notification;
		set
		{
			_notification = value;
			OnValueChanged?.Invoke();
		}
	}
	
	private bool _vibration = true;
	public bool Vibration
	{
		get => _vibration;
		set
		{
			_vibration = value;
			OnValueChanged?.Invoke();
		}
	}
}