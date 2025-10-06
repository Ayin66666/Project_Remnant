using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Organization_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("---UI---")]
    public Image portraitImage;
    public Image loadImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nameText;
    public GameObject[] gradeIcon;
    public TextMeshProUGUI mouseOverText;

    [Header("---Character Data---")]
    [SerializeField] private Character character;
    [SerializeField] private Character_Base body;


    [Header("---Click ---")]
    private float clickTimer = 0f;
    private bool isPressing = false;
    private Coroutine clickCoroutine;


    public void Setting(Character character, Character_Base body)
    {
        this.character = character;
        this.body = body;
    }

    private IEnumerator ClickCheck()
    {
        loadImage.fillAmount = 0f;
        clickTimer = 0f;
        isPressing = true;
        while (isPressing)
        {
            clickTimer += Time.deltaTime;
            loadImage.fillAmount = clickTimer;
            yield return null;
        }

        clickTimer = 1f;
        loadImage.fillAmount = 1f;
        isPressing = false;
    }


    #region Click Evnet
    public void OnPointerDown(PointerEventData eventData)
    {
        if(clickCoroutine != null) StopCoroutine(clickCoroutine);
        clickCoroutine = StartCoroutine(ClickCheck());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (clickCoroutine != null) StopCoroutine(clickCoroutine);
        loadImage.fillAmount = 0;
        isPressing = false;

        // Ŭ�� �ð� üũ
        if (clickTimer < 1)
        {
            // ��
            Organization_Manager.instance.Change_Organization(character);
        }
        else
        {
            // �ΰ� ����
            Organization_Manager.instance.CharacterList_Setting(character, true);
        }
    }

    // �ܼ� Ŭ�� = selected ǥ��
    // �� ������ = �� ����

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverText.gameObject.SetActive(false);
    }
    #endregion
}
