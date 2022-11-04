using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    int[] stofStack = {0,0,0,0,0,0};
    public int walet = 2500;
    int inventorySpace = 100;
    string numbersPressed;
    GameHandler gameHandler;
    private bool hasChosen = false;
    [SerializeField] private GameObject WaletText;
    [SerializeField] private GameObject inventorySpaceText;
    private int currentlyBuying = 0;
    private int currentlySelling = 0;
    private void Start()
    {
        gameHandler = GetComponent<GameHandler>();
        displayWallet();
    }
    public void displayInventory(string[] drugnames,GameObject[] inList)
    {
        for(int i = 0; i < inList.Length; i++)
        {
            inList[i].GetComponent<Text>().text = drugnames[i] + ": " + stofStack[i].ToString();
        }
        inventorySpaceText.GetComponent<Text>().text = "InventorySpace: " + inventorySpace;
    }
    void UpdateInventory(int index, int updateStack, int updateWalet)
    {
        stofStack[index] += updateStack;
        walet += updateWalet;
        inventorySpace -= updateStack;
    }
    public void buy(GameObject textfelt)
    {
        if (!hasChosen)
        {
            textfelt.GetComponent<Text>().text = "What do you want to buy?";
            for (int i = 0; i < gameHandler.byer[gameHandler.cityState].drugNames.Length; i++)
            {
                
                if (gameHandler.FirstLetterInString(gameHandler.byer[gameHandler.cityState].drugNames[i]) == char.ToUpper(gameHandler.lastKeyPressed))
                {
                    currentlyBuying = i;
                    hasChosen = true;
                    break;

                }

            }
        }
        else
        {
            typingNumbers();
            textfelt.GetComponent<Text>().text = "How many do you want to buy: " + numbersPressed;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (int.Parse(numbersPressed) <= inventorySpace && gameHandler.byer[gameHandler.cityState].stofPriser[currentlyBuying] * int.Parse(numbersPressed) <= walet)
                {
                    UpdateInventory(currentlyBuying, int.Parse(numbersPressed), (int)-gameHandler.byer[gameHandler.cityState].stofPriser[currentlyBuying] * int.Parse(numbersPressed));
                    numbersPressed = "";
                    gameHandler.gameState = "";
                    hasChosen = false;
                    displayWallet();
                }
            }
        }
    }
    public void sell(GameObject textfelt)
    {
        if (!hasChosen)
        {
            textfelt.GetComponent<Text>().text = "What do you want to sell?";
            for (int i = 0; i < gameHandler.byer[gameHandler.cityState].drugNames.Length; i++)
            {

                if (gameHandler.FirstLetterInString(gameHandler.byer[gameHandler.cityState].drugNames[i]) == char.ToUpper(gameHandler.lastKeyPressed))
                {
                    currentlySelling = i;
                    hasChosen = true;
                    break;

                }

            }
        }
        else
        {
            typingNumbers();
            textfelt.GetComponent<Text>().text = "How many do you want to sell: " + numbersPressed;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (int.Parse(numbersPressed) <= stofStack[currentlySelling])
                {
                    UpdateInventory(currentlySelling, - int.Parse(numbersPressed), (int)gameHandler.byer[gameHandler.cityState].stofPriser[currentlySelling] * int.Parse(numbersPressed));
                    numbersPressed = "";
                    gameHandler.gameState = "";
                    hasChosen = false;
                    displayWallet();
                }
            }

        }
    }
    void typingNumbers()
    {
        if(Input.GetKeyDown("1") || Input.GetKeyDown("2") || Input.GetKeyDown("3") || Input.GetKeyDown("4") || Input.GetKeyDown("5") || Input.GetKeyDown("6") || Input.GetKeyDown("7") || Input.GetKeyDown("8") || Input.GetKeyDown("9") || Input.GetKeyDown("0"))
        {
            numbersPressed += Input.inputString;
        }else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            char[] oldNumbers = numbersPressed.ToCharArray();
            int x = oldNumbers.Length - 1;
            numbersPressed = "";
            for(int i = 0; i < x; i++)
            {
                numbersPressed += oldNumbers[i];
            }
        }
    }
    private void displayWallet()
    {
        WaletText.GetComponent<Text>().text = "Walet: " + walet;
    }

}
