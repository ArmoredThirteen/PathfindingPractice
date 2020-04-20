using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * Stores a 2d array of move costs for use with pathfinding.
 * Move cost information is stored in a .csv which is read in on Awake().
 * Flips y axis compared to how .csv is shown so translating to world coordinates is easier.
 */
public class MoveMap : MonoBehaviour
{
    [Tooltip("Basic .csv file to load move costs from. Must only have newlines, commas, and decimal numbers")]
    public TextAsset moveCostCSV;
    
    [Tooltip("Any move cost lower than this is considered unpathable, and internally is turned to PositiveInfinity")]
    public float minPathableCost = 1;
    
    // moveCosts are stored as a flattened array so Unity can serialize it
    [HideInInspector]
    public int xLen;
    [HideInInspector]
    public int yLen;
    [HideInInspector]
    public float[] moveCosts;


    /*
     * Loads moveCostCSV into internal moveCosts array.
     * Splits based on newlines and commas.
     * Move cost < minPathableCost is turned into PositiveInfinity.
     */
    [ContextMenu("Load from CSV")]
    public void LoadFromCSV()
    {
        string[] rows = (moveCostCSV.text.Split (new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));

        //TODO: Not a fan of this extra split but it doesn't happen very often
        xLen = rows[0].Split (',').Length;
        yLen = rows.Length;
        moveCosts = new float[xLen * yLen];

        for (int y = 0; y < yLen; y++)
        {
            // foreach column within the row
            float[] splitRow = rows[rows.Length - (y + 1)].Split (',').Select (str => float.Parse(str)).ToArray ();
            for (int x = 0; x < xLen; x++)
            {
                float cost = splitRow[x] >= minPathableCost ? splitRow[x] : float.PositiveInfinity;
                SetMoveCost (x, y, cost);
            }
        }
    }

    public void ResetMoveCosts()
    {
        xLen = 0;
        yLen = 0;
        moveCosts = new float[] { };
    }


    /*
     * Returns true if given x,y coordinates are within bounds of moveCosts.
     */
    public bool NodeExists(int x, int y)
    {
        if (x < 0 || y < 0)
            return false;
        if (x >= xLen || y >= yLen)
            return false;

        return true;
    }

    /*
     * Returns value found at moveCosts x/y.
     * If a node does not exist, PositiveInfinity is returned.
     */
    public float GetMoveCost(int x, int y)
    {
        if (!NodeExists (x, y))
            return float.PositiveInfinity;
        return moveCosts[(xLen * y) + x];
    }

    /*
     * Sets the value found at moveCosts x/y.
     * If a node does not exist, does nothing.
     */
    public void SetMoveCost(int x, int y, float moveCost)
    {
        if (!NodeExists (x, y))
            return;
        moveCosts[(xLen * y) + x] = moveCost;
    }


    public override string ToString()
    {
        StringBuilder builder = new StringBuilder ();

        for (int y = yLen - 1; y >= 0; y--)
        {
            for (int x = 0; x < xLen; x++)
                builder.Append (GetMoveCost (x, y));
            builder.Append ("\n");
        }

        return builder.ToString ();
    }

}
