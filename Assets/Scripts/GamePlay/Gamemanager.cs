using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Gamemanager : MonoBehaviour
{
    [SerializeField]
    ColorItem[] colorItem;

    [SerializeField]
    TMP_Text meaningTxt, colorTxt;

    int randomTrueMeaning;
    int randomWrongMeaning;

    Color32 upperColor;
    Color32 trueColor;

    bool resultisTrue;

    int theNumberOfTrue;
    

    void Start()
    {

        generateRandomColor();
    }

    void generateRandomColor()
    {
        int randomNumber = Random.Range(0, 100);

        if (randomNumber < 50)//doðru ise 
        {
            randomTrueMeaning = Random.Range(0, colorItem.Length);
            meaningTxt.text = colorItem[randomTrueMeaning].colorName;
            upperColor = colorItem[Random.Range(0, colorItem.Length)].color;
            meaningTxt.color = new Color32(upperColor.r, upperColor.g, upperColor.b,255);


            trueColor = colorItem[randomTrueMeaning].color;
            colorTxt.color = new Color32(trueColor.r, trueColor.g, trueColor.b, 255);
            colorTxt.text = colorItem[Random.Range(0, colorItem.Length)].colorName;
            resultisTrue = true;
        }
        else//yanlýþ ise
        {
            randomWrongMeaning = Random.Range(0, colorItem.Length);
            if (randomWrongMeaning != randomTrueMeaning)
            {
                meaningTxt.text = colorItem[randomWrongMeaning].colorName;
                upperColor = colorItem[Random.Range(0, colorItem.Length)].color;
                meaningTxt.color = new Color32(upperColor.r, upperColor.g, upperColor.b, 255);
                trueColor = colorItem[randomTrueMeaning].color;

                colorTxt.color = new Color32(trueColor.r, trueColor.g, trueColor.b, 255);
                colorTxt.text = colorItem[Random.Range(0, colorItem.Length)].colorName;
                resultisTrue = false;

            }
            else
            {
                generateRandomColor();
            }

        }
        meaningTxt.GetComponent<CanvasGroup>().DOFade(1, 0.02f);
        colorTxt.GetComponent<CanvasGroup>().DOFade(1, 0.02f);
    }

    public void truebutton()
    {
        if (resultisTrue)
        {
            print("sonuç doðru");
            theNumberOfTrue++;
            
        }
        else
        {
            print("sonuc yanlýþ");
        }
        StartCoroutine(generateNewColorRoutine());
    }
    public void falsebutton()
    {
        if (resultisTrue)
        {
            print("sonuç yanlýþ");
            
        }
        else
        {
            print("sonuc doðru");
            theNumberOfTrue++;
        }
        StartCoroutine(generateNewColorRoutine());
    }
    IEnumerator generateNewColorRoutine()
    {
        meaningTxt.GetComponent<CanvasGroup>().DOFade(0, 0.02f);
        colorTxt.GetComponent<CanvasGroup>().DOFade(0, 0.02f);

        yield return new WaitForSeconds(0.2f);
        generateRandomColor();


    }




}
