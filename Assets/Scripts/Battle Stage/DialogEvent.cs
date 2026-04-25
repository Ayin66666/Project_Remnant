using System.Collections.Generic;
using UnityEngine;
using Game.Character;


[CreateAssetMenu(fileName = "Dialog Event", menuName = "Battle Stage/Dialog Event", order = int.MaxValue)]
public class DialogEvent : BattleStageEventSO
{
    [Header("---Setting---")]
    [SerializeField] private List<DialogData> dialogDataList;

    // 제작 방식 고민 필요함
    // 스프라이트의 이동이나 강조, 투명화 등, 연출이 다양하게 필요한데,
    // 이걸 매 대사마다 체크하기는 너무 무거우니 변화 타이밍을 체크해서 연출하는 방식으로 구현이 필요함

    /// <summary>
    /// 다이얼로그 대사 데이터
    /// </summary>
    [System.Serializable]
    public struct DialogData
    {
        public string speakerName;
        [TextArea] public string dialogContent;
    }

    /// <summary>
    /// 다이얼로그에 등장하는 캐릭터 이미지 데이터
    /// </summary>
    [System.Serializable]
    public struct DialogSpriteData
    {
        [Header("---Dialog Sprite---")]
        public bool isTranslucent;
        public Vector2 spritePosition;
        public Sprite characterSprite;
    }


    public override void Execute()
    {

    }
}
