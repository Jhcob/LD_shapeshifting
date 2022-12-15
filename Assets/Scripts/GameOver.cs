using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Animator canvasGameOver;
    
    private void OnTriggerEnter(Collider other)
    {
        canvasGameOver.SetTrigger("canvasTrigger");
        Debug.Log("gameOver");    }
}
