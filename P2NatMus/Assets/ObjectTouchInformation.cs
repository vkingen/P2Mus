using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectTouchInformation : MonoBehaviour
{
    ScanningManager scanningManager;

    public GameObject[] textBoxToToggle;

    private bool _isActive = false;


    private void Start()
    {
        scanningManager = FindObjectOfType<ScanningManager>();

        foreach (GameObject item in textBoxToToggle)
        {
            item.SetActive(false);
        }
    }
    public void ToggleInfoBox()
    {
        if(!_isActive)
        {
            textBoxToToggle[scanningManager.currentScannedObject].SetActive(true);
            _isActive = true;
        }
        else
        {
            textBoxToToggle[scanningManager.currentScannedObject].SetActive(false);
            _isActive = false;
        }            
    }
}
