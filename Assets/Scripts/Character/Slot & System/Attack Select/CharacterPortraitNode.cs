using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterPortraitNode : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private PlayerCharacter character;

    [Header("---UI---")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image mentalImage;
    [SerializeField] private Image chargeImage;
    [SerializeField] private TextMeshProUGUI mentalText;
    [SerializeField] private Gradient gradient;


    /// <summary>
    /// 데이터 세팅 함수
    /// </summary>
    /// <param name="character"></param>
    public void SetUp(PlayerCharacter character)
    {
        // 데이터
        this.character = character;

        // UI
        portraitImage.sprite = character.IdentityMasterSO.portrait;
        mentalText.text = character.Mentality.ToString();
    }

    /// <summary>
    /// 정신력 수치 & 이미지 컬러 업데이트 함수
    /// </summary>
    public void UpdataMental()
    {
        float t = Mathf.InverseLerp(-45f, 45f, character.Mentality);
        mentalImage.color = gradient.Evaluate(t);
    }
}
