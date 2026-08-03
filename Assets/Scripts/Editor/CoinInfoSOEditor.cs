using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CoinInfoSO))]
public class CoinInfoSOEditor : Editor
{
    SerializedProperty coinType;
    SerializedProperty damagePercent;
    SerializedProperty effectNodes;

    private void OnEnable()
    {
        coinType = serializedObject.FindProperty("coinType");
        damagePercent = serializedObject.FindProperty("damagePercent");
        effectNodes = serializedObject.FindProperty("effectNodes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawCoinSetting();
        EditorGUILayout.Space(15);

        DrawEffectNodes();

        serializedObject.ApplyModifiedProperties();
    }

    void DrawCoinSetting()
    {
        EditorGUILayout.LabelField("Coin Setting", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(coinType, new GUIContent("Coin Type"));
        EditorGUILayout.PropertyField(damagePercent, new GUIContent("Damage (%)"));
    }

    void DrawEffectNodes()
    {
        EditorGUILayout.LabelField("Effect Nodes", EditorStyles.boldLabel);

        for (int i = 0; i < effectNodes.arraySize; i++)
        {
            SerializedProperty node = effectNodes.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"Effect {i + 1}", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(node, true);

            EditorGUILayout.Space();

            if (GUILayout.Button("Remove Effect"))
            {
                effectNodes.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(15);
        }

        if (GUILayout.Button("Add Effect"))
        {
            effectNodes.InsertArrayElementAtIndex(effectNodes.arraySize);
        }
    }
}