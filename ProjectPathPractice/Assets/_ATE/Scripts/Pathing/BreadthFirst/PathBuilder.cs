using ATE.Terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Pathing.BreadthFirst
{
    public class PathBuilder
	{
        public bool complete;

        public TerrainNode from;
        public TerrainNode to;

        public List<TerrainNode> visited;
        public Queue<TerrainNode> frontier;

        public List<TerrainNode> finalPath;


        public void RestartPathing(TerrainNode fromNode, TerrainNode toNode)
        {
            complete = false;

            from = fromNode;
            to = toNode;

            visited = new List<TerrainNode> ();
            frontier = new Queue<TerrainNode> ();
            finalPath = new List<TerrainNode> ();

            visited.Add (from);
            frontier.Enqueue (from);
        }
        
        // Returns true when pathing is complete
        public void IteratePath()
        {
            Debug.Log ("Iterate");

            if (frontier.Count <= 0)
            {
                Debug.Log ("Frontier is empty");
                complete = true;
                return;
            }
            
            TerrainNode currNode = frontier.Dequeue ();
            if (currNode == to)
            {
                Debug.Log ($"Found: {currNode.name}");
                complete = true;
                return;
            }

            for (int i = 0; i < currNode.paths.Count; i++)
            {
                TerrainNode path = currNode.paths[i];
                if (path == null || path.type == TerrainType.Blocked || visited.Contains (path))
                    continue;

                visited.Add (path);
                frontier.Enqueue (path);
            }
        }
		
	}
}