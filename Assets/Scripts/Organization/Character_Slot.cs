using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Character_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    // �ܼ� Ŭ�� = selected ǥ��
    // �� ������ = �� ����
    [Header("---UI---")]
    public Image portraitImage;
    public Image loadImage;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nameText;
    public GameObject[] gradeIcon;
    public GameObject mouseOverText;
    public GameObject selectedText;


    [Header("---Character Data---")]
    public Character character;
    [SerializeField] private Player_Base body;


    [Header("---Click ---")]
    private float clickTimer = 0f;
    private bool isPressing = false;
    private Coroutine clickCoroutine;


    public void Setting(Character character, Player_Base body)
    {
        this.character = character;
        this.body = body;
    }

    public void Reset_Slot()
    {
        portraitImage.sprite = null;
        levelText.text = null;
        nameText.text = null;
        mouseOverText.SetActive(false);
        selectedText.SetActive(false);

        foreach (GameObject go in gradeIcon)
        {
            go.SetActive(false);
        }
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
        if (clickCoroutine != null) StopCoroutine(clickCoroutine);
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
            // �ٸ� ������ selectedText Off
            UI_Manager.instance.SelectedOff();

            // ������ �ΰ� ���� & selected ǥ�� 
            selectedText.SetActive(true);
            Organization_Manager.instance.Change_Body(character, body);
        }
        else
        {
            // �ΰ� �� ����
            UI_Manager.instance.Character_Description(true, body);
        }
    }

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
