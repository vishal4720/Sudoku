using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text textClock;
    // Start is called before the first frame update
    void Start()
    {
           textClock.text = Clock.instance.GetCurrentTimeText().text;
    }
}
