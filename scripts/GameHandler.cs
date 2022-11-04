using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GameObject CurrentCityNameField;
    [SerializeField] private GameObject jetCityCanvas;
    [SerializeField] private GameObject inputText;
    [SerializeField] private GameObject gameOverImage;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject gameOverHighScore;
    private GameObject[] druglist = new GameObject[6];
    public cityKlasse[] byer = new cityKlasse[6];
    private GameObject[] inventoryList = new GameObject[6];
    private string[] citynames = { "Randers", "Viborg", "Roskilde", "Aalborg", "Odense", "København" };

    public char lastKeyPressed;

    
    public int cityState = 0;
    public string gameState = "";
    private int daysGone = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(citynames.Length >= byer.Length)
        {
            for (int i = 0; i < byer.Length; i++)
            {
                byer[i] = new cityKlasse(citynames[i]);
                GameObject.Find("city" + i).GetComponent<Text>().text = citynames[i];
            }
            jetCityCanvas.SetActive(false);
        }
        else
        {
            Debug.Log("U FUCKED UP!!!");
        }

        for (int i = 0; i < druglist.Length; i++)
        {
            druglist[i] = GameObject.Find("drugPrice" + i);
        }
        for(int i = 0; i< inventoryList.Length; i++)
        {
            inventoryList[i] = GameObject.Find("Inventorydrug" + i);
        }
        byer[cityState].DisplayCity(CurrentCityNameField.GetComponent<Text>(),druglist);
        GetComponent<Inventory>().displayInventory(byer[cityState].drugNames, inventoryList);
    }
    private void Update()
    {
        UpdateLastKey();
        switch (gameState)
        {
            case "buying":
                GetComponent<Inventory>().buy(inputText);
                GetComponent<Inventory>().displayInventory(byer[cityState].drugNames, inventoryList);
                break;
            case "selling":
                GetComponent<Inventory>().sell(inputText);
                GetComponent<Inventory>().displayInventory(byer[cityState].drugNames, inventoryList);
                break;
            case "jetting":
                jetCity(lastKeyPressed);
                break;
            case "GameOver":
                gameOverImage.SetActive(true);
                gameOverText.SetActive(true);
                gameOverHighScore.SetActive(true);
                gameOverHighScore.GetComponent<Text>().text = "Your end wallet was : " + GetComponent<Inventory>().walet;
                break;
            default:
                if(daysGone == 30)
                {
                    gameState = "GameOver";
                }
                if(lastKeyPressed == 'j')
                {
                    gameState = "jetting";
                    jetCityCanvas.SetActive(true);
                }else if(lastKeyPressed == 'b')
                {
                    gameState = "buying";
                }else if(lastKeyPressed == 's')
                {
                    gameState = "selling";
                }
                inputText.GetComponent<Text>().text = "Do you want to buy, sell or jet?";
                break;
        }
        

    } 
    private void UpdateLastKey()
    {
        if (Input.anyKeyDown)
        {
            string keypressed = Input.inputString;
            char[] charsInString;
            charsInString = keypressed.ToCharArray();
            lastKeyPressed = charsInString[charsInString.Length - 1];
        }
    }
    public char FirstLetterInString(string a)
    {
        char[] CharsInString = a.ToCharArray();
        return CharsInString[0];
    }
    private void jetCity(char key)
    {
        inputText.GetComponent<Text>().text = "Where do you want to jet?";
        char upperKey = char.ToUpper(key);
        for(int i = 0; i <byer.Length; i++)
        {
            if (upperKey == FirstLetterInString(citynames[i]) && upperKey != FirstLetterInString(citynames[cityState])){
                cityState = i;
                byer[cityState].UpdateCity();
                byer[cityState].DisplayCity(CurrentCityNameField.GetComponent<Text>(), druglist);
                jetCityCanvas.SetActive(false);
                gameState = "";
                daysGone++;
            }
        }
    }

}
