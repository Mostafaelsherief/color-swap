using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NodeEventsHandler : MonoBehaviour
{
  public static NodeEventsHandler instance;
  public event Action<ChildNode> onNodeReleased;

  private void Awake()
  {
    instance = this;
  }


  public void ReleaseNode(ChildNode childNode)
  {
    onNodeReleased?.Invoke(childNode);
  }
}
