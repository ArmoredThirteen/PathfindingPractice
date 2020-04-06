using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ATE.Terrain
{
    public class TerrainMap_Grid : TerrainMap
    {
        public int sizeX = 15;
        public int sizeY = 10;
        
        public float nodeSpacingX = 1;
        public float nodeSpacingY = 1;

        public TerrainNode[] flatMap;


        [ContextMenu ("BuildDefaultMap")]
        public override void BuildDefaultMap()
        {
            // Clear existing map
            if (flatMap != null)
                for (int i = 0; i < flatMap.Length; i++)
                    if (flatMap[i] != null)
                        GameObject.DestroyImmediate (flatMap[i].gameObject);

            // Generate new default map
            flatMap = new TerrainNode[sizeX * sizeY];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                {
                    // Instantiate and place default prefab
                    TerrainNode newNode = (TerrainNode)PrefabUtility.InstantiatePrefab (defaultNodePrefab, transform);
                    newNode.transform.localPosition = new Vector3 (x * nodeSpacingX, y * nodeSpacingY, 0);
                    newNode.transform.localRotation = defaultNodePrefab.transform.rotation;

                    // Move along x axis first?
                    flatMap[(y * sizeX) + x] = newNode;
                }

            // Connect paths together
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                {
                    TerrainNode theNode = GetNode (x, y);

                    // Connect paths right, left, up, down
                    theNode.paths.Add (GetNode (x + 1, y));
                    theNode.paths.Add (GetNode (x - 1, y));
                    theNode.paths.Add (GetNode (x, y + 1));
                    theNode.paths.Add (GetNode (x, y - 1));
                }
        }


        public TerrainNode GetNode(int x, int y)
        {
            if (x < 0 || x >= sizeX)
                return null;
            if (y < 0 || y >= sizeY)
                return null;

            return flatMap[(y * sizeX) + x];
        }

    }
}