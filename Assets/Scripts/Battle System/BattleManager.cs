using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Game.Character;
using TMPro;


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("---Battle Setting---")]
    [SerializeField] private Phase curPhase;
    public enum Phase { StageStart, Select, Battle, Event, StageEnd }

    [Header("---Stage Setting---")]
    [SerializeField] private int waveNum;
    [SerializeField] private BattleStageSO stageSO;
    [SerializeField] private List<WaveRuntimeData> spawnDatas;
    [SerializeField] private List<SpawnPoint> spawnPoints;
    private Coroutine logicCoroutine;
    private Coroutine uiCoroutine;

    [Header("---Background---")]
    [SerializeField] private SpriteRenderer floor;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform wallRect;
    [SerializeField] private List<GameObject> wall;

    [Header("---Component---")]
    [SerializeField] private Volume postprocessing;
    [SerializeField] private AudioSource audioSource;

    [Header("---UI---")]
    [SerializeField] private bool isUIEvent;
    [SerializeField] private CanvasGroup fadeUI;
    [SerializeField] private CanvasGroup bossUI;
    [SerializeField] private CanvasGroup waveUI;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private CanvasGroup turnUI;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private CanvasGroup clearUI;


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
            // 페이즈 별 몬스터 List 데이터 생성
            List<CharacterBase> enemyList = new List<CharacterBase>();
            for (int j = 0; j < stageSO.PhaseDataList[i].spwanDataList.Count; j++)
            {
                // 몬스터 생성
                SpawnData spawnData = stageSO.PhaseDataList[i].spwanDataList[j];
                GameObject enemy = Instantiate(spawnData.enemy.prefab);

                // 몬스터에 데이터 삽입
                CharacterBase enemyBase = enemy.GetComponent<CharacterBase>();
                enemyBase.SetUp(spawnData.enemy.statData, spawnData.level, 1);

                // 리스트에 추가
                enemyList.Add(enemyBase);
            }

            // 런타임 데이터 생성
            WaveRuntimeData runtimeData = new WaveRuntimeData(enemyList);
            spawnDatas.Add(runtimeData);
        }

        // 시작 UI & 이벤트 & 전투 진입 로직 호출
        if (logicCoroutine != null) StopCoroutine(logicCoroutine);
        logicCoroutine = StartCoroutine(StageStart());
    }

    /// <summary>
    /// 스테이지 시작 시 연출 제어 로직 
    /// (페이드 아웃 -> 시작 UI -> 시작 이벤트 -> 전투 진입 순)
    /// </summary>
    /// <returns></returns>
    private IEnumerator StageStart()
    {
        curPhase = Phase.StageStart;

        // 0. 페이드 아웃
        if (uiCoroutine != null) StopCoroutine(uiCoroutine);
        uiCoroutine = StartCoroutine(Fade(true));

        yield return new WaitWhile(() => isUIEvent);

        // 1. 시작 UI 동작 (스테이지 타입에 따른 일반 & 보스전 UI 연출)
        if (uiCoroutine != null) StopCoroutine(uiCoroutine);
        uiCoroutine = StartCoroutine(stageSO.Type == BattleStageSO.StageType.Normal
            ? WaveUI() : BossUI());

        // 이벤트 대기
        yield return new WaitWhile(() => isUIEvent);

        // 2. 시작 이벤트
        // 일단 없다 가정 

        // 3. 전투 로직 시작
    }
    #endregion


    #region 스테이지 설정 로직
    /// <summary>
    /// 맵 배치 & 포스트 프로세싱 세팅
    /// </summary>
    /// <param name="so"></param>
    /// <param name="phase"></param>
    private void StageSetting(int phase)
    {
        // 초기화
        Debug.Log("초기화");
        floor.sprite = null;
        wall.Clear();

        // 맵 배치
        floor.sprite = stageSO.PhaseDataList[phase].floor;
        if (stageSO.PhaseDataList[phase].haveChageableBackground)
        {
            Debug.Log("맵 배치");
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
            Debug.Log("포스트 프로세싱 세팅");
            postprocessing.profile = stageSO.PhaseDataList[phase].postProcessingProfile;
        }
    }

    /// <summary>
    /// 스테이지 BGM 세팅
    /// </summary>
    /// <param name="index"></param>
    private void BGMSetting(int index)
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
    private void EnemySpawn()
    {
        // 해당 페이즈의 몬스터 소환
        // -> 만약 남은 몬스터가 없다면 다음 페이즈로 전환
        // 1. 웨이브 체크 (남은 웨이브가 있는가?)
        // 2. 몬스터 소환

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
    /// <summary>
    /// 플레이어의 스킬 & 대상 선택 턴
    /// </summary>
    public void SelectPhase()
    {

    }

    /// <summary>
    /// 전투 턴
    /// </summary>
    public void BattlePhase()
    {

    }
    #endregion


    #region UI 로직
    /// <summary>
    /// 화면 페이드 효과
    /// </summary>
    /// <param name="isOn"></param>
    /// <returns></returns>
    private IEnumerator Fade(bool isOn)
    {
        isUIEvent = true;

        fadeUI.gameObject.SetActive(true);

        float start = isOn ? 0f : 1f;
        float end = isOn ? 1f : 0f;
        float timer = 0f;
        fadeUI.alpha = start;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            fadeUI.alpha = Mathf.Lerp(timer, start, end);
            yield return null;
        }
        fadeUI.alpha = end;

        if (!isOn)
            fadeUI.gameObject.SetActive(false);

        isUIEvent = false;
    }

    /// <summary>
    /// 웨이브 표시 UI - 전투 시작 & 웨이브 변경 시 호출
    /// </summary>
    /// <param name="waveNum"></param>
    /// <returns></returns>
    private IEnumerator WaveUI()
    {
        isUIEvent = true;

        // UI 세팅
        waveText.text = $"WAVE {waveNum}";
        waveUI.gameObject.SetActive(true);

        // 페이드 인
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            waveUI.alpha = Mathf.Lerp(0f, 1f, timer);
            yield return null;
        }
        timer = 0;

        // 대기
        yield return new WaitForSeconds(0.75f);

        // 페이드 아웃
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            waveUI.alpha = Mathf.Lerp(1f, 0f, timer);
            yield return null;
        }

        waveUI.gameObject.SetActive(false);
        isUIEvent = false;
    }

    /// <summary>
    /// 보스전 시작 시 호출 - 특수 연출
    /// </summary>
    /// <returns></returns>
    private IEnumerator BossUI()
    {
        isUIEvent = true;

        // UI 표시
        bossUI.gameObject.SetActive(true);

        // 대기
        yield return new WaitForSeconds(1f);

        // 페이드 아웃
        float tiemr = 0;
        while (tiemr < 1f)
        {
            tiemr += Time.deltaTime;
            bossUI.alpha = Mathf.Lerp(1f, 0f, tiemr);
            yield return null;
        }

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

        // 페이드 인
        clearUI.gameObject.SetActive(true);

        // 대기
        yield return new WaitForSeconds(1f);

        // 페이드 아웃
        float timer = 0;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            clearUI.alpha = Mathf.Lerp(1f, 0f, timer);
            yield return null;
        }

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
        public List<CharacterBase> enemyList;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="so"></param>
        public WaveRuntimeData( List<CharacterBase> enemyList)
        {
            totalCount = enemyList.Count;
            currentCount = 0;
            this.enemyList = enemyList;
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
