using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    static DayManager _instance;
    public static DayManager GetInstance() { return _instance; }

    public float DayTimeSeconds = 10 * 60.0f;
    int _currDay = 0;
    float _currTime = 0.0f;

    private void Awake()
    {
        _instance = this;
    }

    void StartNewDay()
    {
        Debug.Log("New day");
        ++_currDay;
        _currTime = DayTimeSeconds;

        FarmManager.GetInstance().OnNextDay();
    }

    private void Update()
    {
        _currTime -= Time.deltaTime;
        if ( _currTime <= 0 )
        {
            StartNewDay();
        }
    }
}