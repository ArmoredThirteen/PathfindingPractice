using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Terrain
{
    [Serializable]
	public class TerrainNode : MonoBehaviour
	{
        public List<TerrainNode> paths = new List<TerrainNode> ();

        public TerrainType type = TerrainType.Ground;
        public int height = 0;

	}
}