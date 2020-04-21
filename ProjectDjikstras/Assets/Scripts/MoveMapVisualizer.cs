using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/*
 * Creates a visual representation of the given MoveMap.
 * Instantiates children where each prefab is chosen based on an associated move cost.
 */
public class MoveMapVisualizer : MonoBehaviour
{
    // Serializable place to tie cell prefabs to move costs
    [System.Serializable]
    public class MoveCostObject
    {
        public float moveCost;
        public GameObject prefab;
    }


    [Tooltip("MoveMap that will be visualized")]
    public MoveMap moveMap;

    [Tooltip ("Prefab used when no other prefab is found for a node's move cost")]
    public GameObject defaultCostObj;
    [Tooltip("For nodes with infinite move cost or other conditions making it unpathable")]
    public GameObject unpathableCostObj;
    [Tooltip("All specifically defined move costs and their associated prefabs")]
    public MoveCostObject[] costObjects;


    /*
     * Destroys children to make way for new visualizer objects.
     */
    [ContextMenu ("Destroy All Children")]
    public void DestroyAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate (transform.GetChild (0).gameObject);
    }

    /*
     * Destroys children then creates and places new ones based on moveCosts array.
     * Created objects are defined by taking the cell's move cost and finding the correct MoveCostObject.
     */
    [ContextMenu ("Rebuild Visual")]
    public void RebuildVisual()
    {
        DestroyAllChildren ();

        // Create and place object from appropriate MoveCostObject
        for (int y = 0; y < moveMap.yLen; y++)
            for (int x = 0; x < moveMap.xLen; x++)
            {
                GameObject thePrefab = defaultCostObj;

                // Choose correct prefab, if one exists
                float moveCost = moveMap.GetMoveCost (x, y);
                if (moveCost == float.PositiveInfinity)
                    thePrefab = unpathableCostObj;
                else
                    for (int i = 0; i < costObjects.Length; i++)
                        if (moveCost == costObjects[i].moveCost)
                        {
                            thePrefab = costObjects[i].prefab;
                            break;
                        }

                // Instantiate and place the prefab
                GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(thePrefab);
                newObj.transform.parent = transform;
                newObj.transform.localPosition = new Vector3 (x, y, 0);

                if (!Application.isPlaying)
                    EditorUtility.SetDirty (newObj.gameObject);
            }

        // Tell Unity the object/scene has changed
        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty (gameObject);
            EditorUtility.SetDirty (moveMap.gameObject);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

}
