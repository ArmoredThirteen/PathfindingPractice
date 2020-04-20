using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RequestPathingExample))]
public class RequestPathingExampleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RequestPathingExample targ = (RequestPathingExample)target;

        EditorStyles.label.wordWrap = true;

        DisplayHelpBox ();
        EditorGUILayout.Space (10);

        if (GUILayout.Button ("Reload CSV into MoveMap"))
            OnMoveMap (targ);

        if (GUILayout.Button ("Request and display PathMap"))
            OnPathMap (targ);

        EditorGUILayout.Space (20);
        base.OnInspectorGUI ();
    }

    private void DisplayHelpBox()
    {
        EditorGUILayout.LabelField(
            "- Use this helper for reloading the MoveMap and requesting/displaying a PathMap.\n" +
            "- All settings found on the components of this object have tooltips.\n" +
            "- To start off, press [Request and display PathMap].\n" +
            "- More maps can be found in [Assets/MapData]. Drag one of them to the MoveMap's MoveCostCSV, then press [Reload CSV into MoveMap] and [Request and display PathMap].");
    }

    private void OnMoveMap(RequestPathingExample targ)
    {
        targ.moveMap.LoadFromCSV ();
        targ.moveMapVisualizer.RebuildVisual ();
        targ.ResetCurrMap ();
        Debug.Log ("Loaded MoveMap, rebuilt visuals, removed old PathMap feedback");
    }

    private void OnPathMap(RequestPathingExample targ)
    {
        targ.RequestPathMap ();
        Debug.Log ("Requested PathMap and drew feedback");
    }

}
