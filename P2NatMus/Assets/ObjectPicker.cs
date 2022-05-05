using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    int currentScannedObject;
    ScanningManager scanningManager;
    private void Start()
    {
        scanningManager = FindObjectOfType<ScanningManager>();
    }
    public void GetScannedObjectNumber(int objectNumber)
    {
        currentScannedObject = objectNumber;
        scanningManager.GetCurrentScannedObject(currentScannedObject);
    }
}
