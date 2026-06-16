using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectNodeSO))]
public class EffectNodeSOEditor : Editor
{
    SerializedProperty triggerType;
    SerializedProperty targetType;
    SerializedProperty conditions;

    bool conditionFoldout = true;

    const int SMALL_SPACE = 3;
    const int NORMAL_SPACE = 8;
    const int LARGE_SPACE = 15;

    void OnEnable()
    {
        triggerType = serializedObject.FindProperty("triggerType");
        targetType = serializedObject.FindProperty("targetType");
        conditions = serializedObject.FindProperty("conditions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawNodeData();

        EditorGUILayout.Space(LARGE_SPACE);

        DrawConditions();

        serializedObject.ApplyModifiedProperties();
    }

    #region Node Data

    void DrawNodeData()
    {
        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField(
            "Effect Node",
            EditorStyles.boldLabel);

        EditorGUILayout.Space(NORMAL_SPACE);

        EditorGUILayout.PropertyField(triggerType);

        EditorGUILayout.Space(SMALL_SPACE);

        EditorGUILayout.PropertyField(targetType);

        EditorGUILayout.EndVertical();
    }

    #endregion

    #region Condition

    void DrawConditions()
    {
        conditionFoldout =
            EditorGUILayout.Foldout(conditionFoldout, "Conditions", true);

        if (!conditionFoldout)
            return;

        EditorGUILayout.Space(NORMAL_SPACE);

        for (int i = 0; i < conditions.arraySize; i++)
        {
            DrawCondition(
                conditions.GetArrayElementAtIndex(i),
                i);

            EditorGUILayout.Space(LARGE_SPACE);
        }

        if (GUILayout.Button("Add Condition"))
        {
            conditions.arraySize++;
        }
    }

    void DrawCondition(SerializedProperty condition, int index)
    {
        SerializedProperty values =
            condition.FindPropertyRelative("values");

        SerializedProperty compareType =
            condition.FindPropertyRelative("compareType");

        SerializedProperty value =
            condition.FindPropertyRelative("value");

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField(
            $"Condition {index + 1}",
            EditorStyles.boldLabel);

        EditorGUILayout.Space(NORMAL_SPACE);

        // Values
        EditorGUILayout.LabelField(
            "Values",
            EditorStyles.boldLabel);

        EditorGUILayout.Space(SMALL_SPACE);

        DrawValues(values);

        EditorGUILayout.Space(LARGE_SPACE);

        // Comparison
        EditorGUILayout.LabelField(
            "Comparison",
            EditorStyles.boldLabel);

        EditorGUILayout.Space(SMALL_SPACE);

        EditorGUILayout.PropertyField(compareType);

        EditorGUILayout.Space(SMALL_SPACE);

        EditorGUILayout.PropertyField(value);

        EditorGUILayout.Space(LARGE_SPACE);

        if (GUILayout.Button("Remove Condition"))
        {
            conditions.DeleteArrayElementAtIndex(index);
        }

        EditorGUILayout.EndVertical();
    }

    #endregion

    #region Value

    void DrawValues(SerializedProperty values)
    {
        for (int i = 0; i < values.arraySize; i++)
        {
            SerializedProperty valueNode =
                values.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("helpBox");

            DrawValue(valueNode, i);

            EditorGUILayout.Space(NORMAL_SPACE);

            if (GUILayout.Button("Remove Value"))
            {
                values.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(NORMAL_SPACE);
        }

        if (GUILayout.Button("Add Value"))
        {
            values.arraySize++;
        }
    }

    void DrawValue(SerializedProperty valueNode, int index)
    {
        SerializedProperty effect =
            valueNode.FindPropertyRelative("effet");

        SerializedProperty valueType =
            valueNode.FindPropertyRelative("valueType");

        EditorGUILayout.LabelField(
            $"Value {index + 1}",
            EditorStyles.boldLabel);

        EditorGUILayout.Space(SMALL_SPACE);

        EditorGUILayout.PropertyField(effect);

        EditorGUILayout.Space(SMALL_SPACE);

        EditorGUILayout.PropertyField(valueType);
    }

    #endregion
}