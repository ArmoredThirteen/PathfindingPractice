using ATE.Nav;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Terrain
{
    [Serializable]
	public class TerrainNode// : MonoBehaviour
	{
        public NavNode nav;

        public TerrainType type = TerrainType.Ground;
        public int height = 0;

	}
}