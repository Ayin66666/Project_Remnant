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


        // 1. 시작 셋팅
        curWaveIndex = 0;
        curEnemySpawnIndex = 0;
        enemyCount = enemyWave[curWaveIndex].enemys.Count;

        // 플레이어 에고 데이터 설정
        Player_Manager.instance.EgoData_Setting();


        // 시작 이벤트 체크
        foreach (StageEvnet stageEvent in stageEvent)
        {
            if(stageEvent.eventPos == StageEvnet.EvnetPos.Stage_Start)
            {

            }
        }

        // 웨이브 만큼 반복
        while (curWaveIndex < enemyWave.Count)
        {
            turnCount++;

            // 이벤트 체크
            Event_Check();

            // 웨이브 종료 체크
            if (enemyCount <= 0)
            {
                // 웨이브 증가
                curWaveIndex++;

                // 전체 웨이브 종료 시 while문 탈출
                if (curWaveIndex >= enemyWave.Count) break;

                // 다음 웨이브 실행
                curEnemySpawnIndex = 0;
                enemyCount = enemyWave[curWaveIndex].enemys.Count;
            }

            // 2. 몬스터 소환
            Spawn(enemyWave[curWaveIndex]);

            // 3. 플레이어 대기
            yield return new WaitWhile(() => isPlayerSelect);

            // 4. 전투 진행
            StartCoroutine(Fight());
            yield return new WaitWhile(() => isFight);
        }

        // 5. 스테이지 종료
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
    /// 제작 필요
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
    ///  제작 필요
    /// </summary>
    /// <returns></returns>
    public IEnumerator Fight()
    {
        isFight = true;

        // 1. 공격 순서 정리

        // 2. 속도 순 공격 호출

        yield return null;
        isFight = false;
    }

    private IEnumerator Stage_End()
    {
        // 종료 이벤트 체크
        foreach (StageEvnet stageEvent in stageEvent)
        {
            if (stageEvent.eventPos == StageEvnet.EvnetPos.Stage_End)
            {

            }
        }

        // 종료 UI 표시
        yield return null;

        // 스테이지 이동
    }
}
