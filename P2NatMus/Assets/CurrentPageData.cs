using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPageData : MonoBehaviour
{
    [HideInInspector]
    public int currentPage = 1;

    private static CurrentPageData _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeCurrentPageOnSceneLoad(int pageNumber)
    {
        currentPage = pageNumber;
    }
}
