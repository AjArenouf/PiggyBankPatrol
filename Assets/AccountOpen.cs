using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountOpen : MonoBehaviour
{
    public GameObject accountCanvas;
    public Button account;

    private bool isCanvasActive = false;

    // Start is called before the first frame update
    void Start()
    {
        account.onClick.AddListener(ToggleAccountCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleAccountCanvas()
    {
        isCanvasActive = !isCanvasActive;

        accountCanvas.SetActive(isCanvasActive);
    }
}
