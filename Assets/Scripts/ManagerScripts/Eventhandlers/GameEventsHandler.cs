using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventsHandler : MonoBehaviour
{
  public static GameEventsHandler instance;
  public event Action onGameWon;
  public event Action checkWin;
  public event Action loadNextLevel;

  private void Awake()
  {
    instance = this;
  }

  public void WinGame()
  {
    onGameWon?.Invoke();
  }
  public void CheckWin()
  {
    checkWin?.Invoke();
  }
  public void LoadNextLevel()
  {
    loadNextLevel?.Invoke();
  }
}
