using System.Diagnostics;
using UnityEngine;

public class TestBeat : MonoBehaviour
{

    Stopwatch m_stopwatch;
    int m_count;
    [SerializeField] private int m_bpm = 100;
    private float m_adjustedTimestep;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_stopwatch = new Stopwatch();
        m_stopwatch.Start();  
        m_count = 0;
        m_adjustedTimestep = (float)m_bpm/60f;
        UnityEngine.Debug.Log(m_adjustedTimestep);
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = (float)m_stopwatch.ElapsedMilliseconds / 1000f * m_adjustedTimestep;
        if ((int)elapsedTime > m_count)
        {
            m_count++;
            UnityEngine.Debug.Log(m_count.ToString());
        }
    }
}
