using UnityEngine;


public class StartScene_Manager : MonoBehaviour
{
    public static StartScene_Manager instance;

    [Header("---UI---")]
    [SerializeField] private GameObject uiSet;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    #region Click_Event
    public void Click_Start()
    {
        Load_Manager.LoadScene("Main_Scene");
    }

    public void Click_Exit()
    {
        // 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    #endregion
}
