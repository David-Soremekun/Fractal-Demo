
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class FramesOutput : MonoBehaviour
{
    private TMP_Text _fpsText;
 
    private void Start()
    {
        _fpsText = GetComponent<TMP_Text>();
        StartCoroutine(FramesPerSecond());
    }
 
    private IEnumerator FramesPerSecond()
    {
        while (true)
        {
            int fps = (int) (1.0f / Time.deltaTime);
            DisplayFPS(fps);
 
            yield return new WaitForSeconds(1.2f);
        }
    }
 
    private void DisplayFPS(float fps)
    {
        _fpsText.text = $"FPS : {fps} ";
    }
    
}
