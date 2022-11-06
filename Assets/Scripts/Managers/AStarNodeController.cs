using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AStarNodeController", menuName = "AStar Node Controller")]
public class AStarNodeController : ScriptableObject
{
    public float DistanceOnX = 1f;
    public float DistanceOnZ = 0.75f;
    public float DistanceOnDiagnal = 1.25f;

    public List<AStarNodeView> AllAStarNodes = new List<AStarNodeView>();

    public AStarNodeView EndNode;

    #region Public Calls from MapGenerator & AStarNodeSelector
    public void SetClickedNodeState(AStarNodeView clickedNode, bool setSelectionState)
    {
        clickedNode.SelectAction(setSelectionState);
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
            nodeView.SelectAction(false);
        }
    }
    #endregion
}
