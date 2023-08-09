using System;
using Newtonsoft.Json;

[Serializable]
public class UserData
{
	[JsonIgnore] public Action OnValueChanged;

	private int _currentLevel;

	public int CurrentLevel
	{
		get => _currentLevel;
		set
		{
			_currentLevel = value;
			OnValueChanged?.Invoke();
		}
	}
	
	private int _unlockedLevel;

	public int UnlockedLevel
	{
		get => _unlockedLevel;
		set
		{
			_unlockedLevel = value;
			OnValueChanged?.Invoke();
		}
	}
	
	private int _coin;
	public int Coin
	{
		get => _coin;
		set
		{
			_coin = value;
			OnValueChanged?.Invoke();
		}
	}

	private int _gem;
	public int Gem
	{
		get => _gem;
		set
		{
			_gem = value;
			OnValueChanged?.Invoke();
		}
	}

	private bool _isAcceptGDPR = false;
	public bool IsAcceptGDPR
	{
		get => _isAcceptGDPR;
		set
		{
			_isAcceptGDPR = value;
			OnValueChanged?.Invoke();
		}
	}
}