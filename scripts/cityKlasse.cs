using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cityKlasse 
{
    
    private string name;
    public float[] stofPriser = new float[6];
    public string[] drugNames = {"Snus","Weed","LSD","Meth","Heroin","Cocaine"};
    public cityKlasse(string name)
    {
        float startspris = 200;
        for (int i = 0; i < stofPriser.Length; i++){
            stofPriser[i] = startspris;
            startspris = startspris * 2;
        }
        this.name = name;
    }

    public void UpdateCity()
    {
        for(int i = 0; i < stofPriser.Length; i++)
        {
            stofPriser[i] = RandomizePrice(stofPriser[i]);
        }
    }

    public void DisplayCity(Text cityName, GameObject[] drugsList)
    {
        cityName.text = name;
        for(int i = 0; i < drugsList.Length; i++)
        {
            drugsList[i].GetComponent<Text>().text = drugNames[i] +": " + (int) stofPriser[i];
        }
    }
    private float RandomizePrice(float førpris)
    {
        if(førpris > 1)
        {
            return Random.Range(førpris * 0.5f, førpris * 1.7f);
        }
        else
        {
            return Random.Range(førpris, førpris * 4f);
        }
        
    }

}
