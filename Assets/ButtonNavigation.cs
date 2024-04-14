using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNavigation : MonoBehaviour
{
    public GameObject homeCanvas;
    public GameObject storeCanvas;
    public GameObject socialCanvas;
    public GameObject accountCanvas;

    public Button home;
    public Button store;
    public Button social;
    public Button account;
    // Start is called before the first frame update
    void Start()
    {
        home.onClick.AddListener(OpenHomeCanvas);
        store.onClick.AddListener(OpenStoreCanvas);
        social.onClick.AddListener(OpenSocialCanvas);
        account.onClick.AddListener(OpenAccountCanvas);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OpenHomeCanvas()
    {
        homeCanvas.SetActive(true);
        storeCanvas.SetActive(false);
        socialCanvas.SetActive(false);
        accountCanvas.SetActive(false);
    }
    void OpenStoreCanvas()
    {
        homeCanvas.SetActive(false);
        storeCanvas.SetActive(true);
        socialCanvas.SetActive(false);
        accountCanvas.SetActive(false);
    }

    void OpenSocialCanvas()
    {
        homeCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        socialCanvas.SetActive(true);
        accountCanvas.SetActive(false);
    }

    void OpenAccountCanvas()
    {
        homeCanvas.SetActive(false);
        storeCanvas.SetActive(false);
        socialCanvas.SetActive(false);
        accountCanvas.SetActive(true);
    }
}
