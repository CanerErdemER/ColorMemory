using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [SerializeField]
    AudioSource start_fx,true_fx, false_fx, bonus_fx, finish_fx,countDown_fx;

    public void startTheGame()
    {
        start_fx.Play();
    }
    public void trueAnswer()
    {
        true_fx.Play();
    }
    public void falseAnswer()
    {
        false_fx.Play();
    }
    public void bonusPoint()
    {
        bonus_fx.Play();
    }
    public void countDown()
    {
        countDown_fx.Play();
    }
    public void finishTheGame()
    {
        finish_fx.Play();
    }
}
