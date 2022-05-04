using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPageDataTemp : MonoBehaviour
{
    CurrentPageData currentPageData;

    private void Start()
    {
        currentPageData = FindObjectOfType<CurrentPageData>();
    }
    public void ChangeCurrentPageOnSceneLoad(int pageNumber)
    {
        //currentPageData.currentPage = pageNumber;
        currentPageData.ChangeCurrentPageOnSceneLoad(pageNumber);
        Debug.Log("PAGE NUMBER IS " + currentPageData.currentPage);
    }
}
