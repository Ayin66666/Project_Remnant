using System.Collections.Generic;
using UnityEngine;


public class EgoUI : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private List<EgoContainerUI> egoContainerList;

    [Header("---UI---")]
    [SerializeField] private RectTransform containerRect;
    [SerializeField] private GameObject[] egoButton;

    [Header("---Prefab---")]
    [SerializeField] private GameObject egoContainerUIPrefab;


    public void SetUp(OrganizationData data)
    {
        Clear();

        for (int i = 0; i < data.ego.Count; i++)
        {
            GameObject obj = Instantiate(egoContainerUIPrefab, containerRect);
            EgoContainerUI container = obj.GetComponent<EgoContainerUI>();
            container.SetUp(data.identity, data.ego[i]);

            if (data.ego[i].master.egoRank == Rank.ZAYIN)
                obj.SetActive(true);
            else 
                obj.SetActive(false);

            egoContainerList.Add(container);
        }
    }

    /// <summary>
    /// 리스트 초기화
    /// </summary>
    public void Clear()
    {
        egoContainerList.Clear();
    }

    /// <summary>
    /// 버튼 이벤트
    /// </summary>
    /// <param name="index"></param>
    public void ClickButton(int index)
    {
        Rank targetRank = (Rank)index;
        foreach (EgoContainerUI co in egoContainerList)
        {
            if(co.Ego.master.egoRank == targetRank) 
                co.gameObject.SetActive(true);
            else 
                co.gameObject.SetActive(false);
        }
    }
}
