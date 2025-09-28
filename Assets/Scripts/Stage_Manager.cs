using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave
{
    [SerializeField] private string waveName;
    public bool spawnWhenCleared;
    public List<GameObject> enemys;
}


public class Stage_Manager : MonoBehaviour
{
    public static Stage_Manager instance;
    public bool isPlayerSelect;

    [Header("---Enemy---")]
    [SerializeField] private int curWaveIndex;
    [SerializeField] private int curEnemySpawnIndex;
    [SerializeField] private int enemyCount;
    [SerializeField] private List<Spawn_Slot> spawnSlot;
    public List<EnemyWave> enemyWave;


    [Header("---Turn---")]
    [SerializeField] private int turnCount = 0;


    [Header("---Sound---")]
    [SerializeField] private AudioSource audioSource;


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

    public IEnumerator Stage_Start()
    {
        // 1. 시작 셋팅
        curWaveIndex = 0;
        curEnemySpawnIndex = 0;

        // 턴 로직 반복
        while (curWaveIndex < enemyWave.Count)
        {
            // 2. 스폰 & 몬스터 보충
            enemyCount = enemyWave[curWaveIndex].enemys.Count;
            if (enemyCount <= 0)
            {
                curWaveIndex++;
                enemyCount = 0;
                curEnemySpawnIndex = 0;
            }
            
            Spawn(enemyWave[curWaveIndex]);

            // 3. 플레이어 대기
            yield return new WaitWhile(() => isPlayerSelect);

            // 4. 전투 진행

            // 5. 맵 체크
        }

        // 6. 스테이지 종료
    }

    public void Spawn(EnemyWave wave)
    {
        for (int i = 0; i < spawnSlot.Count; i++)
        {
            if (!spawnSlot[i].haveCharacter && curEnemySpawnIndex < wave.enemys.Count)
            {
                // wave.enemys[curEnemySpawnIndex].Spawn();
                curEnemySpawnIndex++;
            }
        }
    }

    public void EnemyDie()
    {
        enemyCount--;
    }
}
