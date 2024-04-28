using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomString : MonoBehaviour
{
    public SelfAffirmStrings imageWithStrings;
    public TextMeshProUGUI textComponent;
    public float displayDuration = 3f;
    public Button treatbutton; // Reference to the treat button in the Unity Editor

    private Coroutine displayCoroutine;
    private bool showingButtonString = false;

    // Strings to display when the button is pressed
    private string[] buttonStrings = {
        "You're the reason my treat jar is always full!",
        "Every penny saved means I get closer to that yummy chew toy!"
    };

    // Start is called before the first frame update
    void Start()
    {
        // Ensure text component is assigned
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        // Start displaying strings
        displayCoroutine = StartCoroutine(DisplayStrings());

        // Listen for the treat button click event
        if (treatbutton != null)
        {
            treatbutton.onClick.AddListener(OnTreatButtonClicked);
        }
        else
        {
            Debug.LogError("Treat button reference is not assigned in the RandomString script.");
        }
    }

    void OnTreatButtonClicked()
    {
        // Display one of the specified strings when the treat button is clicked
        int randomIndex = Random.Range(0, buttonStrings.Length);
        DisplayButtonString(randomIndex);
    }

    IEnumerator DisplayStrings()
    {
        while (true)
        {
            if (!showingButtonString)
            {
                // Get a random string from the list
                string randomString = imageWithStrings.stringList[Random.Range(0, imageWithStrings.stringList.Count)];

                // Display the random string
                textComponent.text = randomString;

                // Show the text and image
                textComponent.enabled = true;
                imageWithStrings.imageComponent.enabled = true;

                yield return new WaitForSeconds(displayDuration);

                // Hide the text and image
                textComponent.enabled = false;
                imageWithStrings.imageComponent.enabled = false;
            }

            yield return null;
        }
    }

    // Method to be called when the button is pressed
    public void DisplayButtonString(int index)
    {
        if (index >= 0 && index < buttonStrings.Length)
        {
            // Display the specified string when the button is pressed
            textComponent.text = buttonStrings[index];

            // Show the text
            textComponent.enabled = true;
            showingButtonString = true;

            // Hide the text and image after a delay
            StartCoroutine(HideButtonStringAfterDelay(displayDuration));
        }
    }

    // Coroutine to hide the button string after a delay
    IEnumerator HideButtonStringAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        textComponent.enabled = false;
        showingButtonString = false;
    }
}
