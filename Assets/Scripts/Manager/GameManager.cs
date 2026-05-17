using UnityEngine;


public class GameManager : MonoBehaviour
{
    // 모든 기능 총괄 & 매니저 관리
    public static GameManager instance;

    [Header("---Manager---")]
    public SaveDataManager saveDataManager;
    public OptionManager optionManager;
    public LevelManager levelManager;
    public InventoryManager inventory;

    [Header("---Data---")]
    public StageData tutorialData;
    public StageData mainSceneData;

    [Header("---UI---")]
    public CharacterDescription characterDescription;


    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 24;
    }
}
