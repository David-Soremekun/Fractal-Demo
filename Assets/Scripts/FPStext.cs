using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPStext : MonoBehaviour
{
    // Start is called before the first frame update
    public int value;
    public Text fps;
    void Start()
    {
        
	    
    }

    // Update is called once per frame
    void Update()
    {
        fps.text = "FPS: " + value.ToString();
    }
}
