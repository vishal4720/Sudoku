using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{

    public Text time_text;
    public Text level_text;

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2,'0');
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Config.GameDataFileExists() == false)
        {
            gameObject.GetComponent<Button>().interactable = false;
            time_text.text = " ";
            level_text.text = " ";
        }
        else
        {
            float delta_time = Config.ReadGameTime();
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            time_text.text = hour + ":" + minute + ":" + seconds;
            level_text.text = Config.ReadBoardLevel();
        }
    }

    public void SetGameData()
    {
        GameSettings.Instance.SetGameMode(Config.ReadBoardLevel());
    }
}
