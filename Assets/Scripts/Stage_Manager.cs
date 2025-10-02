using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyWave
{
    [SerializeField] private string waveName;
    public List<GameObject> enemys;
}


public class Stage_Manager : MonoBehaviour
{
    public static Stage_Manager instance;

    [Header("---Stage Setting---")]
    public int turnCount = 0;
    public bool isPlayerSelect;
    public bool isFight;
    private AudioSource audioSource;


    [Header("---Evnet---")]
    [SerializeField] private List<StageEvnet> stageEvent;

    [System.Serializable]
    public struct StageEvnet
    {
        public EventType eventType;
        public EvnetPos eventPos;
        public int evnetCount;
        public Character_Base character;

        public enum EvnetPos { Stage_Start, Count, Kill, Stage_End };
        public enum EventType { Dialog, CutScene }
    }


    [Header("---Enemy---")]
    [SerializeField] private int curWaveIndex;
    [SerializeField] private int curEnemySpawnIndex;
    [SerializeField] private int enemyCount;
    [SerializeField] private List<Spawn_Slot> spawnSlot;
    public List<EnemyWave> enemyWave;


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

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(Stage_Start());
    }


    public IEnumerator Stage_Start()
    {


        // 1. ���� ����
        curWaveIndex = 0;
        curEnemySpawnIndex = 0;
        enemyCount = enemyWave[curWaveIndex].enemys.Count;

        // �÷��̾� ���� ������ ����
        Player_Manager.instance.EgoData_Setting();


        // ���� �̺�Ʈ üũ
        foreach (StageEvnet stageEvent in stageEvent)
        {
            if(stageEvent.eventPos == StageEvnet.EvnetPos.Stage_Start)
            {

            }
        }

        // ���̺� ��ŭ �ݺ�
        while (curWaveIndex < enemyWave.Count)
        {
            turnCount++;

            // �̺�Ʈ üũ
            Event_Check();

            // ���̺� ���� üũ
            if (enemyCount <= 0)
            {
                // ���̺� ����
                curWaveIndex++;

                // ��ü ���̺� ���� �� while�� Ż��
                if (curWaveIndex >= enemyWave.Count) break;

                // ���� ���̺� ����
                curEnemySpawnIndex = 0;
                enemyCount = enemyWave[curWaveIndex].enemys.Count;
            }

            // 2. ���� ��ȯ
            Spawn(enemyWave[curWaveIndex]);

            // 3. �÷��̾� ���
            yield return new WaitWhile(() => isPlayerSelect);

            // 4. ���� ����
            StartCoroutine(Fight());
            yield return new WaitWhile(() => isFight);
        }

        // 5. �������� ����
        StartCoroutine(Stage_End());
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

    /// <summary>
    /// ���� �ʿ�
    /// </summary>
    public void Event_Check()
    {
        for (int i = 0; i < stageEvent.Count; i++)
        {
            switch (stageEvent[i].eventPos)
            {
                case StageEvnet.EvnetPos.Count:
                    break;

                case StageEvnet.EvnetPos.Kill:
                    break;
            }
        }
    }

    /// <summary>
    ///  ���� �ʿ�
    /// </summary>
    /// <returns></returns>
    public IEnumerator Fight()
    {
        isFight = true;

        // 1. ���� ���� ����

        // 2. �ӵ� �� ���� ȣ��

        yield return null;
        isFight = false;
    }

    private IEnumerator Stage_End()
    {
        // ���� �̺�Ʈ üũ
        foreach (StageEvnet stageEvent in stageEvent)
        {
            if (stageEvent.eventPos == StageEvnet.EvnetPos.Stage_End)
            {

            }
        }

        // ���� UI ǥ��
        yield return null;

        // �������� �̵�
    }
}
