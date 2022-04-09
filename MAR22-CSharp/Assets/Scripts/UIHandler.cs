using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handle updating the UI of the game
/// Showing and changing the muffin amount text in the header
/// </summary>
public class UIHandler : MonoBehaviour
{
    public TMP_Text muffinText;
    public TMP_Text muffinPerSecondText;
    
    void Start()
    {
        UpdateMuffinAmountText();
    }
    private void Update()
    {
        UpdateMuffinAmountText();
    }

    public void UpdateMuffinAmountText()
    {
        if (GameManager.instance.totalEarnedMuffins == 1)
        {
            muffinText.text = GameManager.instance.totalEarnedMuffins.ToString() + " muffin";
        }
        else
        {
            muffinText.text = GameManager.instance.totalEarnedMuffins.ToString() + " muffins";
        }

        // update the number of muffins per second text
        muffinPerSecondText.text = $"{GameManager.instance.muffinsPerSecond} {(GameManager.instance.muffinsPerSecond == 1 ? "muffin" : "muffins")} / sec ";
    }
}
