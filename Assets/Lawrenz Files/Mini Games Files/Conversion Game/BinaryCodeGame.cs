using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BinaryCodeGame : MonoBehaviour
{
    public TextMeshProUGUI binaryDisplay;
    public TMP_InputField decimalInput;
    public Button submitButton;

    private string binaryCode;
    private int decimalValue;

    private void Start()
    {
        // Generate a random binary code
        binaryCode = GenerateBinaryCode();
        binaryDisplay.text = binaryCode;

        // Attach the submit method to the button's onClick event
        submitButton.onClick.AddListener(SubmitAnswer);
    }

    private string GenerateBinaryCode()
    {
        string binaryCode = "";

        // Generate a random binary code of length 8
        for (int i = 0; i < 8; i++)
        {
            int randomBit = Random.Range(0, 2);
            binaryCode += randomBit.ToString();
        }

        return binaryCode;
    }

    private void SubmitAnswer()
    {
        // Parse the input decimal value
        if (int.TryParse(decimalInput.text, out decimalValue))
        {
            // Convert the decimal value to binary
            string convertedBinary = System.Convert.ToString(decimalValue, 2).PadLeft(8, '0');

            // Compare the converted binary with the generated binary code
            if (convertedBinary == binaryCode)
            {
                Debug.Log("Correct! The binary code was decoded successfully.");
                // Add your logic for successful decoding here (e.g., unlock the vault)
            }
            else
            {
                Debug.Log("Incorrect! Try again.");
            }
        }
        else
        {
            Debug.Log("Invalid input! Please enter a valid decimal value.");
        }
    }
}
