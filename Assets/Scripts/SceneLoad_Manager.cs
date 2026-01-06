using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class SceneLoad_Manager : MonoBehaviour
{
    [Header("---Setting---")]
    public static string sceneName;
    private Coroutine loadCoroutine;

    [Header("---UI---")]
    [SerializeField] private Slider progressbar;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private CanvasGroup tipCanvasGroup;
    [SerializeField] private TextMeshProUGUI tipText;


    /// <summary>
    /// 씬 호출
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        SceneLoad_Manager.sceneName = sceneName;
        SceneManager.LoadScene("Loading_Scene");
    }


    private void Start()
    {
        if (loadCoroutine != null) StopCoroutine(loadCoroutine);
        loadCoroutine = StartCoroutine(Load());
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

            if (progressbar.value >= 1f)
            {
                loadingText.text = "Press SpaceBar to Start.";
            }

            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                // Scene Move
                operation.allowSceneActivation = true;
            }


            yield return null;
        }
    }
}
