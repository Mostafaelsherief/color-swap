using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildNode : MonoBehaviour
{
    bool _isMovable;
    Node currentParentNode;
    Color _nodeColor;

    public bool IsMovable { get => _isMovable; set => _isMovable = value; }
    public Node CurrentParentNode { get => currentParentNode; set => currentParentNode = value; }

    private void Start()
    {
        InputEventsHandler.instance.onDragPositionChanged += MoveTowardsDragPosition;
        InputEventsHandler.instance.onDragEnded += ReleaseNode;
        _nodeColor = GetComponent<SpriteRenderer>().color;
        Debug.Log(_nodeColor);
  }

    public Color GetColor()
    {
        return _nodeColor;
    }

    // This Moves 
    void MoveTowardsDragPosition(Vector3 position)
    {
        if(IsMovable)
        {
            LeanTween.cancel(gameObject);
            transform.position = Vector3.MoveTowards(transform.position,position,10*Time.fixedDeltaTime);
        }
    }
    
    // This sends an event that this node is released , all the nodes listen to this event to determine of this 
    //child node is to be swapped depending on its position and on the connected nodes to the affected node 
    void ReleaseNode()
    {
        if (_isMovable)
        {
            _isMovable = false;
            NodeEventsHandler.instance.ReleaseNode(this);
        }
    }

    public void MoveTowardsParentNode()
    {
      LeanTween.move(gameObject, currentParentNode.transform.position, 1).setEaseOutBounce();
    }
}
