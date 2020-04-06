using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE.Terrain
{
    public class TerrainTypeSettings : MonoBehaviour
	{
        [System.Serializable]
        public class TypeSettings
        {
            public TerrainType type = TerrainType.Ground;
            public Material material;
        }


        public TypeSettings[] settings;


        public TypeSettings GetSettings(TerrainType type)
        {
            for (int i = 0; i < settings.Length; i++)
                if (settings[i].type == type)
                    return settings[i];

            return null;
        }

	}
}