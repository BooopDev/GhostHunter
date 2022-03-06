using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimer : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    float timer;
    string minutes;
    string seconds;

    private void Start()
    {
        timer = 300;
        text = GetComponent<TMPro.TextMeshProUGUI>();
        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = (timer % 60).ToString("00");
        text.text = string.Format("{0}:{1}", minutes, seconds);
    }

    void Update()
    {
        if(!GameOver.hasGameEnded)
        {
            if (timer < 0.5f)
            {
                GameOver.EndGame("You Ran Out Of Time...");
                timer = 0;
                minutes = Mathf.Floor(timer / 60).ToString("00");
                seconds = (timer % 60).ToString("00");
                text.text = string.Format("{0}:{1}", minutes, seconds);
            }
            else
            {
                timer -= Time.deltaTime;

                minutes = Mathf.Floor(timer / 60).ToString("00");
                seconds = (timer % 60).ToString("00");
                text.text = string.Format("{0}:{1}", minutes, seconds);
            }
        }
    }
}
