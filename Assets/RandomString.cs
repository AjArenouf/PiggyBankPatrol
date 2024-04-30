using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomString : MonoBehaviour
{
    public Image imageComponent;
    public TextMeshProUGUI textComponent;
    public float displayDuration = 5f;
    public float vanishDuration = 5f;
    public Button treatButton;
    public Button clothesButton;

    public Coroutine displayCoroutine;
    private bool showingButtonString = false;

    // Rebekah Variables
    private int dialogArrayIndex = 0;
    private bool showingDialog = false;

    /* Index list
     * 0 = regular
     * 1 = clothes
     * 2 = button
     */
    private string[][] dialogStrings = {
       new string [] { "More money saved means more treats for me! You're the best!",
        "Purrfect! Saving money means a bigger scratching post!",
        "You're my favorite human, especially when you reach your savings goals!",
        "Saving like that deserves a big belly rub!",
        "You're the best at resisting those chew toys... I mean, resisting spending!",
        "Thanks for being so smart! Maybe I'll get an extra treat today!" },
       new string [] { "My wardrobe thanks you for being so smart with money.",
        "Maybe those fancy sunglasses I've been eyeing are finally in my future!" },
       new string[] { "You're the reason my treat jar is always full!",
        "Every penny saved means I get closer to that yummy chew toy!" }
    };
    /*
    // Strings to display when the button is pressed
    private string[] buttonStrings = {
        "You're the reason my treat jar is always full!",
        "Every penny saved means I get closer to that yummy chew toy!"
    };

    private string[] clothesButtonStrings =
    {
        "My wardrobe thanks you for being so smart with money.",
        "Maybe those fancy sunglasses I've been eyeing are finally in my future!"
    };

    private string[] generalStrings =
    {
        "More money saved means more treats for me! You're the best!",
        "Purrfect! Saving money means a bigger scratching post!",
        "You're my favorite human, especially when you reach your savings goals!",
        "Saving like that deserves a big belly rub!",
        "You're the best at resisting those chew toys... I mean, resisting spending!",
        "Thanks for being so smart! Maybe I'll get an extra treat today!"
    };
    */

    void OnEnable()
    {
        Debug.Log("I've been enabled!");
        displayCoroutine = StartCoroutine(DisplayStrings());
    }

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
        if (treatButton != null)
        {
            treatButton.onClick.AddListener(OnTreatButtonClicked);
        }
        else
        {
            Debug.LogError("Treat button reference is not assigned in the RandomString script.");
        }

        // Listen for the clothes button click event
        if (clothesButton != null)
        {
            clothesButton.onClick.AddListener(OnClothesButtonClicked);
        }
        else
        {
            Debug.LogError("Clothes button reference is not assigned in the RandomString script.");
        }
    }

    void OnTreatButtonClicked()
    {
        dialogArrayIndex = 2;
        Debug.Log("Treat button clicked!");
        // Display one of the specified strings when the treat button is clicked
        /*
        int randomIndex = Random.Range(0, buttonStrings.Length);
        DisplayButtonString(buttonStrings[randomIndex]);
        */
    }

    void OnClothesButtonClicked()
    {
        dialogArrayIndex = 1;
        Debug.Log("Clothes button clicked!");
        // Display a random clothes string when the clothes button is clicked
        /*
        int randomIndex = Random.Range(0, clothesButtonStrings.Length);
        DisplayButtonString(clothesButtonStrings[randomIndex]);
        */
    }

   IEnumerator DisplayDialog()
    {
        int randomIndex = Random.Range(0, dialogStrings[dialogArrayIndex].Length);
        // Get a random string from the general strings list
        string randomString = dialogStrings[dialogArrayIndex][randomIndex];

        // Display the random string
        textComponent.text = randomString;

        // Show the text and image
        textComponent.enabled = true;
        imageComponent.enabled = true;
        showingDialog = true;
        Debug.Log("Displaying dialog!");
        yield return HideDialogAfterDelay();
    }

    // Coroutine to hide the button string after a delay
    IEnumerator HideDialogAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        textComponent.enabled = false;
        imageComponent.enabled = false;
        showingDialog = false;
        if (dialogArrayIndex != 0)
        {
            dialogArrayIndex = 0;
        }
        Debug.Log("Dialog hidden!");
    }

    
    IEnumerator DisplayStrings()
    {
        while (true)
        {
            if (!showingDialog)
            {
                yield return DisplayDialog();
                Debug.Log("----------in-between dialogs----------");
                yield return new WaitForSeconds(vanishDuration);
            }

            /*
            if (!showingButtonString)
            {
                // Get a random string from the general strings list
                string randomString = generalStrings[Random.Range(0, generalStrings.Length)];

                // Display the random string
                textComponent.text = randomString;

                // Show the text and image
                textComponent.enabled = true;
                imageComponent.enabled = true;

                yield return new WaitForSeconds(displayDuration);

                // Hide the text and image
                textComponent.enabled = false;
                imageComponent.enabled = false;

                yield return new WaitForSeconds(vanishDuration);
            }
*/
            yield return null;
            
        }
    }

    /*
    // Method to be called when the button is pressed
    public void DisplayButtonString(string str)
    {
        // Display the specified string
        textComponent.text = str;

        // Show the text
        textComponent.enabled = true;
        imageComponent.enabled = true;
        showingButtonString = true;

        StartCoroutine(HideButtonStringAfterDelay(vanishDuration));
    }

    // Coroutine to hide the button string after a delay
    IEnumerator HideButtonStringAfterDelay(float delay)
    {

        yield return new WaitForSeconds(delay);

        textComponent.enabled = false;
        imageComponent.enabled = false;
        showingButtonString = false;

        yield return new WaitForSeconds(vanishDuration);

    }
    */
}
