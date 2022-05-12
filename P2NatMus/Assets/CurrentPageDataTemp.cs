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
        currentPageData.ChangeCurrentPageOnSceneLoad(pageNumber);
    }
}
