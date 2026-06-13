using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillSO))]
public class SkillSOEditor : Editor
{
    // 모든 커스텀 에디터 스크립트는 AI의 도움을 받아 작성하였습니다.

    // Skill Data
    SerializedProperty skillName;
    SerializedProperty crimeType;
    SerializedProperty skillType;
    SerializedProperty attackType;
    SerializedProperty skillVariantType;
    SerializedProperty originalPower;
    SerializedProperty coinPower;
    SerializedProperty targetCount;
    SerializedProperty coins;

    // UI
    SerializedProperty icon;
    SerializedProperty uiDatas;

    bool skillFoldout = true;
    bool coinFoldout = true;
    bool uiFoldout = true;

    void OnEnable()
    {
        skillName = serializedObject.FindProperty("skillName");
        crimeType = serializedObject.FindProperty("crimeType");
        skillType = serializedObject.FindProperty("skillType");
        attackType = serializedObject.FindProperty("attackType");
        skillVariantType = serializedObject.FindProperty("skillVariantType");

        originalPower = serializedObject.FindProperty("originalPower");
        coinPower = serializedObject.FindProperty("coinPower");
        targetCount = serializedObject.FindProperty("targetCount");

        coins = serializedObject.FindProperty("coins");

        icon = serializedObject.FindProperty("icon");
        uiDatas = serializedObject.FindProperty("uiDatas");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawSkillData();

        EditorGUILayout.Space(10);

        DrawCoins();

        EditorGUILayout.Space(10);

        DrawUI();

        serializedObject.ApplyModifiedProperties();
    }

    #region Skill Data

    void DrawSkillData()
    {
        skillFoldout = EditorGUILayout.Foldout(skillFoldout, "Skill Data", true);

        if (!skillFoldout)
            return;

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.PropertyField(skillName);
        EditorGUILayout.PropertyField(crimeType);
        EditorGUILayout.PropertyField(skillType);
        EditorGUILayout.PropertyField(attackType);
        EditorGUILayout.PropertyField(skillVariantType);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(originalPower);
        EditorGUILayout.PropertyField(coinPower);
        EditorGUILayout.PropertyField(targetCount);

        EditorGUILayout.EndVertical();
    }

    #endregion

    #region Coin

    void DrawCoins()
    {
        coinFoldout = EditorGUILayout.Foldout(coinFoldout, "Coins", true);

        if (!coinFoldout)
            return;

        for (int i = 0; i < coins.arraySize; i++)
        {
            SerializedProperty coin = coins.GetArrayElementAtIndex(i);

            SerializedProperty value =
                coin.FindPropertyRelative("value");

            SerializedProperty hitDatas =
                coin.FindPropertyRelative("hitDatas");

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"Coin {i + 1}", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(value, new GUIContent("Front / Back Value"));

            EditorGUILayout.Space();

            DrawHitDatas(hitDatas);

            EditorGUILayout.Space();

            if (GUILayout.Button("Remove Coin"))
            {
                coins.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Coin"))
        {
            coins.InsertArrayElementAtIndex(coins.arraySize);
        }
    }

    void DrawHitDatas(SerializedProperty hitDatas)
    {
        EditorGUILayout.LabelField("Hit Data", EditorStyles.boldLabel);

        for (int i = 0; i < hitDatas.arraySize; i++)
        {
            SerializedProperty hit =
                hitDatas.GetArrayElementAtIndex(i);

            SerializedProperty hitCount =
                hit.FindPropertyRelative("hitCount");

            SerializedProperty damagePercent =
                hit.FindPropertyRelative("damagePercent");

            EditorGUILayout.BeginVertical("helpBox");

            EditorGUILayout.LabelField($"Hit {i + 1}");

            EditorGUILayout.PropertyField(hitCount);
            EditorGUILayout.PropertyField(damagePercent);

            if (GUILayout.Button("Remove Hit"))
            {
                hitDatas.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Hit"))
        {
            hitDatas.InsertArrayElementAtIndex(hitDatas.arraySize);
        }
    }

    #endregion

    #region UI

    void DrawUI()
    {
        uiFoldout = EditorGUILayout.Foldout(uiFoldout, "UI", true);

        if (!uiFoldout)
            return;

        EditorGUILayout.BeginVertical("box");

        DrawIconPreview();

        for (int i = 0; i < uiDatas.arraySize; i++)
        {
            SerializedProperty ui =
                uiDatas.GetArrayElementAtIndex(i);

            SerializedProperty sync =
                ui.FindPropertyRelative("sync");

            SerializedProperty skillDescription =
                ui.FindPropertyRelative("skillDescription");

            EditorGUILayout.BeginVertical("helpBox");

            EditorGUILayout.LabelField($"UI Data {i + 1}", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(sync);

            EditorGUILayout.LabelField("Description");

            DrawLargeTextArea(skillDescription, 10);

            if (GUILayout.Button("Remove UI Data"))
            {
                uiDatas.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add UI Data"))
        {
            uiDatas.InsertArrayElementAtIndex(uiDatas.arraySize);
        }

        EditorGUILayout.EndVertical();
    }

    #endregion

    #region Utility

    void DrawIconPreview()
    {
        EditorGUILayout.PropertyField(icon);

        if (icon.objectReferenceValue == null)
            return;

        Texture2D tex =
            AssetPreview.GetMiniThumbnail(icon.objectReferenceValue);

        if (tex != null)
        {
            GUILayout.Label(tex,
                GUILayout.Width(96),
                GUILayout.Height(96));
        }
    }

    void DrawLargeTextArea(SerializedProperty property, int lines)
    {
        GUIStyle style = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true
        };

        float height = EditorGUIUtility.singleLineHeight * lines * 1.5f;

        property.stringValue = EditorGUILayout.TextArea(
            property.stringValue,
            style,
            GUILayout.Height(height));
    }

    #endregion
}