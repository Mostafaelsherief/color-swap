using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] ChildNode childNode;
    float dragPositionThreshold = 0.3f;
    float releasePositionThreshold = 0.3f;
    public List<Node> connectedNodes;
    NodeAnimationController nodeAnimationController;

    private void Awake()
    {
        /*In this version  of the game the nodes are added to a list , so this function simply ensures that the connection 
         * between the nodes is 2 way , of course this isnt the best solution ,a proper LevelEditor would be better to solve this 
         * problem 
         */
        AddCurrentNodeToConnectedNodes();
    }
    private void Start()
    {
        nodeAnimationController = GetComponent<NodeAnimationController>();
        InputEventsHandler.instance.onDragStarted += DragChildNode;
        NodeEventsHandler.instance.onNodeReleased += IsReleasedChildNodeInCurrentNodePosition;
        GameEventsHandler.instance.onGameWon += OnGameWon;
        childNode.CurrentParentNode = this;
    }

    #region ManagingGraphConnections

    void AddCurrentNodeToConnectedNodes()
    {
        foreach(Node node in connectedNodes)
        {
            node.AddNode(this);
        }
    }

    public bool IsNodeInConnectedNodes(Node node)
    {
        return connectedNodes.Contains(node);
    }

    public void AddNode(Node node)
    {
        if (!connectedNodes.Contains(node))
        {
            connectedNodes.Add(node);
        }
    }

    #endregion
    #region Managing ChildNode
    void DragChildNode(Vector3 position)
    {
        if (Vector3.Magnitude(transform.position - position) < dragPositionThreshold)
        {
            // when enlarging the Base node, we dont need the child node to inherit ts scale 
            transform.DetachChildren();
            childNode.gameObject.transform.parent = transform.parent;
            childNode.IsMovable = true;
            nodeAnimationController.StartNodeDragAnimation();      
        }
    }

    public Color GetChildNodeColor()
    {
        return childNode.GetColor();
    }

    public void ChangeChildNode(ChildNode node)
    {
        childNode = node;
        childNode.CurrentParentNode = this;
    }

    void IsReleasedChildNodeInCurrentNodePosition(ChildNode releasedNode)
    {
        // check if the Moved node position is near this node , and check if its parent is connected to this node 
        if (Vector3.Magnitude(releasedNode.transform.position - transform.position) < releasePositionThreshold && IsNodeInConnectedNodes(releasedNode.CurrentParentNode))
        {
            // Swap occurs 
            SwapChildNodes(releasedNode);
            //each time a swap occurs we check if the game is won , instead of doing this every frame
            GameEventsHandler.instance.CheckWin();
        }
        else 
        { 
            // no swap , move towards parent node 
            releasedNode.MoveTowardsParentNode();
        }
    }   
    
    void SwapChildNodes(ChildNode releasedNode)
    {   
        // Swapping 
        Node releasedNodeParent = releasedNode.CurrentParentNode;
        releasedNodeParent.ChangeChildNode(childNode);
        ChangeChildNode(releasedNode);

        // To Move the nodes to their new corresponding Positions
        releasedNodeParent.childNode.MoveTowardsParentNode();
        childNode.MoveTowardsParentNode();
    }

    #endregion 
    void OnGameWon()
    {
        //to prevent any node movement after game is won 
        InputEventsHandler.instance.onDragStarted -= DragChildNode; 
        NodeEventsHandler.instance.onNodeReleased -= IsReleasedChildNodeInCurrentNodePosition;
    }
}
