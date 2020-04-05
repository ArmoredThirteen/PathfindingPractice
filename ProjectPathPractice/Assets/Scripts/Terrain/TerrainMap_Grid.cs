using System.Collections;
using System.Collections.Generic;
using ATE.Nav;
using UnityEngine;

namespace ATE.Terrain
{
    public class TerrainMap_Grid : TerrainMap
    {
        public int testTerrainWidth = 15;
        public int testTerrainHeight = 10;

        public TerrainNode[,] map;


        public int LenY
        {
            get { return map.GetLength (0); }
        }

        public int LenX
        {
            get { return map.GetLength (1); }
        }


        [ContextMenu ("Build Test Terrain")]
        public override void BuildTestTerrain()
        {
            map = new TerrainNode[testTerrainHeight, testTerrainWidth];

            for (int y = 0; y < testTerrainHeight; y++)
                for (int x = 0; x < testTerrainWidth; x++)
                    map[y,x] = new TerrainNode ();

            Debug.Log (GetMapString ());
        }


        [ContextMenu ("Rebuild Nav Map")]
        public override void RebuildNavMap()
        {
            for (int y = 0; y < LenY; y++)
                for (int x = 0; x < LenX; x++)
                    BuildNavNode (x, y);
        }

        public void BuildNavNode(int x, int y)
        {
            NavNode newNav = new NavNode ();
            map[y,x].nav = newNav;

            // Up, right, down, left
            newNav.paths.Add (y + 1 >= LenY ? null : map[y + 1, x].nav);
            newNav.paths.Add (x + 1 >= LenX ? null : map[y, x + 1].nav);
            newNav.paths.Add (y - 1 < 0 ? null : map[y - 1, x].nav);
            newNav.paths.Add (x - 1 < 0 ? null : map[y, x - 1].nav);
        }


        public string GetMapString()
        {
            string returnStr = "";

            for (int y = 0; y < LenY; y++)
            {
                for (int x = 0; x < LenX; x++)
                {
                    returnStr += map[y,x].type + ", ";
                }
                returnStr += "\r\n";
            }

            return returnStr;
        }

    }
}