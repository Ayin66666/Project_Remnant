using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game_Manager : MonoBehaviour
{
    // ¸ðµç ±â´É ÃÑ°ý? À¯ÀÏÇÑ ½Ì±ÛÅæ?
    public static Game_Manager instance;

    [Header("---Manager---")]
    public SaveData_Manager saveData_Manager;
    public Option_Manager option_Manager;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
