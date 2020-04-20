using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Generates PathMaps through given MoveMaps.
 */
public class Pathing
{
    /*
     * Request a PathMap given a MoveMap and starting position.
     * Early exits a particular path after reaching maxTotalMove.
     */
    public static PathMap RequestPathMap(MoveMap moveMap, Vector2Int startPosition, float maxTotalMove)
    {
        PathMap pathMap = new PathMap (startPosition);

        // 'Priority queue' for tracking which nodes to move forward from
        List<PathNode> frontier = new List<PathNode> () { new PathNode (startPosition, 0) };

        //TODO: Can be simplified by using actual graph nodes instead of Vector2Int coordinates
        //      Neighbors could be gathered simply with 'currNode.neighbors', and be graph shape agnostic
        //      The cost of additional references may or may not outweigh the overhead of vector math below
        Vector2Int[] neighborDirs = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

        while (frontier.Count > 0)
        {
            PathNode curr = frontier[0];
            frontier.RemoveAt (0);

            for (int i = 0; i < neighborDirs.Length; i++)
            {
                Vector2Int next = curr.location + neighborDirs[i];
                
                // Unpathable, ignore completely
                if (moveMap.GetMoveCost (next.x, next.y) == float.PositiveInfinity)
                    continue;

                float totalMoveCost = curr.moveCost + moveMap.GetMoveCost (next.x, next.y);
                // Not enough move left to push frontier
                if (totalMoveCost > maxTotalMove)
                    continue;

                // Either add new node or replace existing values
                if (!pathMap.nodes.ContainsKey (next))
                    pathMap.nodes.Add (next, new PathNode (curr.location, totalMoveCost));
                else if (pathMap.nodes.ContainsKey (next) && pathMap.nodes[next].moveCost > totalMoveCost)
                    pathMap.nodes[next] = new PathNode (curr.location, totalMoveCost);
                // New totalMoveCost isn't good enough to push frontier
                else
                    continue;
                
                frontier.Add (new PathNode (next, totalMoveCost));
            }

            // Would possibly perform better if using a sorted list instead of resorting entire list each time
            frontier.Sort ((n1, n2) => n1.moveCost.CompareTo (n2.moveCost));
        }

        return pathMap;
    }

}
