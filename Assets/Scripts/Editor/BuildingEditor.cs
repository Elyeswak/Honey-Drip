using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{
    private SerializedProperty _localEventOnIdle;
    private SerializedProperty _localEventOnProgress;
    private SerializedProperty _localEventOnComplete;

    private bool _showLocalEvents = false; // Whether to show the local events section

    private void OnEnable()
    {
        _localEventOnIdle = serializedObject.FindProperty("_localEventOnIdle");
        _localEventOnProgress = serializedObject.FindProperty("_localEventOnProgress");
        _localEventOnComplete = serializedObject.FindProperty("_localEventOnComplete");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Draw the default inspector without the local events fields
        DrawDefaultInspectorExceptEvents();

        // Draw collapsible section for local events
        _showLocalEvents = EditorGUILayout.BeginFoldoutHeaderGroup(_showLocalEvents, "Local Events");
        if (_showLocalEvents)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_localEventOnIdle);
            EditorGUILayout.PropertyField(_localEventOnProgress);
            EditorGUILayout.PropertyField(_localEventOnComplete);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }

    // Helper method to draw the default inspector excluding local events
    private void DrawDefaultInspectorExceptEvents()
    {
        // Draw all properties except the local events fields
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;
            if (IsLocalEventProperty(iterator))
                continue; // Skip drawing local events
            EditorGUILayout.PropertyField(iterator, true);
        }
    }

    // Helper method to check if a property represents a local event
    private bool IsLocalEventProperty(SerializedProperty property)
    {
        return property.name == "_localEventOnIdle"
            || property.name == "_localEventOnProgress"
            || property.name == "_localEventOnComplete";
    }
}
