using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum UpgradeType
{
    MuffinsPerClick,
    MuffinsPerSecond,
}

public class UpgradeButton : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text priceText;
    public int pricePerLevel;
    public UpgradeType upgradeType;
    public int level;

    private int price;
    private GameManager gameManager;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
        // update the level text UI
        levelText.text = level.ToString();

        // calculate the current price of the upgrade
        // how much it's going to cost to buy the next level up
        price = (level+1) * pricePerLevel;

        // update the price text UI
        priceText.text = price.ToString();

        // color the price text according to whether the player can afford it (green if the player can, red if the player can't)
        /*
        if(gameManager.totalEarnedMuffins >= price)
        {
            priceText.color = Color.green;
        }
        else
        {
            priceText.color = Color.red;
        }
        */

        // Turnary Operator
        // <CONDITION> ? <TRUE VALUE> : <FALSE VALUE>>
        priceText.color = gameManager.totalEarnedMuffins >= price ? Color.green : Color.red;

    }

    public void OnUpgradeButtonClicked()
    {
        if (gameManager.TryGetPurchaseUpgrade(upgradeType, price))
        {
            // if true, increase the upgrade level
            level++;
        }
    }
}
