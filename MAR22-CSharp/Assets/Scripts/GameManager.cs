using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for holding relevant state of the game, namely the total earned muffins, nmuffins per click, deciding if we can buy an upgrade 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalEarnedMuffins;
    public int muffinsPerClick = 1;
    public int muffinsPerSecond = 0;
    public UpgradeButton upgrade1Level;
    public UpgradeButton upgrade2Level;

    private float passiveMuffinCountdown = 1f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        LoadMyData();

        //InvokeRepeating(nameof(CollectPassiveMuffins), 1f, 1f);
    }

    private void CollectPassiveMuffins()
    {
        totalEarnedMuffins += muffinsPerSecond;
    }

    private void Update()
    {
        // resetting the game
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ResetGame();
        }

        // passive muffin increase
        passiveMuffinCountdown -= Time.deltaTime;

        // if the countdown has run out
        if(passiveMuffinCountdown <= 0)
        {
            CollectPassiveMuffins();
            passiveMuffinCountdown = 1f;
        }
    }

    #region save/load

    private void ResetGame()
    {
        Debug.Log("Reset game");
        totalEarnedMuffins = 0;
        muffinsPerClick = 1;
        muffinsPerSecond = 0;
        upgrade1Level.level = 0;
        upgrade2Level.level = 0;
    }

    private void SaveMyData()
    {
        // create the save data object
        SaveData saveData = new SaveData();

        // populate the save data object with the game's current state
        saveData.totalEarnedMuffins = totalEarnedMuffins;
        saveData.muffinsPerClick = muffinsPerClick;
        saveData.muffinsPerSecond = muffinsPerSecond;

        // TODO: save more data
        saveData.upgrade1Level = upgrade1Level.level;
        saveData.upgrade2level = upgrade2Level.level;

        // convert the save data object to JSON
        string saveJSON = JsonUtility.ToJson(saveData);
        Debug.Log("Saving JSON: " + saveJSON);

        // dump the save data into play prefs
        PlayerPrefs.SetString("savedgame", saveJSON);
    }

    private void LoadMyData()
    {
        Debug.Log("Loaded data");

        // load the save game JSON from player prefs
        string saveJSON = PlayerPrefs.GetString("savedgame", "{}");
        Debug.Log("Loading JSON: " + saveJSON);

        // convert the JSON into a SaveData object
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveJSON);

        // restore the game's state from the save filed
        totalEarnedMuffins = saveData.totalEarnedMuffins;
        muffinsPerClick = saveData.muffinsPerClick;
        muffinsPerSecond = saveData.muffinsPerSecond;

        // TODO: load more variables
        upgrade1Level.level = saveData.upgrade1Level;
        upgrade2Level.level = saveData.upgrade2level;
    }

    private void OnApplicationQuit()
    {
        SaveMyData();
    }
    #endregion

    /// <summary>
    /// Add to the total earned muffins
    /// </summary>
    public void AddMuffins(int muffinsToAdd)
    {
        int criticalClicks = Random.Range(0, 99);

        if (criticalClicks == 1)
        {
            totalEarnedMuffins = totalEarnedMuffins + (muffinsToAdd * 10);
        }
        else
        {
            totalEarnedMuffins = totalEarnedMuffins + muffinsToAdd;
        }
    }

    // return true if the player can afford an upgrade, false if the player can't. 
    public bool TryGetPurchaseUpgrade(UpgradeType type, int price)
    {
        // if the player CANT afford the upgrade
        if(totalEarnedMuffins < price)
        {
            // bails the method, returns false so the upgrade doesn't increase
            return false;
        }

        // take the player's muffins
        totalEarnedMuffins -= price;

        // TODO: apply the upgrade
        //if(type == UpgradeType.MuffinsPerClick)
        //{
        //    // increase the number of muffins
        //    muffinsPerClick += 1;
        //}
        //else if(type == UpgradeType.MuffinsPerSecond)
        //{
        //    // do something here
        //}

        switch (type)
        {
            case UpgradeType.MuffinsPerClick:
                // apply the upgrade
                muffinsPerClick += 1;
                break;
            case UpgradeType.MuffinsPerSecond:
                // apply the upgrade
                muffinsPerSecond += 1;
                break;
        }

        // return true so the upgrade level increases;
        return true;
    }
}
