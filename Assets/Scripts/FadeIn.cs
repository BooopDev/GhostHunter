using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    Image image;
    float value = 1;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        value = Mathf.Clamp(value - 0.01f, 0, 1);
        Color col = new Color(0, 0, 0, value);
        image.color = col;
        if(image.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
