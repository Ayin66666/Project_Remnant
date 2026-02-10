using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillSO))]
public class SkillSOEditor : Editor
{
    // --- Type ---
    SerializedProperty skillType;
    SerializedProperty skillVariantType;

    // --- Skill Data ---
    SerializedProperty attackType;
    SerializedProperty crimeType;
    SerializedProperty coinPower;
    SerializedProperty targetCount;
    SerializedProperty coins;

    // --- UI ---
    SerializedProperty ui;
    SerializedProperty icon;
    SerializedProperty skillName;
    SerializedProperty skillDescription;

    // --- Action ---
    SerializedProperty skill;

    void OnEnable()
    {
        // Type
        skillType = serializedObject.FindProperty("skillType");
        skillVariantType = serializedObject.FindProperty("skillVariantType");

        // Skill Data
        attackType = serializedObject.FindProperty("attackType");
        crimeType = serializedObject.FindProperty("crimeType");
        coinPower = serializedObject.FindProperty("coinPower");
        targetCount = serializedObject.FindProperty("targetCount");
        coins = serializedObject.FindProperty("coins");

        // UI
        ui = serializedObject.FindProperty("ui");
        icon = ui.FindPropertyRelative("icon");
        skillName = ui.FindPropertyRelative("skillName");
        skillDescription = ui.FindPropertyRelative("skillDescription");

        // Action
        skill = serializedObject.FindProperty("skill");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // ===== Type =====
        EditorGUILayout.LabelField("Type", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(skillType);
        EditorGUILayout.PropertyField(skillVariantType);

        EditorGUILayout.Space(10);

        // ===== Skill Data =====
        EditorGUILayout.LabelField("Skill Data", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(attackType);
        EditorGUILayout.PropertyField(crimeType);
        EditorGUILayout.PropertyField(coinPower);
        EditorGUILayout.PropertyField(targetCount);
        EditorGUILayout.PropertyField(coins);

        EditorGUILayout.Space(10);

        // ===== Skill UI =====
        EditorGUILayout.LabelField("Skill UI", EditorStyles.boldLabel);
        DrawIconPreview();
        EditorGUILayout.PropertyField(skillName);
        DrawLargeTextArea(skillDescription, 6);

        EditorGUILayout.Space(10);

        // ===== Action =====
        EditorGUILayout.LabelField("Action", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(skill);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawIconPreview()
    {
        EditorGUILayout.PropertyField(icon);

        if (icon.objectReferenceValue != null)
        {
            Texture2D tex = AssetPreview.GetAssetPreview(icon.objectReferenceValue);
            if (tex != null)
            {
                GUILayout.Label(tex, GUILayout.Width(96), GUILayout.Height(96));
            }
        }
    }

    void DrawLargeTextArea(SerializedProperty property, int heightMultiplier)
    {
        EditorGUILayout.LabelField(property.displayName);

        GUIStyle style = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true
        };

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float height = lineHeight * heightMultiplier * 3;

        property.stringValue = EditorGUILayout.TextArea(
            property.stringValue,
            style,
            GUILayout.Height(height)
        );
    }
}