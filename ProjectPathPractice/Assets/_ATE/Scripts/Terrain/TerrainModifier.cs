using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Terrain
{
	public class TerrainModifier : MonoBehaviour
	{
        public TerrainType type = TerrainType.Blocked;

        public Transform pointOne;
        public Transform pointTwo;
        public float castRadius = 0.5f;
        public Vector3 castDir = Vector3.down;
        public float castDist = 10;


        public void ModifyTerrain(TerrainTypeSettings typeSettings)
        {
            RaycastHit[] hits = Physics.CapsuleCastAll (pointOne.position, pointTwo.position, castRadius, castDir, castDist);
            for (int i = 0; i < hits.Length; i++)
            {
                TerrainNode node = hits[i].collider.GetComponent<TerrainNode> ();
                if (node == null)
                    continue;

                node.type = type;
                node.ApplyTerrainTypeSettings (typeSettings);
            }
        }
		
	}
}