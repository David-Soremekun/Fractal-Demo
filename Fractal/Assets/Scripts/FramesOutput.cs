
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class FramesOutput : MonoBehaviour
{
    private TMP_Text _fpsText;
    private int frameCount = 0;
    private float fps = 0.0f;
    private float timeLeft = 0.5f;
    private float timePassed = 0f;
    private float updateInterval = 0.5f;
    private void Awake()
    {
        _fpsText = GetComponent<TMP_Text>();
        if (!_fpsText)
        {
            Debug.LogError("No Text Componenet attached to Script");
            enabled=false;
            return;
        }
    }
    private void Update()
    {
        frameCount += 1;
        timeLeft -= Time.deltaTime;
        timePassed += Time.timeScale / Time.deltaTime;
        if (timeLeft<=0.0f)
        {
            fps = timePassed / frameCount;
            
            timeLeft = updateInterval;
            timePassed = 0.0f;
            frameCount = 0;
        }
        if (fps < 30) { _fpsText.color = Color.red; }
        else if (fps < 60) { _fpsText.color = Color.yellow; }
        else{ _fpsText.color = Color.white;  }

        _fpsText.text = string.Format("FPS: {0}", (int)fps);

    }
 
    //private IEnumerator FramesPerSecond()
    //{
    //    while (true)
    //    {
    //        int fps = (int) (1.0f / Time.deltaTime);
    //        DisplayFPS(fps);
 
    //        yield return new WaitForSeconds(1.2f);
    //    }
    //}
 
    private void DisplayFPS(float fps)
    {
        _fpsText.text = $"FPS : {fps} ";
    }
    
}
