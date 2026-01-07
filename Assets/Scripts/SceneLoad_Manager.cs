using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class SceneLoad_Manager : MonoBehaviour
{
    [Header("---Setting---")]
    public static string sceneName;
    public static string stageName;
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
    public static void LoadScene(string sceneName, string stageNameText)
    {
        SceneLoad_Manager.sceneName = sceneName;
        SceneLoad_Manager.stageName = stageNameText;
        SceneManager.LoadScene("Loading_Scene");
    }


    private void Start()
    {
        if (loadCoroutine != null) StopCoroutine(loadCoroutine);
        loadCoroutine = StartCoroutine(Load());
        Tip();
        StageName();
    }

    private void Tip()
    {
        tipText.text = tips[Random.Range(0, tips.Length)];
    }

    private void StageName()
    {
        stageNameText.text = stageName;
    }

    private IEnumerator Load()
    {
        // 로딩 로직
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
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
                SceneLoad_Manager.sceneName = "";
                SceneLoad_Manager.stageName = "";
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
