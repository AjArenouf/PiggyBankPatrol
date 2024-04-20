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

    public Button Happy;
    public Button Sad;
    public Button Depressed;

    public int maxHealth = 100;
    public int currentHealth;

    public HappinessBar happinessBar;

    // Start is called before the first frame update
    void Start()
    {
        home.onClick.AddListener(OpenHomeCanvas);
        store.onClick.AddListener(OpenStoreCanvas);
        social.onClick.AddListener(OpenSocialCanvas);
        account.onClick.AddListener(OpenAccountCanvas);

        Happy.onClick.AddListener(HappyButtonClicked);
        Sad.onClick.AddListener(SadButtonClicked);
        Depressed.onClick.AddListener(DepressedButtonClicked);

        currentHealth = maxHealth;
        happinessBar.SetMaxHealth(maxHealth);
        happinessBar.SetHealth(currentHealth);
    }

    void HappyButtonClicked()
    {
        currentHealth = maxHealth;
        happinessBar.SetHealth(currentHealth);
    }

    void SadButtonClicked()
    {
        currentHealth = Mathf.Max(currentHealth - 50, 0);
        happinessBar.SetHealth(currentHealth);
    }

    void DepressedButtonClicked()
    {
        currentHealth = Mathf.Max(currentHealth - 75, 0);
        happinessBar.SetHealth(currentHealth);
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

  /*  void TakeDamage(int damage)
    {
        currentHealth -= damage;
        happinessBar.SetHealth(currentHealth);
    }*/
}
