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
        // 1. ���� ����
        curWaveIndex = 0;
        curEnemySpawnIndex = 0;

        // �� ���� �ݺ�
        while (curWaveIndex < enemyWave.Count)
        {
            // 2. ���� & ���� ����
            enemyCount = enemyWave[curWaveIndex].enemys.Count;
            if (enemyCount <= 0)
            {
                curWaveIndex++;
                enemyCount = 0;
                curEnemySpawnIndex = 0;
            }
            
            Spawn(enemyWave[curWaveIndex]);

            // 3. �÷��̾� ���
            yield return new WaitWhile(() => isPlayerSelect);

            // 4. ���� ����

            // 5. �� üũ
        }

        // 6. �������� ����
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
