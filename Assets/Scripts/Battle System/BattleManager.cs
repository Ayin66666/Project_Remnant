using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Game.Character;


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("---Stage Setting---")]
    [SerializeField] private int curPhase;
    [SerializeField] private BattleStageSO stageSO;
    [SerializeField] private List<WaveRuntimeData> spawnDatas;
    [SerializeField] private List<SpawnPoint> spawnPoints;

    [Header("---Background---")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private RectTransform wallRect;
    [SerializeField] private SpriteRenderer floor;
    [SerializeField] private List<GameObject> wall;

    [Header("---Post Processing---")]
    [SerializeField] private Volume postprocessing;

    [Header("---Audio---")]
    [SerializeField] private AudioSource audioSource;

    [Header("---UI---")]
    [SerializeField] private bool isUIEvent;
    [SerializeField] private List<CanvasGroup> startUI;
    [SerializeField] private List<GameObject> clearUI;
    [SerializeField] private CanvasGroup fadeUI;


    #region 시작 로직
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

    /// <summary>
    /// 스테이지 데이터를 받아서 맵 & 몬스터 세팅
    /// </summary>
    public void SetUp(BattleStageSO so)
    {
        // 데이터 세팅
        stageSO = so;

        // 맵 세팅
        StageSetting(0);
        BGMSetting(0);

        // so 데이터 기반 런타임 데이터 생성
        for (int i = 0; i < stageSO.PhaseDataList.Count; i++)
        {
            // 데이터 생성
            WaveRuntimeData runtimeData = new WaveRuntimeData(stageSO.PhaseDataList[i]);
            spawnDatas.Add(runtimeData);
        }

        // 전투 시작 UI
        if (true)
        {
            // 1. 특수연출이라면
        }
        else
        {
            // 2. 일반전이라면
        }
    }
    #endregion


    #region 스테이지 설정 로직
    /// <summary>
    /// 맵 배치 & 포스트 프로세싱 세팅
    /// </summary>
    /// <param name="so"></param>
    /// <param name="phase"></param>
    public void StageSetting(int phase)
    {
        // 초기화
        floor.sprite = null;
        wall.Clear();

        // UI 배치
        if (stageSO.PhaseDataList[phase].haveChageableBackground)
        {
            floor.sprite = stageSO.PhaseDataList[phase].floor;
            for (int i = 0; i < wall.Count; i++)
            {
                GameObject wallObj = Instantiate(wallPrefab, wallRect.transform);
                SpriteRenderer renderer = wallObj.GetComponent<SpriteRenderer>();
                renderer.sprite = stageSO.PhaseDataList[phase].wall;
                wall.Add(wallObj);
            }
        }

        // 포스트 프로세싱 세팅
        if (stageSO.PhaseDataList[phase].postProcessingProfile != null)
        {
            postprocessing.profile = stageSO.PhaseDataList[phase].postProcessingProfile;
        }
    }

    /// <summary>
    /// 스테이지 BGM 세팅
    /// </summary>
    /// <param name="index"></param>
    public void BGMSetting(int index)
    {
        if (stageSO.PhaseDataList[index].phaseBgm == null)
        {
            Debug.Log("BGM이 존재하지 않습니다.");
            return;
        }

        audioSource.Pause();
        audioSource.clip = stageSO.PhaseDataList[index].phaseBgm;
        audioSource.Play();
    }

    /// <summary>
    /// 소환 로직 구현 중 -> 일단 기본적인건 만들었고 페이즈 체크 기능 필요!
    /// </summary>
    /// <param name="phase"></param>
    public void EnemySpawn()
    {
        // 해당 페이즈의 몬스터 소환
        // -> 만약 남은 몬스터가 없다면 다음 페이즈로 전환

        if (spawnDatas[curPhase].currentCount < spawnDatas[curPhase].totalCount)
        {
            // 소환해야 하는 몬스터 수 체크
            int spawnCount = 0;
            foreach (SpawnPoint point in spawnPoints)
            {
                if (point.character == null)
                    spawnCount++;
            }

            // 몬스터 소환
            for (int i = 0; i < spawnCount; i++)
            {
                // 소환 위치 설정
                SpawnPoint spawn = spawnPoints.FirstOrDefault(x => x.character == null);

                // 몬스터 배치 -> IdentityMasterSO에서 EnemyMasterSO로 변경했음으로 새로 구현 필요
                /*
                CharacterBase enemy = spawnDatas[curPhase].enemyList[spawnDatas[curPhase].currentCount];
                spawn.character = enemy;
                enemy.transform.position = spawn.spawnPos.position;
                enemy.SetUp();
                enemy.gameObject.SetActive(true);
                */
                // 소환된 몬스터 수 업데이트
                spawnDatas[curPhase].currentCount++;
            }
        }
        else
        {
            // 다음 페이즈가 있는지 체크
            if (curPhase + 1 >= stageSO.PhaseDataList.Count)
            {
                // 스테이지 클리어
                // 일단은 디버그만 찍지만, 실제로는 UI 표시 후 이벤트 체크, 스테이지에서 나가는 로직 필요
                Debug.Log("스테이지 클리어!");
            }
            else
            {
                // 다음 페이즈로 전환
                curPhase++;

                StageSetting(curPhase);
                BGMSetting(curPhase);
            }

        }
    }

    /// <summary>
    /// 스테이지 클리어 시 호출 (승리 UI, 스테이지 로딩)
    /// -> 이후 이벤트에 따른 대화나 연출같은 기능 추가 필요
    /// </summary>
    /// <returns></returns>
    private IEnumerator StageClear()
    {
        // 종료 UI 및 UI 이벤트 체크
        StartCoroutine(ClearUI((int)stageSO.Type));
        yield return new WaitWhile(() => isUIEvent);

        // 씬 전환
        SceneLoadManager.LoadScene(GameManager.instance.mainSceneData);
    }
    #endregion


    #region 전투 시스템 로직
    #endregion


    #region UI 로직
    /// <summary>
    /// 시작 타입에 따른 UI 연출 변경
    /// </summary>
    /// <param name="startType"></param>
    /// <returns></returns>
    public IEnumerator StartUI(int startType)
    {
        isUIEvent = true;

        // 페이드
        fadeUI.gameObject.SetActive(true);
        fadeUI.alpha = 1f;

        // 대기
        yield return new WaitForSeconds(0.5f);

        // 페이드 종료
        float timer = 0f;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            fadeUI.alpha = Mathf.Lerp(1f, 0f, timer);
            yield return null;
        }
        fadeUI.alpha = 1;

        // 시작 UI
        startUI[startType].gameObject.SetActive(true);

        // 시작 UI 종료
        while(timer < 1f)
        {
            timer += Time.deltaTime;
            startUI[startType].alpha = Mathf.Lerp(1f, 0f, timer);
            yield return null;
        }
        fadeUI.alpha = 0;
        startUI[startType].gameObject.SetActive(false);

        isUIEvent = false;
    }

    /// <summary>
    /// 클리어 타입에 따른 UI 연출 변경
    /// </summary>
    /// <param name="clearType">
    /// 0 = 일반  
    /// 1 = 보스 
    /// </param>
    /// <returns></returns>
    private IEnumerator ClearUI(int clearType)
    {
        isUIEvent = true;

        clearUI[clearType].SetActive(true);
        yield return new WaitForSeconds(0.5f);

        fadeUI.gameObject.SetActive(true);
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            fadeUI.alpha = Mathf.Lerp(0f, 1f, timer);
            yield return null;
        }
        fadeUI.alpha = 1f;

        isUIEvent = false;
    }
    #endregion


    #region 데이터 클래스
    [System.Serializable]
    public class WaveRuntimeData
    {
        /// <summary>
        /// 총 몬스터 수
        /// </summary>
        public int totalCount;
        /// <summary>
        /// 소환된 몬스터의 인덱스
        /// </summary>
        public int currentCount;
        /// <summary>
        /// 몬스터 오브젝트 리스트
        /// </summary>
        public List<EnemyMasterSO> enemyList;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="so"></param>
        public WaveRuntimeData(BattleStageSO.PhaseData so)
        {
            // 몬스터 수 세팅
            totalCount = so.enemies.Count;
            currentCount = 0;

            // 몬스터 소환 후 리스트 채우기
            enemyList = new List<EnemyMasterSO>();
            for (int i = 0; i < so.enemies.Count; i++)
            {
                for (int j = 0; j < so.enemies[i].spawnNum; j++)
                {
                    // 새로 구현 필요
                }
            }
        }
    }

    [System.Serializable]
    public class SpawnPoint
    {
        public CharacterBase character;
        public Transform spawnPos;
    }
    #endregion
}
