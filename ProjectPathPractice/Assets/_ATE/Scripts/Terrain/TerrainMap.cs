﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Terrain
{
	public abstract class TerrainMap : MonoBehaviour
	{
        public TerrainNode defaultNodePrefab;
        public TerrainTypeSettings typeSettings;

        public abstract void BuildDefaultMap();
		
	}
}