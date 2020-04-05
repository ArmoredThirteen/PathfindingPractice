using ATE.Nav;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Terrain
{
	public abstract class TerrainMap : MonoBehaviour
	{
        public abstract void BuildTestTerrain();
        public abstract void RebuildNavMap();
		
	}
}