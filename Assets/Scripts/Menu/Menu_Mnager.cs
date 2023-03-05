using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu_Mnager : MonoBehaviour
{
    [SerializeField]
    GameObject headlerOBJ, gameNameOBJ, startBTTN;
    [SerializeField]
    GameObject numbers;
    [SerializeField]
    GameObject GameObjects;


    int ofTheNumber;
    private void Start()
    {
        StartCoroutine(GetscreenSceneElementsRoutine());
    }
    IEnumerator GetscreenSceneElementsRoutine()
    {
        headlerOBJ.GetComponent<CanvasGroup>().DOFade(1, 1f);
        headlerOBJ.GetComponent<RectTransform>().DOLocalMoveX(0,1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.2f);

        gameNameOBJ.GetComponent<CanvasGroup>().DOFade(1, 1f);
        gameNameOBJ.GetComponent<RectTransform>().DOLocalMoveX(0, 1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.2f);

        startBTTN.GetComponent<CanvasGroup>().DOFade(1, 1f);
        startBTTN.GetComponent<RectTransform>().DOLocalMoveY(-600, 1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.2f);
    }
    public void StartTheGame()
    {
        headlerOBJ.GetComponent<CanvasGroup>().DOFade(0, 1f);
        headlerOBJ.GetComponent<RectTransform>().DOLocalMoveX(1600, 1f);
      

        gameNameOBJ.GetComponent<CanvasGroup>().DOFade(0, 1f);
        gameNameOBJ.GetComponent<RectTransform>().DOLocalMoveX(-806, 1f);
        

        startBTTN.GetComponent<CanvasGroup>().DOFade(1, 1f);
        startBTTN.GetComponent<RectTransform>().DOLocalMoveY(-880, 1f);

        StartCoroutine(countdownRoutine());

        



    }
    IEnumerator countdownRoutine()
    {
        numbers.transform.GetChild(ofTheNumber).gameObject.SetActive(true);
        numbers.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        numbers.GetComponent<RectTransform>().DOScale(1, 0.5f);
        yield return new WaitForSeconds(.7f);
        numbers.GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        numbers.GetComponent<RectTransform>().DOScale(0, 0.5f);
        yield return new WaitForSeconds(.3f);
        numbers.transform.GetChild(ofTheNumber).gameObject.SetActive(false);
        ofTheNumber++;
        if (ofTheNumber < numbers.transform.childCount)
        {
            StartCoroutine(countdownRoutine());
        }
        GameObjects.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObjects.GetComponent<CanvasGroup>().DOFade(3, 1f);
    }
}
