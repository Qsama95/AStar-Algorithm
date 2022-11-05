using Pathing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "AStarNode", menuName = "AStar Component")]
public class AStarNode : ScriptableObject, IAStarNode
{
    [Header("Node Self Properties")]
    [SerializeField] private GameObject _nodeHexagonModel;
    [SerializeField] private Texture _nodeTexture;
    [SerializeField] private float _travelCost;

    [Header("Node Neighbours")]
    [SerializeField] private AStarNode _starNodeUpLeft;
    [SerializeField] private AStarNode _starNodeUpRight;
    [SerializeField] private AStarNode _starNodeLeft;
    [SerializeField] private AStarNode _starNodeRight;
    [SerializeField] private AStarNode _starNodeLowLeft;
    [SerializeField] private AStarNode _starNodeLowRight;

    #region Private Properties
    private List<IAStarNode> _neighbours = new List<IAStarNode>();
    private Vector3 _worldPos;
    #endregion

    #region Public Properties
    public Vector3 WorldPos
    {
        get
        {
            return _worldPos;
        }
        set
        {
            _worldPos = value;
        }
    }
    #endregion

    #region Public Methods
    public void InstantiateNodeOnMap(Vector3 pos)
    {
        GameObject nodeSelf = Instantiate(_nodeHexagonModel, WorldPos = pos, Quaternion.identity);
        nodeSelf.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", _nodeTexture);
        nodeSelf.GetComponent<AStarNodeView>().AStarNode = this;

        AStarMap.AllAStarNodes.Add(nodeSelf.GetComponent<AStarNodeView>());
    }
    #endregion

    private void InitialzeAStarNodeNeighbours()
    {
        _neighbours.Add(_starNodeUpLeft);
        _neighbours.Add(_starNodeUpRight);
        _neighbours.Add(_starNodeLeft);
        _neighbours.Add(_starNodeRight);
        _neighbours.Add(_starNodeLowLeft);
        _neighbours.Add(_starNodeLowRight);
    }

    #region IAStarNode Inheritance
    public IEnumerable<IAStarNode> Neighbours
    {
        get { return _neighbours; }
    }

    public float CostTo(IAStarNode neighbour)
    {
        return _travelCost;
    }

    public float EstimatedCostTo(IAStarNode goal)
    {
        // world distance from the current node to end node
        if (!goal.Equals(this))
        {
            return _travelCost + Vector3.Distance(_worldPos, _worldPos);
        }
        return 0f;
    }
    #endregion
}
