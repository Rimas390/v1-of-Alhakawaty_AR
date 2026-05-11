using UnityEngine;
using UnityEditor;

public class SetPivot : EditorWindow
{
    [MenuItem("Tools/Set Pivot")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SetPivot));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Set Pivot to Center"))
        {
            SetPivotToCenter();
        }
    }

    private void SetPivotToCenter()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("Please select a GameObject first.");
            return;
        }

        GameObject selectedGO = Selection.activeGameObject;
        MeshFilter meshFilter = selectedGO.GetComponent<MeshFilter>();

        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogWarning("Selected GameObject must have a MeshFilter with a Mesh.");
            return;
        }

        Mesh mesh = meshFilter.sharedMesh;
        Vector3 center = mesh.bounds.center;

        // Reset mesh center to 0
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= center;
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        // Adjust transform to match new pivot
        selectedGO.transform.position += center;

        Debug.Log("Pivot set to center for " + selectedGO.name);
    }
}