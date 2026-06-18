using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillSO))]
public class SkillSOEditor : Editor
{
    // Skill Data
    SerializedProperty crimeType;
    SerializedProperty skillType;
    SerializedProperty attackType;
    SerializedProperty skillVariantType;

    SerializedProperty originalPower;
    SerializedProperty coinPower;
    SerializedProperty targetCount;

    SerializedProperty skillEffects;
    SerializedProperty coins;

    // UI
    SerializedProperty skillName;
    SerializedProperty icon;
    SerializedProperty uiDatas;

    bool skillFoldout = true;
    bool coinFoldout = true;
    bool uiFoldout = true;

    void OnEnable()
    {
        // Skill Data
        crimeType = serializedObject.FindProperty("crimeType");
        skillType = serializedObject.FindProperty("skillType");
        attackType = serializedObject.FindProperty("attackType");
        skillVariantType = serializedObject.FindProperty("skillVariantType");

        originalPower = serializedObject.FindProperty("originalPower");
        coinPower = serializedObject.FindProperty("coinPower");
        targetCount = serializedObject.FindProperty("targetCount");

        skillEffects = serializedObject.FindProperty("skillEffects");
        coins = serializedObject.FindProperty("coins");

        // UI
        skillName = serializedObject.FindProperty("skillName");
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

        EditorGUILayout.PropertyField(crimeType);
        EditorGUILayout.PropertyField(skillType);
        EditorGUILayout.PropertyField(attackType);
        EditorGUILayout.PropertyField(skillVariantType);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(originalPower);
        EditorGUILayout.PropertyField(coinPower);
        EditorGUILayout.PropertyField(targetCount);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Skill Effects", EditorStyles.boldLabel);

        DrawEffectNodes(skillEffects);

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
            DrawCoin(coins.GetArrayElementAtIndex(i), i);

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Coin"))
        {
            coins.arraySize++;
        }
    }

    void DrawCoin(SerializedProperty coin, int index)
    {
        SerializedProperty motionValue =
            coin.FindPropertyRelative("motionValue");

        SerializedProperty hitDatas =
            coin.FindPropertyRelative("hitDatas");

        SerializedProperty effectNodes =
            coin.FindPropertyRelative("effectNodes");

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField($"Coin {index + 1}",
            EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(
            motionValue,
            new GUIContent("Front / Back Value"));

        EditorGUILayout.Space();

        DrawHitDatas(hitDatas);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Coin Effects",
            EditorStyles.boldLabel);

        DrawEffectNodes(effectNodes);

        EditorGUILayout.Space();

        if (GUILayout.Button("Remove Coin"))
        {
            coins.DeleteArrayElementAtIndex(index);
        }

        EditorGUILayout.EndVertical();
    }

    void DrawHitDatas(SerializedProperty hitDatas)
    {
        EditorGUILayout.LabelField("Hit Data",
            EditorStyles.boldLabel);

        for (int i = 0; i < hitDatas.arraySize; i++)
        {
            SerializedProperty hit =
                hitDatas.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("helpBox");

            EditorGUILayout.LabelField($"Hit {i + 1}");

            EditorGUILayout.PropertyField(
                hit.FindPropertyRelative("hitCount"));

            EditorGUILayout.PropertyField(
                hit.FindPropertyRelative("damagePercent"));

            if (GUILayout.Button("Remove Hit"))
            {
                hitDatas.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Hit"))
        {
            hitDatas.arraySize++;
        }
    }

    #endregion

    #region Effect

    void DrawEffectNodes(SerializedProperty effectNodes)
    {
        for (int i = 0; i < effectNodes.arraySize; i++)
        {
            if (DrawEffectNode(
                    effectNodes.GetArrayElementAtIndex(i),
                    i))
            {
                effectNodes.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Effect"))
        {
            effectNodes.arraySize++;
        }
    }

    bool DrawEffectNode(SerializedProperty node, int index)
    {
        EditorGUILayout.BeginVertical("helpBox");

        EditorGUILayout.LabelField(
            $"Effect {index + 1}",
            EditorStyles.boldLabel);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(
            "Trigger",
            EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(
            node.FindPropertyRelative("triggerType"));

        EditorGUILayout.PropertyField(
            node.FindPropertyRelative("targetType"));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(
            "Condition",
            EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(
            node.FindPropertyRelative("compareType"));

        EditorGUILayout.PropertyField(
            node.FindPropertyRelative("conditionValue"));

        EditorGUILayout.Space();

        DrawValueNodes(
            node.FindPropertyRelative("values"));

        EditorGUILayout.Space();

        DrawActionNode(
            node.FindPropertyRelative("action"));

        EditorGUILayout.Space();

        bool remove = GUILayout.Button("Remove Effect");

        EditorGUILayout.EndVertical();

        return remove;
    }

    void DrawValueNodes(SerializedProperty values)
    {
        EditorGUILayout.LabelField(
            "Values",
            EditorStyles.boldLabel);

        for (int i = 0; i < values.arraySize; i++)
        {
            SerializedProperty value =
                values.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField($"Value {i + 1}");

            EditorGUILayout.PropertyField(
                value.FindPropertyRelative("effect"));

            EditorGUILayout.PropertyField(
                value.FindPropertyRelative("valueType"));

            if (GUILayout.Button("Remove Value"))
            {
                values.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Add Value"))
        {
            values.arraySize++;
        }
    }

    void DrawActionNode(SerializedProperty action)
    {
        EditorGUILayout.LabelField(
            "Action",
            EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.PropertyField(
            action.FindPropertyRelative("actionType"));

        EditorGUILayout.PropertyField(
            action.FindPropertyRelative("valueNode"));

        EditorGUILayout.PropertyField(
            action.FindPropertyRelative("sinType"));

        EditorGUILayout.PropertyField(
            action.FindPropertyRelative("actionValue"));

        EditorGUILayout.Space();

        EditorGUILayout.LabelField(
            "Original Action",
            EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(
            action.FindPropertyRelative("actionIndex"));

        EditorGUILayout.PropertyField(
            action.FindPropertyRelative("actionDescription"));

        EditorGUILayout.EndVertical();
    }

    #endregion

    #region UI

    void DrawUI()
    {
        uiFoldout = EditorGUILayout.Foldout(uiFoldout, "UI", true);

        if (!uiFoldout)
            return;

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.PropertyField(skillName);

        DrawIconPreview();

        EditorGUILayout.Space();

        for (int i = 0; i < uiDatas.arraySize; i++)
        {
            SerializedProperty ui =
                uiDatas.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("helpBox");

            EditorGUILayout.LabelField(
                $"UI Data {i + 1}",
                EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(
                ui.FindPropertyRelative("sync"));

            DrawLargeTextArea(
                ui.FindPropertyRelative("skillDescription"),
                10);

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
            uiDatas.arraySize++;
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
            GUILayout.Label(
                tex,
                GUILayout.Width(96),
                GUILayout.Height(96));
        }
    }

    void DrawLargeTextArea(
        SerializedProperty property,
        int lines)
    {
        GUIStyle style = new GUIStyle(EditorStyles.textArea)
        {
            wordWrap = true
        };

        float height =
            EditorGUIUtility.singleLineHeight *
            lines * 1.5f;

        property.stringValue =
            EditorGUILayout.TextArea(
                property.stringValue,
                style,
                GUILayout.Height(height));
    }

    #endregion
}