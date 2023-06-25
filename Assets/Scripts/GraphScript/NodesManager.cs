using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesManager : MonoBehaviour
{
    Node[] nodes;
    public LineRenderer line;
    public delegate bool GraphInnerNodeFunction(Node currentNode, Node node);

    private void Start()
    {
        nodes = FindObjectsOfType<Node>();
        GameEventsHandler.instance.checkWin += CheckWin;
        GraphInnerNodeFunction drawLinesbetweenNodes = DrawLines;
        TraverseNodes(DrawLines);
    }

    public bool CheckForConsecutiveColoredNodes(Node currentNode, Node node)
    {
        if (currentNode.GetChildNodeColor() == node.GetChildNodeColor())
        {
            return false;
        }
      //  Debug.Log(currentNode.name + "And " +node.name  + "Different Colors ");
        return true;
    }


    // This Function Draws a Line between Two nodes 
    public bool DrawLines(Node currentNode, Node node)
    {
        LineRenderer lineObject = Instantiate(line, Vector3.zero, Quaternion.identity);
        Vector3[] points = new[] { currentNode.transform.position, node.transform.position };
        // To prevent it from overlapping with out objects
        lineObject.transform.position += Vector3.forward * 4;
        lineObject.GetComponent<LineRenderer>();
        lineObject.SetPositions(points);
        lineObject.transform.parent = currentNode.transform.parent;
        return true;
    }

    /*
     * we need to get any similar colored connection , this can be applied 
     * using graph traversal techniques:
     * Add neighbours to stack 
     * mark visited nodes 
     * pop visited neighbours from to visit stack
     * in case of similar colored connection return false
     * repeat until stack is empty 
     */

    bool TraverseNodes(GraphInnerNodeFunction function)
    {
        Stack<Node> toVisit = new Stack<Node>();
        List<Node> visited = new List<Node>();
        toVisit.Push(nodes[0]);
        while (toVisit.Count > 0)
        {
            Node currentNode = toVisit.Pop();
            foreach (Node node in currentNode.connectedNodes)
            {
                if (!visited.Contains(node))
                {
                  toVisit.Push(node);
        
                   if (!function(currentNode, node))
                    {
                        return false;
                    }
                }
            }
            visited.Add(currentNode);
            
        }
        return true;
    }

    void CheckWin()
    {
        if (TraverseNodes(CheckForConsecutiveColoredNodes))
        {
            GameEventsHandler.instance.WinGame();
        }
    }
}
