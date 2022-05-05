using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // delete after testing

public class ScanningManager : MonoBehaviour
{
    public int currentScannedObject;
    public bool[] scannedObjects;

    private static ScanningManager _instance;
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
    public void GetCurrentScannedObject(int objectNumber)
    {
        currentScannedObject = objectNumber;
        scannedObjects[currentScannedObject] = true;
    }
}
