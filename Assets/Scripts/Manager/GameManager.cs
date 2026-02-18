using UnityEngine;


public class GameManager : MonoBehaviour
{
    // ¸ðµç ±â´É ÃÑ°ý? À¯ÀÏÇÑ ½Ì±ÛÅæ?
    public static GameManager instance;

    [Header("---Manager---")]
    public SaveDataManager saveDataManager;
    public OptionManager optionManager;
    public ExpManager expManager;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
