using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/*
 * Requests a new PathMap and builds the visual representation of it.
 * Visuals include path and total move cost to get to the nodes.
 */
public class RequestPathingExample : MonoBehaviour
{
    [Tooltip("Move map to path through")]
    public MoveMap moveMap;
    [Tooltip("Visualizer for building what the move map looks like")]
    public MoveMapVisualizer moveMapVisualizer;

    [Tooltip("Position within move map to start pathing from (index starts at 0)")]
    public Vector2Int startPosition;
    [Tooltip("Allowed total move cost along a given path")]
    public float maxTotalMove;

    [Tooltip("Offset so move cost feedback doesn't overlap as badly with path lines")]
    public Vector3 moveCostTextOffset;


    private PathMap currPathMap;


    [ContextMenu ("Request path map")]
    public void RequestPathMap()
    {
        currPathMap = Pathing.RequestPathMap (moveMap, startPosition, maxTotalMove);
        SceneView.RepaintAll ();
    }

    public void ResetCurrMap()
    {
        currPathMap = null;
    }


    private void OnDrawGizmos()
    {
        if (currPathMap == null)
            return;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;

        // Draw weights
        foreach (Vector2Int key in currPathMap.nodes.Keys.ToList ())
        {
            PathNode currNode = currPathMap.nodes[key];

            Vector3 middlePos = transform.position + new Vector3 (key.x, key.y, 0);
            Vector3 fromPos = transform.position + new Vector3 (currNode.location.x, currNode.location.y, 0);

            // Total move cost
            Handles.Label(middlePos + moveCostTextOffset, $"{currNode.moveCost}", style);

            // Path between node and its previous node
            Handles.color = Color.blue;
            Handles.DrawLine (middlePos, fromPos);
        }
            
    }

}
