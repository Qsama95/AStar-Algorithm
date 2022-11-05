using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AStarNodeMap", menuName = "AStar Node Map")]
public class AStarNodeMap : ScriptableObject
{
    /// <summary>
    /// node int and names
    /// </summary>
    //  Desert = 1 = "d",
    //  Forest = 2 = "f",
    //  Grass = 3 = "g",
    //  Mountain = 4 = "m",
    //  Water = 5 = "w"

    [System.Serializable]
    public class NodeNameRow
    {
        public List<string> NodeNames = new List<string>();
    }

    public class NodeTypeRow
    {
        public List<AStarNodeType> NodeTypes = new List<AStarNodeType>();
    }

    public List<NodeTypeRow> GetNodeTypeMap()
    {
        return _nodeTypeMap;
    }

    /// <summary>
    /// construct the node map with rows and in each NodeNameRow, enter the node names in this row
    /// </summary>
    [SerializeField] private List<NodeNameRow> _nodeNameMap = new List<NodeNameRow>();
    [SerializeField] private List<NodeTypeRow> _nodeTypeMap = new List<NodeTypeRow>();

    // initialize node type map
    public void TranslateNodeNamesInNodeMapToNodeTypes()
    {
        foreach (NodeNameRow row in _nodeNameMap)
        {
            NodeTypeRow nodeTypeRow = new NodeTypeRow();
            foreach (string nodeName in row.NodeNames)
            {
                switch (nodeName)
                {
                    case "d":
                        nodeTypeRow.NodeTypes.Add(AStarNodeType.Desert);
                        break;
                    case "f":
                        nodeTypeRow.NodeTypes.Add(AStarNodeType.Forest);
                        break;
                    case "g":
                        nodeTypeRow.NodeTypes.Add(AStarNodeType.Grass);
                        break;
                    case "m":
                        nodeTypeRow.NodeTypes.Add(AStarNodeType.Mountain);
                        break;
                    case "w":
                        nodeTypeRow.NodeTypes.Add(AStarNodeType.Water);
                        break;
                }
            }
            _nodeTypeMap.Add(nodeTypeRow);
        }
    }
}

