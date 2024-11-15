using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer2 : MonoBehaviour
{
    public float playerTime = 50f;
    public bool timeSoundsStarted = false;
    public Image timer;
    public bool paused = false;
    public SnakeLadder snakeLadder;
    private void Start()
    {
        playerTime = 50f;
        timer = transform.GetComponent<Image>();
    }
    private void OnEnable()
    {
        timer.fillAmount = 1;
    }
    private void Update()
    {
        if (timer.fillAmount == 1) paused = false;

        if (!paused)
            updateClock();
    }
    private void updateClock()
    {
        //Debug.Log("update clock");
        float minus;

        minus = 2.0f / playerTime * Time.deltaTime;

        timer.fillAmount -= minus;

        if (timer.fillAmount < 0.25f && !timeSoundsStarted)
            timeSoundsStarted = true;


        if (timer.fillAmount == 0)
        {
            paused = true;
            snakeLadder.ExaustedTime();
        }
    }
}


