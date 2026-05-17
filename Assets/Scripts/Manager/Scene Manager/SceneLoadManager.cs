using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoadManager : MonoBehaviour
{
    [Header("---Setting---")]
    public static StageData stageData;
    private Coroutine loadCoroutine;
    [SerializeField, TextArea] private string[] tips;

    [Header("---UI---")]
    [SerializeField] private Slider progressbar;
    [SerializeField] private TextMeshProUGUI tipText;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private Image backgroundImage;


    /// <summary>
    /// 씬 호출
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(StageData data)
    {
        Debug.Log(data == null ? "null" : data);

        // 데이터 세팅
        stageData = data;

        // 씬 이동
        SceneManager.LoadScene("Loading_Scene");
    }


    #region 씬 로직
    private void Start()
    {
        if (loadCoroutine != null) StopCoroutine(loadCoroutine);
        loadCoroutine = StartCoroutine(Load());

        SetUpUI();
    }

    private void SetUpUI()
    {
        stageNameText.text = stageData.stageSO.StageName;
        tipText.text = tips[Random.Range(0, tips.Length)];
    }

    private IEnumerator Load()
    {
        // 로딩 로직
        AsyncOperation operation = SceneManager.LoadSceneAsync(stageData.stageSO.SceneName);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime / 3);
            }
            else if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                // Scene Move
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
    #endregion
}
