using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AStarNodeView : MonoBehaviour
{
    [SerializeField] private LayerMask _neighbourLayerMask;
    [SerializeField] private Transform _nodeOrigin;

    public AStarNode AStarNode { get; set; }

    private RaycastHit hitUpLeft;
    private RaycastHit hitUpRight;
    private RaycastHit hitLeft;
    private RaycastHit hitRight;
    private RaycastHit hitLowLeft;
    private RaycastHit hitLowRight;

    public List<Transform> Neighbours;

    public void RegisterActions()
    {

    }

    private void OnDestroy()
    {
        
    }

    private void SetSelfColliderActive(bool active)
    {
        GetComponent<Collider>().enabled = active;
    }

    // Search 6 directions with corresponding distance and check if there is a hit on AStarNode
    public void SearchForNeighbours()
    {
        // disable collider on itself
        SetSelfColliderActive(false);

        if (Physics.Raycast(_nodeOrigin.position, -_nodeOrigin.right, out hitLeft, AStarMap.DistanceOnX, _neighbourLayerMask))
        {
            if (!Neighbours.Contains(hitLeft.transform)) {
            }
                Neighbours.Add(hitLeft.transform);
            //Debug.Log("hit a neighbour on left: " + hitLeft.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, _nodeOrigin.right, out hitRight, AStarMap.DistanceOnX, _neighbourLayerMask))
        {
            if (!Neighbours.Contains(hitRight.transform)) {
            }
                Neighbours.Add(hitRight.transform);
            //Debug.Log("hit a neighbour on right: " + hitRight.transform.GetComponent<AStarNodeView>().AStarNode);  
        }
        if (Physics.Raycast(_nodeOrigin.position, (-_nodeOrigin.right + _nodeOrigin.forward).normalized, out hitUpLeft, AStarMap.DistanceOnDiagnal, _neighbourLayerMask))
        {
            if (!Neighbours.Contains(hitUpLeft.transform)) {
            }
                Neighbours.Add(hitUpLeft.transform);
            //Debug.Log("hit a neighbour on up left: " + hitUpLeft.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, (_nodeOrigin.right + _nodeOrigin.forward).normalized, out hitUpRight, AStarMap.DistanceOnDiagnal, _neighbourLayerMask))
        {
            if (!Neighbours.Contains(hitUpRight.transform)) {
            }
                Neighbours.Add(hitUpRight.transform);
            //Debug.Log("hit a neighbour on up right: " + hitUpRight.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, -(_nodeOrigin.right + _nodeOrigin.forward).normalized, out hitLowLeft, AStarMap.DistanceOnDiagnal, _neighbourLayerMask))
        {
            if (!Neighbours.Contains(hitLowLeft.transform)) {
            }
                Neighbours.Add(hitLowLeft.transform);
            //Debug.Log("hit a neighbour on low left: " + hitLowLeft.transform.GetComponent<AStarNodeView>().AStarNode);
        }
        if (Physics.Raycast(_nodeOrigin.position, (_nodeOrigin.right - _nodeOrigin.forward).normalized, out hitLowRight, AStarMap.DistanceOnDiagnal, _neighbourLayerMask))
        {
            if (!Neighbours.Contains(hitLowRight.transform)) {
            }
                Neighbours.Add(hitLowRight.transform);
            //Debug.Log("hit a neighbour on low right: " + hitLowRight.transform.GetComponent<AStarNodeView>().AStarNode);
        }

        // enable collider on itself
        SetSelfColliderActive(true);
    }

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
    }
}
