using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField]GameObject loadNextLevelText;
    [SerializeField]GameObject encouragingWordsText;
    [SerializeField]TextMeshProUGUI levelText;
    
    void Start()
    {
        GameEventsHandler.instance.onGameWon += ActivateWinUI;
        levelText.text = "Level " + (PlayerPrefs.GetInt("CurrentLevel")+1);
    }
    void ActivateWinUI()
    {
        loadNextLevelText.SetActive(true);
        encouragingWordsText.SetActive(true);
    }
    public void LoadNextLevel()
    {
        GameEventsHandler.instance.LoadNextLevel();
    }
}
