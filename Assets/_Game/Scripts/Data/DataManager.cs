using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager
{
	public SaveGameData SaveGameData;
	public UserData UserData;
	public SettingData SettingData;
	
	public Dictionary<EIceCream, IceCreamEntity.Param> IceCreamDictionary = new Dictionary<EIceCream, IceCreamEntity.Param>();

	private static DataManager _instance;

	public static DataManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new DataManager();
			}

			return _instance;
		}
	}

	private DataManager()
	{
		ImportData();
		
		InitUserData();
        InitSettingData();
		InitSaveGameData();
	}

	public void SaveAllData()
	{
		SaveGame();
	}

	private void ImportData()
	{
		// TODO: Import Asset Data Here
		IceCreamDictionary = Resources.Load<IceCreamEntity>(DATA_RESOURCES.DATA.ICE_CREAM_DATA).Params.ToDictionary(e => e.ID, e => e);
	}

	void InitSaveGameData()
	{
		if (PlayerPrefs.HasKey(CONST.PLAYER_PREF_SAVE_GAME_DATA))
		{
			string saveGameStr = PlayerPrefs.GetString(CONST.PLAYER_PREF_SAVE_GAME_DATA);
			SaveGameData = JsonConvert.DeserializeObject<SaveGameData>(saveGameStr);
		} else
		{
			SaveGameData = new SaveGameData();
		}

		SaveGame();
	}

	void SaveGame(SaveGameData saveGameData = null)
	{
		if (saveGameData != null) SaveGameData = saveGameData;
		if (SaveGameData != null)
		{
			PlayerPrefs.SetString(CONST.PLAYER_PREF_SAVE_GAME_DATA, JsonConvert.SerializeObject(SaveGameData));
		}
	}

	private void InitSettingData()
	{
		if (PlayerPrefs.HasKey(CONST.PLAYER_PREF_SETTING_DATA))
		{
			string dataStr = PlayerPrefs.GetString(CONST.PLAYER_PREF_SETTING_DATA);
			Debug.Log("Setting Data: " + dataStr);
			SettingData = JsonConvert.DeserializeObject<SettingData>(dataStr);
		} else
		{
			SettingData = new SettingData();
		}

		SettingData.OnValueChanged = SaveSetting;
		SaveSetting();
	}

	void SaveSetting()
	{
		if (SettingData != null)
		{
			PlayerPrefs.SetString(CONST.PLAYER_PREF_SETTING_DATA, JsonConvert.SerializeObject(SettingData));
		}
	}

	private void InitUserData()
	{
		if (PlayerPrefs.HasKey(CONST.PLAYER_PREF_USER_DATA))
		{
			string dataStr = PlayerPrefs.GetString(CONST.PLAYER_PREF_USER_DATA);
			Debug.Log("User Data: " + dataStr);
			UserData = JsonConvert.DeserializeObject<UserData>(dataStr);
		} else
		{
			UserData = new UserData();
		}

		UserData.OnValueChanged = SaveUserData;
		SaveUserData();
	}

	void SaveUserData()
	{
		if (UserData != null)
		{
			PlayerPrefs.SetString(CONST.PLAYER_PREF_USER_DATA, JsonConvert.SerializeObject(UserData));
		}
	}
}
