using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CoinInfoSO))]
public class CoinInfoSOEditor : Editor
{
    SerializedProperty effectNodes;

    private void OnEnable()
    {
        effectNodes = serializedObject.FindProperty("effectNodes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawMotion();
        EditorGUILayout.Space(15);

        DrawEffectNodes();

        serializedObject.ApplyModifiedProperties();
    }

    void DrawMotion()
    {
        EditorGUILayout.LabelField("Motion Value", EditorStyles.boldLabel);
    }

    void DrawEffectNodes()
    {
        EditorGUILayout.LabelField("Effect Nodes", EditorStyles.boldLabel);

        for (int i = 0; i < effectNodes.arraySize; i++)
        {
            SerializedProperty node =
                effectNodes.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"Effect {i}", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(node, true);

            EditorGUILayout.Space();

            if (GUILayout.Button("Remove Effect"))
            {
                effectNodes.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();

            // ★ Effect끼리 여백 크게
            EditorGUILayout.Space(15);
        }

        if (GUILayout.Button("Add Effect"))
        {
            effectNodes.InsertArrayElementAtIndex(effectNodes.arraySize);
        }
    }
}