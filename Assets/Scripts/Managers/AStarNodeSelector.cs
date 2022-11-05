using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNodeSelector : MonoBehaviour
{
    [SerializeField] private LayerMask _aStarNodeLayerMask;

    private AStarNode _startNode;
    private AStarNode _endNode;

    private RaycastHit _hitOnNode;
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        DetectingAStarNode();
    }

    private void DetectingAStarNode()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hitOnNode, Mathf.Infinity, _aStarNodeLayerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("click on node: " + _hitOnNode.transform.GetComponent<AStarNodeView>().AStarNode);
                AStarNodeView selectedNode = _hitOnNode.transform.GetComponent<AStarNodeView>();

            }
        }
    }

    private void RestartNodeSelection()
    {

    }

    private void SelectNode(AStarNode aStarNode)
    {

    }

    private void DeselectNode(AStarNode aStarNode)
    {

    }
}
