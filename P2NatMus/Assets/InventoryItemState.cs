using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItemState : MonoBehaviour
{
    public int inventoryItemNumber;
    public bool hasBeenScanned = false;
    public GameObject[] icon;
    Button button;
    ScanningManager scanningManager;

    private void Start()
    {
        button = GetComponent<Button>();
        scanningManager = FindObjectOfType<ScanningManager>();

        if (scanningManager.scannedObjects[inventoryItemNumber] == true)
            hasBeenScanned = true;

        foreach (GameObject item in icon)
        {
            item.SetActive(false);
        }
        if(!hasBeenScanned)
        {
            icon[0].SetActive(true);
            button.enabled = false;
        }
        else
        {
            icon[1].SetActive(true);
            button.enabled = true;
        }
    }

}
