using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ATE.Terrain
{
	public class TerrainModifyMap : MonoBehaviour
	{
        public TerrainMap map;

        public TerrainTypeSettings typeSettings;
        public TerrainType defaultTerrainType = TerrainType.Ground;

        [ContextMenu ("Process Terrain Objects")]
        public void Process()
        {
            // Reset all nodes to use default type
            TerrainNode[] terrainNodes = map.transform.GetComponentsInChildren<TerrainNode> ();
            for (int i = 0; i < terrainNodes.Length; i++)
            {
                if (terrainNodes[i].type == defaultTerrainType)
                    continue;

                terrainNodes[i].type = defaultTerrainType;
                terrainNodes[i].ApplyTerrainTypeSettings (typeSettings);
            }

            // Make all TerrainObjects cast onto the nodes and modify them appropriately
            TerrainModifier[] terrainObjs = transform.GetComponentsInChildren<TerrainModifier> ();
            for (int i = 0; i < terrainObjs.Length; i++)
                terrainObjs[i].ModifyTerrain (typeSettings);

            // Tell Unity the object/scene has changed
            EditorUtility.SetDirty (this.gameObject);
            EditorUtility.SetDirty (map.gameObject);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

	}
}