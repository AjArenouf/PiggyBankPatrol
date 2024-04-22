using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Button treat1;

    public Button Happy;
    public Button Sad;
    public Button Depressed;

    public int maxHealth = 100;
    public int currentHealth;

    public TextMeshProUGUI inGameCurrency;

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

        currentHealth = 50;
        happinessBar.SetMaxHealth(maxHealth);
        happinessBar.SetHealth(currentHealth);

    }

    void HappyButtonClicked()
    {
        // Convert the current currency value to an integer
        int currentCurrencyValue = int.Parse(inGameCurrency.text.Replace(",", "")); // Remove commas if present

        // Subtract a certain amount from the currency value
        int newCurrencyValue = currentCurrencyValue - 85; // Subtract 85 (you can adjust this value)

        // Ensure the currency value doesn't go below 0
        newCurrencyValue = Mathf.Max(newCurrencyValue, 0);

        // Convert the new currency value back to string format
        string newCurrencyString = newCurrencyValue.ToString("N0"); // Format with commas

        // Update the in-game currency text component
        inGameCurrency.text = newCurrencyString;

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
