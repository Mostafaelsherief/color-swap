using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Here I simply Store the levels in a gameobject array and The Current Level is stored with player prefs 
    public GameObject[] levels;

    private void Awake()
    {
        Instantiate(levels[PlayerPrefs.GetInt("CurrentLevel")], levels[PlayerPrefs.GetInt("CurrentLevel")].transform.position, Quaternion.identity);       
    }

    private void Start()
    {
        GameEventsHandler.instance.loadNextLevel += LoadNextLevel;
    }

    void LoadNextLevel()
    {
        if(PlayerPrefs.GetInt("CurrentLevel")==0)
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
        SceneManager.LoadScene("SampleScene");
    }
    


}
