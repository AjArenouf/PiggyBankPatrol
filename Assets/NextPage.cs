using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPage : MonoBehaviour
{
    public Button next;
    public GameObject homeCanvas;
    public GameObject openingCanvas;
    public GameObject ConstantNav;
    // Start is called before the first frame update
    void Start()
    {
        next.onClick.AddListener(OpenHomeCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OpenHomeCanvas()
    {
        homeCanvas.SetActive(true);
        openingCanvas.SetActive(false);
        ConstantNav.SetActive(true);
     
    }
}
