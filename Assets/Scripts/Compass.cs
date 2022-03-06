using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    float timer;
    string minutes;
    string seconds;
    Transform target;
    Vector3 direction;
    void Start()
    {
        timer = 10;
        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = (timer % 60).ToString("00");
        text.text = string.Format("{0}:{1}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        foreach (GameObject chest in chests)
        {
            if (chest.GetComponent<ChestCheck>().hasGhost)
            {
                target = chest.transform;
            }
        }

        Vector3 targetDir = target.position - transform.root.position;
        Vector3 forward = transform.root.forward;
        float angle = Vector3.Angle(targetDir, forward);
        float dot = Vector3.Dot(transform.root.right, targetDir);
        if(dot > 0)
        {
            angle = -angle;
        }
        direction = new Vector3(0, 0, angle);
        
        Timer();
    }

    void Timer()
    {
        if (timer <= 0)
        {
            timer = 10;
            UpdateCompass();
        }
        timer -= Time.deltaTime;

        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = (timer % 60).ToString("00");
        text.text = string.Format("{0}:{1}", minutes, seconds);
    }
    void UpdateCompass()
    {
        transform.localRotation = Quaternion.Euler(direction);
    }
}
