using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ATE.Terrain
{
    [Serializable]
	public class TerrainNode : MonoBehaviour
	{
        public List<TerrainNode> paths = new List<TerrainNode> ();

        public TerrainType type = TerrainType.Ground;
        public float moveCost = 10;


        public void ApplyTerrainTypeSettings(TerrainTypeSettings typeSettings)
        {
            TerrainTypeSettings.TypeSettings theSettings = typeSettings.GetSettings (type);

            Renderer nodeRenderer = GetComponent<Renderer> ();
            nodeRenderer.material = theSettings.material;

            moveCost = theSettings.moveCost;

            EditorUtility.SetDirty (this);
        }

	}
}