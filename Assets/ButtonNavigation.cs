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

    public Button TreatBone;
    public Button Collar;
    public Button Depressed;

    public Button treats;
    public Button accessories;

    public GameObject bone;
    public GameObject collar;

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

        TreatBone.onClick.AddListener(HappyButtonClicked);
        Collar.onClick.AddListener(SadButtonClicked);
        Depressed.onClick.AddListener(DepressedButtonClicked);

        treats.onClick.AddListener(HideAccessories);
        accessories.onClick.AddListener(HideTreats);

       /* bone.onClick.AddListener(GoHome);
        collar.onClick.AddListener(GoHome1);*/

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

        OpenHomeCanvas();
    }

    void SadButtonClicked()
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
        currentHealth = Mathf.Max(currentHealth - 50, 0);
        happinessBar.SetHealth(currentHealth);

        OpenHomeCanvas();
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

    void HideAccessories()
    {
        bone.SetActive(true);
        collar.SetActive(false);
    }

    void HideTreats()
    {
        collar.SetActive(true);
        bone.SetActive(false);
    }

/*    void GoHome()
    {
        storeCanvas.SetActive(false);
    }

    void GoHome1()
    {
        storeCanvas.SetActive(false);
    }*/
}
