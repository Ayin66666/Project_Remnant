using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // ¸ðµç ±â´É ÃÑ°ý? À¯ÀÏÇÑ ½Ì±ÛÅæ?
    public static GameManager instance;

    [Header("---Manager---")]
    public SaveDataManager saveData_Manager;
    public OptionManager option_Manager;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
