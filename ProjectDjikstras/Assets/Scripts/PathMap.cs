using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Storage class for all the PathNodes and positions relevant to a pathing request.
 */
public class PathMap
{
    // Keys all visited nodes in this path request
    // Values are the previous node in the key location's path as well as the total move cost to the key's location
    public Dictionary<Vector2Int, PathNode> nodes;


    public PathMap(Vector2Int startPosition)
    {
        nodes = new Dictionary<Vector2Int, PathNode> ();
        nodes.Add (startPosition, new PathNode (startPosition, 0));
    }
    
}
