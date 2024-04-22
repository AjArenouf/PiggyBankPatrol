using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TreatAffect : MonoBehaviour
{
    public TextMeshProUGUI inGameCurrency;
    public Slider happinessBar;
    public Button treat1;
    int score;
    public int maxHealth = 100;
    public int currentHealth;
    public int treat1Cost = 200;

    // Start is called before the first frame update
    void Start()
    {
        treat1.onClick.AddListener(treat1ButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void treat1ButtonClicked()
    {
        if (HasEnoughCurrency(treat1Cost))
        {
            DecreaseCurrency(treat1Cost);

            currentHealth = maxHealth;

            happinessBar.value = (float)currentHealth / maxHealth;
        }
    }

    bool HasEnoughCurrency(int amount)
    {
        // Check if the in-game currency is greater than or equal to the specified amount
        return score >= amount;
    }

    void DecreaseCurrency(int amount)
    {
        // Subtract the specified amount from the in-game currency
        score -= amount;
        // Update the UI text to display the new currency amount
        UpdateCurrencyUI();
    }

    void UpdateCurrencyUI()
    {
        // Update the UI text to display the current in-game currency amount
        inGameCurrency.text = "Currency: " + score.ToString();
    }
}
