using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private int hour_ = 0;
    private int minute_ = 0;
    private int seconds_ = 0;

    private Text textClock;
    private float delta_time;
    private bool stop_clock_ = false;

    public static Clock instance;

    private void Awake()
    {
        if(instance)
            Destroy(instance);

        instance = this;

        textClock = GetComponent<Text>();

        if (GameSettings.Instance.GetContinuePreviousGame())
            delta_time = Config.ReadGameTime();
        else        
            delta_time = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        stop_clock_ = false ;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Game Paused : " + GameSettings.Instance.GetPaused() + " Stop Clock : " + stop_clock_);
        if(GameSettings.Instance.GetPaused()==false && stop_clock_ == false)
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            String hour = LeadingZero(span.Hours);
            String minute = LeadingZero(span.Minutes);
            String seconds = LeadingZero(span.Seconds);

            textClock.text = hour + ":" + minute + ":" + seconds;
        }
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2,'0');
    }

    public void OnGameOver()
    {
        stop_clock_ = true;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    public static string GetCurrentTime()
    {
        return instance.delta_time.ToString();
    }

    public Text GetCurrentTimeText()
    {
        return textClock;
    }

    public void StartClock()
    {
        stop_clock_ = false;
    }
}
