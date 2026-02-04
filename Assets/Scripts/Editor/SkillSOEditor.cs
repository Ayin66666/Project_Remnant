using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillSO))]
public class SkillSOEditor : Editor
{
    SerializedProperty attackType;
    SerializedProperty crimeType;
    SerializedProperty coinPower;
    SerializedProperty coins;
    SerializedProperty ui;
    SerializedProperty skill;

    SerializedProperty icon;
    SerializedProperty skillName;
    SerializedProperty skillDescription;

    void OnEnable()
    {
        attackType = serializedObject.FindProperty("attackType");
        crimeType = serializedObject.FindProperty("crimeType");
        coinPower = serializedObject.FindProperty("coinPower");
        coins = serializedObject.FindProperty("coins");
        ui = serializedObject.FindProperty("ui");
        skill = serializedObject.FindProperty("skill");

        icon = ui.FindPropertyRelative("icon");
        skillName = ui.FindPropertyRelative("skillName");
        skillDescription = ui.FindPropertyRelative("skillDescription");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Skill Data", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(attackType);
        EditorGUILayout.PropertyField(crimeType);
        EditorGUILayout.PropertyField(coinPower);
        EditorGUILayout.PropertyField(coins);

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Skill UI", EditorStyles.boldLabel);

        DrawIconPreview();
        EditorGUILayout.PropertyField(skillName);

        DrawLargeTextArea(skillDescription, 6);

        EditorGUILayout.Space(10);

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

        GUIStyle style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float height = lineHeight * 3 * heightMultiplier;

        property.stringValue = EditorGUILayout.TextArea(
            property.stringValue,
            style,
            GUILayout.Height(height)
        );
    }
}
