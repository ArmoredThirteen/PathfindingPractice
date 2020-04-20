using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Ties a location with a move cost which can be used primarily in two ways.
 * One is for individual nodes' locations and their move costs.
 * The other is to tie together the previous node and the path's total move cost.
 */
public class PathNode
{
    public Vector2Int location;
    public float moveCost;

    public PathNode(Vector2Int theNode, float theMoveCost)
    {
        location = theNode;
        moveCost = theMoveCost;
    }

}
