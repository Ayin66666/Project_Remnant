using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static BattleManager;


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("---Stage Setting---")]
    [SerializeField] private int curPhase;
    [SerializeField] private BattleStageSO stageSO;
    [SerializeField] private List<WaveRuntimeData> spawnDatas;
    [SerializeField] private List<SpwanPoint> spawnPoints;

    [Header("---Background---")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private RectTransform wallRect;
    [SerializeField] private SpriteRenderer floor;
    [SerializeField] private List<GameObject> wall;

    [Header("---Post Processing---")]
    [SerializeField] private Volume postprocessing;

    [Header("---Audio---")]
    [SerializeField] private AudioSource audioSource;


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
        for(int i = 0; i < stageSO.PhaseDataList.Count; i++)
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

    #region 진행 로직
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
        if(stageSO.PhaseDataList[phase].haveChageableBackground)
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
    /// 소환 로직 구현 중
    /// </summary>
    /// <param name="phase"></param>
    public void Spawn()
    {
        // 페이즈 phase 번의 몬스터 소환
        // -> 만약 남은 몬스터가 없다면 다음 페이즈로 전환

        if(spawnDatas[curPhase].currentCount < spawnDatas[curPhase].totalCount)
        {
            // 몬스터 소환
            spawnDatas[curPhase].currentCount++;
            GameObject enemy = spawnDatas[curPhase].enemyList[spawnDatas[curPhase].currentCount];

            // 소환 위치 찾기
            foreach (SpwanPoint point in spawnPoints)
            {
                if(point.character == null)
                {
                    point.character = enemy.GetComponent<CharacterBase>();
                    enemy.transform.position = point.spawnPos.position;
                    enemy.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            // 다음 페이즈로 전환
            curPhase++;

            StageSetting(curPhase);
            BGMSetting(curPhase);
        }
    }

    /// <summary>
    /// 이벤트 체크
    /// </summary>
    public void EventCheck()
    {

    }

    /// <summary>
    /// 스테이지 상태 체크 (클리어 조건, 플레이어 패배 등등)
    /// </summary>
    public void ClearCheck()
    {
        // 클리어 조건은 크게 다음과 같이 나뉨
        // 1. 모든 적 처치
        // 2. N턴 버티기
        // 3. 조건 몬스터의 체력 N 이하
    }
    #endregion


    #region UI 로직
    #endregion


    #region 데이터 클래스
    [System.Serializable]
    public class WaveRuntimeData
    {
        public int totalCount;
        public int currentCount;
        public List<GameObject> enemyList;

        public WaveRuntimeData(BattleStageSO.PhaseData so)
        {
            // 몬스터 수 세팅
            totalCount = so.enemies.Count;
            currentCount = 0;

            // 몬스터 소환 후 리스트 채우기
            for(int i = 0; i < so.enemies.Count; i++)
            {
                for(int j = 0; j < so.enemies[i].spawnNum; j++)
                {
                    GameObject obj = Instantiate(so.enemies[i].enemy.prefab);
                    obj.SetActive(false);
                    enemyList.Add(obj);
                }
            }
        }
    }


    [System.Serializable]
    public class  SpwanPoint
    {
        public CharacterBase character;
        public Transform spawnPos;
    }
    #endregion
}
