﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTemp : MonoBehaviour
{
    SceneManagerTemp instance;

    private void Start()
    {
        if (instance != null) // && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    bool hasBeenActivatedWithAwesomeDelayHomie = false;
    public void ChangeSceneWithDelay()
    {
        if(!hasBeenActivatedWithAwesomeDelayHomie)
            StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);
        hasBeenActivatedWithAwesomeDelayHomie = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
