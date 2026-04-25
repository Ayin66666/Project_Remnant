using Game.Character;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("---Stage Setting---")]
    [SerializeField] private BattleStageSO stageSO;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<Transform> spawnPoints;

    [Header("---Background---")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private RectTransform wallRect;
    [SerializeField] private SpriteRenderer floor;
    [SerializeField] private List<GameObject> wall;

    [Header("---Post Processing---")]
    [SerializeField] private Volume postprocessing;

    [Header("---Audio---")]
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

    /// <summary>
    /// 스테이지 데이터를 받아서 맵 & 몬스터 세팅
    /// </summary>
    public void SetUp(BattleStageSO so)
    {
        // 데이터 세팅
        stageSO = so;

        // 맵 세팅
        StageSetting(so, 0);
        BGMSetting(0);

        // 몬스터 세팅

        // 전투 시작 UI
        if(true)
        {
            // 1. 특수연출이라면
        }
        else
        {
            // 2. 일반전이라면
        }
    }

    /// <summary>
    /// 맵 배치 & 포스트 프로세싱 세팅
    /// </summary>
    /// <param name="so"></param>
    /// <param name="phase"></param>
    public void StageSetting(BattleStageSO so, int phase)
    {
        // 초기화
        floor.sprite = null;
        wall.Clear();

        // UI 배치
        floor.sprite = so.PhaseDataList[phase].floor;
        for (int i = 0; i < wall.Count; i++)
        {
            GameObject wallObj = Instantiate(wallPrefab, wallRect.transform);
            SpriteRenderer renderer = wallObj.GetComponent<SpriteRenderer>();
            renderer.sprite = so.PhaseDataList[phase].wall;
            wall.Add(wallObj);
        }

        // 포스트 프로세싱 세팅
        if (so.PhaseDataList[phase].postProcessingProfile != null)
        {
            postprocessing.profile = so.PhaseDataList[phase].postProcessingProfile;
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
    public void Spawn(int phase)
    {
        foreach(SpawnData data in stageSO.PhaseDataList[phase].enemies)
        {
            // 몬스터 소환
            GameObject obj = Instantiate(data.enemy.prefab, spawnPoints[data.spawnNum].position, Quaternion.identity);
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
}
