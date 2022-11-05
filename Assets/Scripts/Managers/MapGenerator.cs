using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static AStarNodeMap;

public class MapGenerator : MonoBehaviour
{
    /// node enum parameters and corresponding names
    //  Desert = 1 = "d",
    //  Forest = 2 = "f",
    //  Grass = 3 = "g",
    //  Mountain = 4 = "m",
    //  Water = 5 = "w"

    [Header("Node Types")]
    [SerializeField]
    private AStarNode _desetNode;
    [SerializeField]
    private AStarNode _forestNode;
    [SerializeField]
    private AStarNode _grassNode;
    [SerializeField]
    private AStarNode _mountainNode;
    [SerializeField]
    private AStarNode _waterNode;

    [Header("Map Properties")]
    [SerializeField]
    private AStarNodeMap _nodeMap;
    [SerializeField]
    private float _distanceOnX;
    [SerializeField]
    private float _distanceOnZ;
    [SerializeField]
    private Vector3 _startPos = Vector3.zero;

    private float _distanceOnDiagnal;
    private List<NodeTypeRow> _nodeTypeMap = new List<NodeTypeRow>();

    public float DistanceOnX { get => _distanceOnX; set => _distanceOnX = value; }
    public float DistanceOnZ { get => _distanceOnZ; set => _distanceOnZ = value; }
    public float DistanceOnDiagnal { get => _distanceOnDiagnal; set => _distanceOnDiagnal = value; }

    private void Awake()
    {
        InitProperties();
        GenerateNodeMap();
        FindNeighboursForNodes();
    }

    private void InitProperties()
    {
        _distanceOnDiagnal = Mathf.Sqrt(_distanceOnX * _distanceOnX + _distanceOnZ * _distanceOnZ);
        AStarMap.DistanceOnX = _distanceOnX;
        AStarMap.DistanceOnZ = _distanceOnZ;
        AStarMap.DistanceOnDiagnal = _distanceOnDiagnal;

        _nodeMap.TranslateNodeNamesInNodeMapToNodeTypes();
        _nodeTypeMap = _nodeMap.GetNodeTypeMap();
    }

    private void GenerateNodeMap()
    {
        int maxRow = _nodeTypeMap.Count;
        for (int r = 0; r < maxRow; r++)
        {
            int maxColumm = _nodeTypeMap[r].NodeTypes.Count;
            bool offsetOnX = !(r % 2 == 0);

            for (int c = 0; c < maxColumm; c++)
            {
                float realDistanceOnX = _distanceOnX * c;
                float realDistanceOnZ = _distanceOnZ * r;

                // check if the line is odd or even, if it is even, then shift to right with a bias
                if (offsetOnX) realDistanceOnX += 0.5f;

                Vector3 posToInstantiate = _startPos + new Vector3(realDistanceOnX, 0, -realDistanceOnZ);
                AStarNodeType currentNodeType = _nodeTypeMap[r].NodeTypes[c];

                switch (currentNodeType)
                {
                    case AStarNodeType.Desert:
                        _desetNode.InstantiateNodeOnMap(posToInstantiate);
                        break;

                    case AStarNodeType.Forest:
                        _forestNode.InstantiateNodeOnMap(posToInstantiate);
                        break;

                    case AStarNodeType.Grass:
                        _grassNode.InstantiateNodeOnMap(posToInstantiate);
                        break;

                    case AStarNodeType.Mountain:
                        _mountainNode.InstantiateNodeOnMap(posToInstantiate);
                        break;

                    case AStarNodeType.Water:
                        _waterNode.InstantiateNodeOnMap(posToInstantiate);
                        break;
                }
            }
        }
    }

    private void FindNeighboursForNodes()
    {
        foreach (AStarNodeView node in AStarMap.AllAStarNodes)
        {
            node.SearchForNeighbours();
        }
    }
}

public static class AStarMap
{
    public static float DistanceOnX = 1f;
    public static float DistanceOnZ = 0.75f;
    public static float DistanceOnDiagnal = 1.25f;

    public static List<AStarNodeView> AllAStarNodes = new List<AStarNodeView>();
}
