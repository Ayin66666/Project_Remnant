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

    }

    public void Click_Extra()
    {

    }

    public void Click_Option()
    {

    }

    public void Click_Exit()
    {

    }
    #endregion
}
