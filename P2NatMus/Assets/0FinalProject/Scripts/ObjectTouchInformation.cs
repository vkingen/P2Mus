﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectTouchInformation : MonoBehaviour
{
    private ScanningManager _scanningManager;

    public GameObject[] textBoxToToggle; // these textBoxToToggle gameobjects are the different information texts in the objectInspector scene

    private bool _isActive = false;


    private void Start()
    {
        _scanningManager = FindObjectOfType<ScanningManager>();

        foreach (GameObject item in textBoxToToggle) // Starting with setting all the textBoxToToggle gameobjects to false
        {
            item.SetActive(false);
        }
    }
    public void ToggleInfoBox()
    {
        if(!_isActive) // When the button is pushed this method toggles the appropriate textBoxToToggle gameobject on and off
        {
            textBoxToToggle[_scanningManager.currentScannedObject].SetActive(true);
            _isActive = true;
        }
        else
        {
            textBoxToToggle[_scanningManager.currentScannedObject].SetActive(false);
            _isActive = false;
        }            
    }
}
