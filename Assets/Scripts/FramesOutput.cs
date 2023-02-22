
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class FramesOutput : MonoBehaviour
{
    public int m_frameCounter;
    private float m_timeCounter;
    public float m_lastFramerate;
    public float m_refreshTime;


    void Start()
    {
        m_frameCounter = 0;
        m_timeCounter = 0.0f;
        m_lastFramerate = 0.0f;
        m_refreshTime = 0.5f;
}

    void Update()
    {
        if( m_timeCounter < m_refreshTime )
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            
            m_lastFramerate = (float)m_frameCounter/m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
    }
}
