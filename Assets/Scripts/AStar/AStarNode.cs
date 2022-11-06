using Pathing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "AStarNode", menuName = "AStar Component")]
public class AStarNode : ScriptableObject
{
    [Header("Node Self Properties")]
    // internal use
    [SerializeField] private GameObject _nodeHexagonModel;
    [SerializeField] private Texture _nodeTexture;
    [SerializeField] private AStarNodeController _aStarNodeController;

    // external use
    [SerializeField] private bool _isSelectable;
    [SerializeField] private float _travelCost;
    [SerializeField] private Mesh _nodeHexagonModelMesh;

    #region Public Properties
    public float TravelCost { get => _travelCost; set => _travelCost = value; }
    public Mesh NodeHexagonModelMesh { get => _nodeHexagonModelMesh; set => _nodeHexagonModelMesh = value; }
    public bool IsSelectable { get => _isSelectable; set => _isSelectable = value; }
    #endregion

    #region Public Methods
    public void InstantiateNodeOnMap(Vector3 pos)
    {
        GameObject nodeSelf = Instantiate(_nodeHexagonModel, pos, Quaternion.identity);
        nodeSelf.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", _nodeTexture);

        AStarNodeView nodeView = nodeSelf.GetComponent<AStarNodeView>();
        nodeView.AStarNode = this;
        _aStarNodeController.AllAStarNodes.Add(nodeView);
    }
    #endregion
}
