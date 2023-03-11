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
    VoiceManager VM;
    Gamemanager GM;
    private void Awake()
    {
        VM = Object.FindObjectOfType<VoiceManager>();
        GM = Object.FindObjectOfType<Gamemanager>();
        
    }


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
        GameObjects.SetActive(false);
        headlerOBJ.GetComponent<CanvasGroup>().DOFade(0, 1f);
        headlerOBJ.GetComponent<RectTransform>().DOLocalMoveX(1600, 1f);
      

        gameNameOBJ.GetComponent<CanvasGroup>().DOFade(0, 1f);
        gameNameOBJ.GetComponent<RectTransform>().DOLocalMoveX(-806, 1f);
        

        startBTTN.GetComponent<CanvasGroup>().DOFade(1, 1f);
        startBTTN.GetComponent<RectTransform>().DOLocalMoveY(-880, 1f);

        StartCoroutine(countdownRoutine());
        GameObjects.SetActive(true);
        
        





    }
    IEnumerator countdownRoutine()
    {
        
        numbers.transform.GetChild(ofTheNumber).gameObject.SetActive(true);
        numbers.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        numbers.GetComponent<RectTransform>().DOScale(1, 0.5f);
        VM.countDown();
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
        if (ofTheNumber == 3)//oyunun ba�lad��� geri say�m�n bitti�i yer
        {
            VM.startTheGame();
            GM.pausePressed = false;
            GM.startGame();
           
            
        }
        
        


        yield return new WaitForSeconds(2f);
        GameObjects.GetComponent<CanvasGroup>().DOFade(1, 1f);
        

    }
}
