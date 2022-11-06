using Pathing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNodeSelector : MonoBehaviour
{
    [SerializeField] private LayerMask _aStarNodeLayerMask;
    [SerializeField] private AStarNodeController _aStarNodeController;

    private AStarNodeView _startNode;
    private AStarNodeView _endNode;
    private AStarNodeView _mouseOnNode;

    private RaycastHit _hitOnNode;
    private Camera _mainCam;

    #region Private Methods
    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        DetectingAStarNode();
    }

    private void DetectingAStarNode()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hitOnNode, Mathf.Infinity, _aStarNodeLayerMask))
        {
            AStarNodeView detectedNode = _hitOnNode.transform.GetComponent<AStarNodeView>();
            // mouse hover
            // first detection
            if (!_mouseOnNode)
            {
                SetHighlightedNode(detectedNode, false);
            }

            // update detection
            if (_mouseOnNode != detectedNode)
            {
                SetHighlightedNode(detectedNode, true);
            }

            // mouse click
            if (Input.GetMouseButtonDown(0))
            {
                AStarNodeView clickedNode = _hitOnNode.transform.GetComponent<AStarNodeView>();
                Debug.Log("click on node: " + clickedNode.AStarNode);

                // avoid specific node type
                if (!IsNodeSelectable(clickedNode))
                {
                    Debug.Log("Clicked node can not be selected! Please reselect a node!");
                    return;
                }

                ClickOnSelectableNode(clickedNode);
                FindPath();
            }
        }
        else
        {
            // clear detection
            if (!_mouseOnNode) return;
            SetHighlightedNode(_mouseOnNode, false);
        }
    }

    private void FindPath()
    {
        if (IsStartNodeSelected() && IsEndNodeSelected())
        {
            IList<IAStarNode> path = AStar.GetPath(_startNode, _endNode);
            Debug.Log("path node count: " + path.Count);
            VisualizePath(path);
        }
    }

    private void VisualizePath(IList<IAStarNode> path)
    {
        path.Remove(_startNode);
        path.Remove(_endNode);
        _aStarNodeController.VisualizeNodesOnPath(path);
    }

    private void RestartNodeSelection()
    {
        _aStarNodeController.DeselectAllNodes();
        _aStarNodeController.EndNode = null;
        _startNode = null;
        _endNode = null;
    }

    private void ClickOnSelectableNode(AStarNodeView clickedNode)
    {
        // restart selection
        if (IsStartNodeSelected() && IsEndNodeSelected())
        {
            RestartNodeSelection();
        }

        // deselect the selected node and set its start or end node ref to null
        if (clickedNode.IsSelected())
        {
            SetClickedNodeState(clickedNode, false);
            if (_startNode == clickedNode)
            {
                _startNode = null;
            }
            else
            {
                _endNode = null;
                _aStarNodeController.EndNode = _endNode;
            }
        }
        // set clicked node as start or end node
        else
        {
            SetClickedNodeState(clickedNode, true);
            if (!IsStartNodeSelected())
            {
                _startNode = clickedNode;
            }
            else
            {
                _endNode = clickedNode;
                _aStarNodeController.EndNode = _endNode;
            }
        }
    }

    private void SetClickedNodeState(AStarNodeView clickedNode, bool setSelectionState)
    {
        _aStarNodeController.SetClickedNodeState(clickedNode, setSelectionState);
    }

    private bool IsNodeSelectable(AStarNodeView clickedNode)
    {
        return _aStarNodeController.IsNodeSelectable(clickedNode);
    }

    private bool IsStartNodeSelected()
    {
        if (_startNode != null)
        {
            return true;
        }
        return false;
    }

    private bool IsEndNodeSelected()
    {
        if (_endNode != null)
        {
            return true;
        }
        return false;
    }

    private void SetHighlightedNode(AStarNodeView detectedNode, bool isUpdatingNode)
    {
        if (isUpdatingNode) _aStarNodeController.SetNodeHighlighted(_mouseOnNode, false);
        _mouseOnNode = detectedNode;
        _aStarNodeController.SetNodeHighlighted(_mouseOnNode, true);
    }
    #endregion
}
