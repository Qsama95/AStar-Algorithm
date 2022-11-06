using Pathing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AStarNodeView : MonoBehaviour, IAStarNode
{
    [SerializeField] private LayerMask _neighbourLayerMask;
    [SerializeField] private Transform _nodeOrigin;
    [SerializeField] private AStarNodeController _aStarNodeController;

    #region Public Properties
    public AStarNode AStarNode { get; set; }
    public bool IsSelected;

    public Vector3 InitialPos { get => _initialPos; set => _initialPos = value; }
    public AStarNodeView StarNodeUpLeft { get => _starNodeUpLeft; set => _starNodeUpLeft = value; }
    public AStarNodeView StarNodeUpRight { get => _starNodeUpRight; set => _starNodeUpRight = value; }
    public AStarNodeView StarNodeLeft { get => _starNodeLeft; set => _starNodeLeft = value; }
    public AStarNodeView StarNodeRight { get => _starNodeRight; set => _starNodeRight = value; }
    public AStarNodeView StarNodeLowLeft { get => _starNodeLowLeft; set => _starNodeLowLeft = value; }
    public AStarNodeView StarNodeLowRight { get => _starNodeLowRight; set => _starNodeLowRight = value; }
    #endregion

    #region Private Properties
    private List<AStarNodeView> _neighbours = new List<AStarNodeView>();
    private Vector3 _initialPos;
    private AStarNodeView _starNodeUpLeft;
    private AStarNodeView _starNodeUpRight;
    private AStarNodeView _starNodeLeft;
    private AStarNodeView _starNodeRight;
    private AStarNodeView _starNodeLowLeft;
    private AStarNodeView _starNodeLowRight;

    private RaycastHit hitUpLeft;
    private RaycastHit hitUpRight;
    private RaycastHit hitLeft;
    private RaycastHit hitRight;
    private RaycastHit hitLowLeft;
    private RaycastHit hitLowRight;
    #endregion

    private void Start()
    {
        _initialPos = transform.position;
    }

    private void OnDestroy()
    {
    }

    private void SetSelfColliderActive(bool active)
    {
        GetComponent<Collider>().enabled = active;
    }

    private void OnSelected(bool isSelected)
    {
        if (isSelected)
        {
            transform.position += Vector3.up * 0.1f;
        }
        else
        {
            transform.position = _initialPos;
        }
        IsSelected = isSelected;
    }

    public void InitialzeAStarNodeNeighbours()
    {
        if (_starNodeUpLeft) _neighbours.Add(_starNodeUpLeft);
        if (_starNodeUpRight) _neighbours.Add(_starNodeUpRight);
        if (_starNodeLeft) _neighbours.Add(_starNodeLeft);
        if (_starNodeRight) _neighbours.Add(_starNodeRight);
        if (_starNodeLowLeft) _neighbours.Add(_starNodeLowLeft);
        if (_starNodeLowRight) _neighbours.Add(_starNodeLowRight);
    }

    public void RegisterActions()
    {
    }

    public void SelectAction(bool isSelected)
    {
        OnSelected(isSelected);
    }

    // Search 6 directions with corresponding distance and check if there is a hit on AStarNode
    public void SearchForNeighbours()
    {
        // disable collider on itself
        SetSelfColliderActive(false);

        float distanceOnX = _aStarNodeController.DistanceOnX;
        float distanceOnDiagnal = _aStarNodeController.DistanceOnDiagnal;

        if (Physics.Raycast(_nodeOrigin.position, (-_nodeOrigin.right + _nodeOrigin.forward).normalized, out hitUpLeft, distanceOnDiagnal, _neighbourLayerMask))
        {
            _starNodeUpLeft = hitUpLeft.transform.GetComponent<AStarNodeView>();
            //NeighbourNodeViews.Add(_starNodeUpLeft);
            //Debug.Log("hit a neighbour on up left: " + hitUpLeft.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, (_nodeOrigin.right + _nodeOrigin.forward).normalized, out hitUpRight, distanceOnDiagnal, _neighbourLayerMask))
        {
            _starNodeUpRight = hitUpRight.transform.GetComponent<AStarNodeView>();
            //NeighbourNodeViews.Add(StarNodeUpRight);
            //Debug.Log("hit a neighbour on up right: " + hitUpRight.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, -_nodeOrigin.right, out hitLeft, distanceOnX, _neighbourLayerMask))
        {
            _starNodeLeft = hitLeft.transform.GetComponent<AStarNodeView>();
            //NeighbourNodeViews.Add(_starNodeLeft);
            //Debug.Log("hit a neighbour on left: " + hitLeft.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, _nodeOrigin.right, out hitRight, distanceOnX, _neighbourLayerMask))
        {
            _starNodeRight = hitRight.transform.GetComponent<AStarNodeView>();
            //NeighbourNodeViews.Add(_starNodeRight);
            //Debug.Log("hit a neighbour on right: " + hitRight.transform.GetComponent<AStarNodeView>().AStarNode);  
        }
        if (Physics.Raycast(_nodeOrigin.position, -(_nodeOrigin.right + _nodeOrigin.forward).normalized, out hitLowLeft, distanceOnDiagnal, _neighbourLayerMask))
        {
            _starNodeLowLeft = hitLowLeft.transform.GetComponent<AStarNodeView>();
            //NeighbourNodeViews.Add(_starNodeLowLeft);
            //Debug.Log("hit a neighbour on low left: " + hitLowLeft.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, (_nodeOrigin.right - _nodeOrigin.forward).normalized, out hitLowRight, distanceOnDiagnal, _neighbourLayerMask))
        {
            _starNodeLowRight = hitLowRight.transform.GetComponent<AStarNodeView>();
            //NeighbourNodeViews.Add(_starNodeLowRight);
            //Debug.Log("hit a neighbour on low right: " + hitLowRight.transform.GetComponent<AStarNodeView>().AStarNode);
        }

        // enable collider on itself
        SetSelfColliderActive(true);
    }

    #region IAStarNode Inheritance
    public IEnumerable<IAStarNode> Neighbours
    {
        get { return _neighbours; }
    }

    public float CostTo(IAStarNode neighbour)
    {
        return AStarNode.TravelCost;
    }

    public float EstimatedCostTo(IAStarNode goal)
    {
        // world distance from the current node to end node
        if (!goal.Equals(this))
        {
            return AStarNode.TravelCost + Vector3.Distance(_initialPos, _aStarNodeController.EndNode.InitialPos);
        }
        return 0f;
    }
    #endregion

    #region Visualization
    void OnDrawGizmos()
    {
        if (hitLeft.transform)
        {
            DrawArrow.ForGizmo(transform.position, (hitLeft.point - transform.position), Color.red);
        }
        if (hitRight.transform)
        {
            DrawArrow.ForGizmo(transform.position, (hitRight.point - transform.position), Color.red);
        }
        if (hitUpLeft.transform)
        {
            DrawArrow.ForGizmo(transform.position, (hitUpLeft.point - transform.position), Color.red);
        }
        if (hitUpRight.transform)
        {
            DrawArrow.ForGizmo(transform.position, (hitUpRight.point - transform.position), Color.red);
        }
        if (hitLowLeft.transform)
        {
            DrawArrow.ForGizmo(transform.position, (hitLowLeft.point - transform.position), Color.red);
        }
        if (hitLowRight.transform)
        {
            DrawArrow.ForGizmo(transform.position, (hitLowRight.point - transform.position), Color.red);
        }

        if (IsSelected)
        {
            // Display the selected node
            Gizmos.color = new Color(1, 1, 0, 0.75F);
            Gizmos.DrawWireMesh(AStarNode.NodeHexagonModelMesh, transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {

    }
    #endregion
}
