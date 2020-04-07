using ATE.Terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Pathing.Dijkstra
{
    public class PathBuilder
	{
        public class MoveCostNode
        {
            public TerrainNode node;
            public float totalMoveCost;

            public MoveCostNode(TerrainNode theNode, float theMoveCost)
            {
                node = theNode;
                totalMoveCost = theMoveCost;
            }
        }

        public bool complete;

        public TerrainNode from;
        public TerrainNode to;

        public List<TerrainNode> visited;
        public List<MoveCostNode> frontier;

        public Dictionary<int, MoveCostNode> currPath;
        public List<TerrainNode> finalPath;


        public void RestartPathing(TerrainNode fromNode, TerrainNode toNode)
        {
            complete = false;

            from = fromNode;
            to = toNode;

            visited = new List<TerrainNode> ();
            frontier = new List<MoveCostNode> ();

            currPath = new Dictionary<int, MoveCostNode> ();
            finalPath = new List<TerrainNode> ();

            visited.Add (from);
            frontier.Add (new MoveCostNode (from, 0));
        }
        
        // Returns true when pathing is complete
        public void IteratePath()
        {
            if (frontier.Count <= 0)
            {
                Debug.Log ("Frontier is empty");
                complete = true;
                return;
            }
            
            MoveCostNode curr = frontier[0];
            frontier.RemoveAt (0);
            if (curr.node == to)
            {
                Debug.Log ($"Found: {curr.node.name}");

                TerrainNode currNode = curr.node;
                finalPath.Add (currNode);
                while (currNode != from)
                {
                    currNode = currPath[currNode.GetInstanceID ()].node;
                    finalPath.Add (currNode);
                }
                finalPath.Reverse ();

                complete = true;
                return;
            }
            
            for (int i = 0; i < curr.node.paths.Count; i++)
            {
                TerrainNode path = curr.node.paths[i];
                if (path == null || path.type == TerrainType.Blocked)
                    continue;

                float newCost = curr.totalMoveCost + path.moveCost;
                
                // Current paths either don't have the path node, or they do but the new move cost is better than existing move cost
                if (visited.Contains (path))
                    continue;
                if (currPath.ContainsKey (path.GetInstanceID ()) && newCost >= currPath[path.GetInstanceID ()].totalMoveCost)
                    continue;

                currPath.Remove (path.GetInstanceID ());

                currPath.Add (path.GetInstanceID (), new MoveCostNode (curr.node, newCost));
                visited.Add (path);

                frontier.Add (new MoveCostNode (path, newCost));
                frontier.Sort ((a,b) => a.totalMoveCost.CompareTo (b.totalMoveCost));
            }
        }
		
	}
}