using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    GameObject pausePanel,resultPanel,upperPanel,midPanel,LowerPanel;
    [SerializeField]
    TMP_Text tTXT, fTXT,trueResult,falseResult,resultTotalPoint;

    VoiceManager VM;

    int randomTrueMeaning;
    int randomWrongMeaning;

    Color32 upperColor;
    Color32 trueColor;

    bool resultisTrue;

    int theNumberOfTrue;
    int theNumberOfFalse;
    int thenumberOfBonus;
    bool bonus;
    int point = 10;
    int totalPoint;
    int remainingTime;

    public bool pausePressed;

    private void Awake()
    {
        VM = Object.FindObjectOfType<VoiceManager>();
    }
    void Start()
    {
        pausePressed = true;
        generateRandomColor();
       
    }
    public void startGame()
    {
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
        if (pausePressed)
            return;
        if (resultisTrue)
        {
            
            theNumberOfTrue++;
            VM.trueAnswer();
            PointUp();
            
        }
        else
        {
            ReducePoint();
            VM.falseAnswer();
            theNumberOfFalse++;
        }
        StartCoroutine(generateNewColorRoutine());
    }
    public void falsebutton()
    {
        if(pausePressed)
            return;
        if (resultisTrue)
        {
            ReducePoint();
            VM.falseAnswer();
            theNumberOfFalse++;
            
        }
        else
        {
            
            theNumberOfTrue++;
            VM.trueAnswer();
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
                VM.bonusPoint();
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
    public IEnumerator countDownRoutine()
    {
        yield return new WaitForSeconds(1f);
        
        remainingTime--;
        if (remainingTime < 10)
        {
            timeTxt.text = "0" + remainingTime.ToString();
            //rutin oluþturuklup 10 dan geri sayým yapýlacak
        }
        else
        {
            timeTxt.text = remainingTime.ToString();
        }
        

        StartCoroutine(countDownRoutine());
        if (remainingTime <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(finishTheGameRoutine());
        }
    }
    public void PauseTheGame()
    {
        if (!pausePressed)
        {
            pausePressed = true;
            StopAllCoroutines();
            pausePanel.GetComponent<CanvasGroup>().DOFade(1, .5f);
            pausePanel.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);
            tTXT.text = theNumberOfTrue.ToString();
            fTXT.text = theNumberOfFalse.ToString();


        }
       
        
    }
    public void ReturnTheGame()
    {
        if (pausePanel)
        {
            StartCoroutine(countDownRoutine());
            pausePressed = false;

            pausePanel.GetComponent<CanvasGroup>().DOFade(0, .5f);
            pausePanel.GetComponent<RectTransform>().DOScale(0, .5f).SetEase(Ease.InBack);
        }
    }
    void writeTheResults()
    {
        trueResult.text = theNumberOfTrue.ToString();
        falseResult.text = theNumberOfFalse.ToString();
        resultTotalPoint.text = totalPoint.ToString();
    }
    IEnumerator finishTheGameRoutine()
    {
        upperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        midPanel.SetActive(false);
        Bonus_ýmg.SetActive(false);
        writeTheResults();

        VM.finishTheGame();
        
        yield return new WaitForSeconds(.2f);

        pausePressed = true;
        resultPanel.GetComponent<CanvasGroup>().DOFade(1, .5f);
        resultPanel.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void quitgame()
    
    {
        Application.Quit();
    }


}
