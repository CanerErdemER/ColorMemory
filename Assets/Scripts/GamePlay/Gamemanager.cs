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

    [SerializeField]
    TMP_Text totalPointTxt,timeTxt;
    [SerializeField]
    GameObject Bonus_ýmg;

    int randomTrueMeaning;
    int randomWrongMeaning;

    Color32 upperColor;
    Color32 trueColor;

    bool resultisTrue;

    int theNumberOfTrue;
    int thenumberOfBonus;
    bool bonus;
    int point = 10;
    int totalPoint;
    int remainingTime;
    

    void Start()
    {

        generateRandomColor();
        StartCoroutine(countDownRoutine());
        remainingTime = 60;
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
            
            theNumberOfTrue++;
            PointUp();
            
        }
        else
        {
            ReducePoint();
        }
        StartCoroutine(generateNewColorRoutine());
    }
    public void falsebutton()
    {
        if (resultisTrue)
        {
            ReducePoint();
            
        }
        else
        {
            
            theNumberOfTrue++;
            PointUp();
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
    void PointUp()
    {
        thenumberOfBonus++;
        if (thenumberOfBonus == 5)
        {
            bonus = true;
        }
        if (bonus)
        {
            if (thenumberOfBonus >= 5 && thenumberOfBonus <= 10)
            {
                Bonus_ýmg.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
                Bonus_ýmg.GetComponent<RectTransform>().DOScale(1, 0.4f).SetEase(Ease.OutBounce);

                point = thenumberOfBonus * 10;
            }
        }
        if (thenumberOfBonus == 10)
        {
            thenumberOfBonus = 0;
            point = 10;
            bonus = false;

            Bonus_ýmg.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
            Bonus_ýmg.GetComponent<RectTransform>().DOScale(0, 0.2f).SetEase(Ease.InBounce);
        }
        totalPoint += point;
        totalPointTxt.text = totalPoint.ToString();
    }
    void ReducePoint()
    {
        thenumberOfBonus = 0;
        point = 10;
        bonus = false;

        Bonus_ýmg.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
        Bonus_ýmg.GetComponent<RectTransform>().DOScale(0, 0.2f).SetEase(Ease.InBounce);
        totalPoint -= 10;

        if (totalPoint <= 0)
        {
            totalPoint = 0;
        }
        totalPointTxt.text = totalPoint.ToString();

    }
    IEnumerator countDownRoutine()
    {
        yield return new WaitForSeconds(1f);
        remainingTime--;
        if (remainingTime < 10)
        {
            timeTxt.text = "0" + remainingTime.ToString();
        }
        else
        {
            timeTxt.text = remainingTime.ToString();
        }
        

        StartCoroutine(countDownRoutine());
        if (remainingTime <= 0)
        {
            StopAllCoroutines();
            //time is up,finish the game
        }
    }




}
