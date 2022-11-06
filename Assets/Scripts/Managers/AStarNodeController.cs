using Pathing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AStarNodeController", menuName = "AStar Node Controller")]
public class AStarNodeController : ScriptableObject
{
    public float DistanceOnX { get; set; }
    public float DistanceOnZ { get; set; }
    public float DistanceOnDiagnal { get; set; }
    public List<AStarNodeView> AllAStarNodes { get; set; }
    public AStarNodeView EndNode { get; set; }

    #region Public Calls from MapGenerator & AStarNodeSelector
    public void SetNodeHighlighted(AStarNodeView clickedNode, bool setHighlighted)
    {
        clickedNode.SetHighlighted(setHighlighted);
    }

    public void SetClickedNodeState(AStarNodeView clickedNode, bool setSelectionState)
    {
        clickedNode.SetSelected(setSelectionState, true);
    }

    public bool IsNodeSelectable(AStarNodeView clickedNode)
    {
        return clickedNode.AStarNode.IsSelectable;
    }

    public void CleanAStarNodes()
    {
        AllAStarNodes.Clear();
    }

    public void FindNeighboursForNodes()
    {
        foreach (AStarNodeView nodeView in AllAStarNodes)
        {
            nodeView.SearchForNeighbours();
        }
    }

    public void InitializeNodeNeighbours()
    {
        foreach (AStarNodeView nodeView in AllAStarNodes)
        {
            nodeView.InitialzeAStarNodeNeighbours();
        }
    }

    public void DeselectAllNodes()
    {
        foreach (AStarNodeView nodeView in AllAStarNodes)
        {
            nodeView.SetSelected(false);
        }
    }

    public void VisualizeNodesOnPath(IList<IAStarNode> path)
    {
        foreach (AStarNodeView nodeOnPath in path)
        {
            nodeOnPath.SetSelected(true);
        }
    }
    #endregion
}
