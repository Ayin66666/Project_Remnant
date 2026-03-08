using UnityEditor;
using UnityEngine;
using Game.Character;


[CustomPropertyDrawer(typeof(PassiveUIData))]
public class PassiveUIDataDrawer : PropertyDrawer
{
    private const float LineHeight = 18f;
    private const float Padding = 8f;
    private const int DescriptionLines = 7;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lines = 1 + DescriptionLines;
        return (LineHeight * lines) + (Padding * (lines + 1));
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // 전체 폭 사용
        position = EditorGUI.IndentedRect(position);

        var nameProp = property.FindPropertyRelative("passiveName");
        var descProp = property.FindPropertyRelative("passiveDescription");

        Rect rect = position;
        rect.height = LineHeight;

        // 패시브 이름 (중앙 정렬 느낌)
        EditorGUI.LabelField(rect, "Passive Name");
        rect.y += LineHeight + Padding;
        nameProp.stringValue = EditorGUI.TextField(rect, nameProp.stringValue);

        // 설명
        rect.y += LineHeight + Padding;
        rect.height = LineHeight * DescriptionLines;
        descProp.stringValue = EditorGUI.TextArea(rect, descProp.stringValue);

        EditorGUI.EndProperty();
    }
}