using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillSO))]
public class SkillSOEditor : Editor
{
    // --- Type ---
    SerializedProperty skillType;
    SerializedProperty skillVariantType;

    // --- Root UI ---
    SerializedProperty icon;
    SerializedProperty skillName;

    // --- Skill Info List ---
    SerializedProperty skillInfos;

    // --- Action ---
    SerializedProperty skill;

    void OnEnable()
    {
        skillType = serializedObject.FindProperty("skillType");
        skillVariantType = serializedObject.FindProperty("skillVariantType");

        icon = serializedObject.FindProperty("icon");
        skillName = serializedObject.FindProperty("skillName");

        skillInfos = serializedObject.FindProperty("skillinfo");

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

        // ===== Skill UI =====
        EditorGUILayout.LabelField("Skill UI", EditorStyles.boldLabel);

        DrawIconPreview();
        EditorGUILayout.PropertyField(skillName);

        EditorGUILayout.Space(10);

        // ===== Skill Data =====
        EditorGUILayout.LabelField("Skill Data", EditorStyles.boldLabel);

        for (int i = 0; i < skillInfos.arraySize; i++)
        {
            SerializedProperty info = skillInfos.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Skill Info {i}", EditorStyles.boldLabel);

            DrawSkillInfo(info);

            if (GUILayout.Button("Remove Skill Info"))
            {
                skillInfos.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        if (GUILayout.Button("Add Skill Info"))
        {
            skillInfos.InsertArrayElementAtIndex(skillInfos.arraySize);
        }

        EditorGUILayout.Space(10);

        // ===== Action =====
        EditorGUILayout.LabelField("Action", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(skill);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawSkillInfo(SerializedProperty info)
    {
        SerializedProperty attackType = info.FindPropertyRelative("attackType");
        SerializedProperty crimeType = info.FindPropertyRelative("crimeType");
        SerializedProperty coinPower = info.FindPropertyRelative("coinPower");
        SerializedProperty targetCount = info.FindPropertyRelative("targetCount");
        SerializedProperty coins = info.FindPropertyRelative("coins");
        SerializedProperty ui = info.FindPropertyRelative("ui");

        EditorGUILayout.PropertyField(attackType);
        EditorGUILayout.PropertyField(crimeType);
        EditorGUILayout.PropertyField(coinPower);
        EditorGUILayout.PropertyField(targetCount);

        EditorGUILayout.Space(5);

        DrawCoins(coins);

        EditorGUILayout.Space(5);

        DrawSkillUI(ui);
    }

    void DrawCoins(SerializedProperty coins)
    {
        EditorGUILayout.LabelField("Coins", EditorStyles.boldLabel);

        for (int i = 0; i < coins.arraySize; i++)
        {
            SerializedProperty coin = coins.GetArrayElementAtIndex(i);

            SerializedProperty value = coin.FindPropertyRelative("value");
            SerializedProperty hitCount = coin.FindPropertyRelative("hitCount");

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.PropertyField(value, new GUIContent("Value (Front / Back)"));
            EditorGUILayout.PropertyField(hitCount);

            if (GUILayout.Button("Remove Coin"))
            {
                coins.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Coin"))
        {
            coins.InsertArrayElementAtIndex(coins.arraySize);
        }
    }

    void DrawSkillUI(SerializedProperty ui)
    {
        SerializedProperty skillDescription = ui.FindPropertyRelative("skillDescription");

        EditorGUILayout.LabelField("Description", EditorStyles.boldLabel);

        DrawLargeTextArea(skillDescription, 5);
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