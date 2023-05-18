using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    public int selectedCharacters;
    public string playername;
    private bool startcondition = false;
    public GameObject popup;    
    public TMP_InputField.SubmitEvent onEndEdit { get; set; }
    [SerializeField]
    Button startButton;

    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
    
    }
    public void Female() {
            selectedCharacters = 1;
        PlayerPrefs.SetInt("selectedCharacters", selectedCharacters);

    }
    public void Male()
    {
       selectedCharacters = 0;
        PlayerPrefs.SetInt("selectedCharacters", selectedCharacters);

    }
    private void Update()
    {

        if (inputField != null)
        {
            var text = inputField.text;
            if (string.IsNullOrEmpty(text) || text.Length <= 3 || text.Contains("+") ||
                text.Contains("=") || text.Contains("-") || text.Contains("~") || text.Contains(";") ||
                text.Contains(":") || text.Contains("'") || text.Contains("(") || text.Contains(")") || text.Contains("}") || text.Contains("{") || text.Contains("[")
                || text.Contains("]") || text.Contains("#") || text.Contains("$") || text.Contains("%") || text.Contains("&") || text.Contains("*") || text.Contains(",")
                || text.Contains(".") || text.Contains(">") || text.Contains("<") || text.Contains("?")|| text.Contains("-")|| text.Contains("^")|| text.Contains("-"))
            {
                startButton.interactable = false;
                
            }
            if (text.Length <= 3 || text.Contains("+") ||
                text.Contains("=") || text.Contains("-") || text.Contains("~") || text.Contains(";") ||
                text.Contains(":") || text.Contains("'") || text.Contains("(") || text.Contains(")") || text.Contains("}") || text.Contains("{") || text.Contains("[")
                || text.Contains("]") || text.Contains("#") || text.Contains("$") || text.Contains("%") || text.Contains("&") || text.Contains("*") || text.Contains(",")
                || text.Contains(".") || text.Contains(">") || text.Contains("<") || text.Contains("?") || text.Contains("-") || text.Contains("^") || text.Contains("-") || text.Contains("!"))
            {
                popup.SetActive(true);
            }
            else
            {
                startButton.interactable = true;
                startcondition = true;
            }
            if (popup != null)
            {
                if(text.Length == inputField.characterLimit)  
                {
                    popup.SetActive(true);
                
                }

              
            }

        }

        //PlayerPrefs.SetString("PlayerName", playername);
        
         
    }
     

    public void StartGame()
    {
        if (startcondition)
        {
            startButton.interactable = true;
            PlayerPrefs.SetString("PlayerName", inputField.text);

            PlayerPrefs.SetInt("selectedCharacters", selectedCharacters);
        }

    }

}
