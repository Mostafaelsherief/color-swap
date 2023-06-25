using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class InputEventsHandler : MonoBehaviour
{
  public static InputEventsHandler instance; 
  public event Action<Vector3> onDragStarted;
  public event Action<Vector3> onDragPositionChanged;
  public event Action onDragEnded;
  private void Awake()
  {
    instance = this;
  }

  public void StartDrag(Vector3 position)
  {
      onDragStarted?.Invoke(position);
  }

  public void ChangeDragPosition(Vector3 position)
  {
    onDragPositionChanged?.Invoke(position);
  }
  public void EndDrag()
  {
    onDragEnded?.Invoke();
  }
}
