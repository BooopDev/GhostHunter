using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static bool hasGameEnded = false;
    public GameObject panelScreen;
    public TMPro.TextMeshProUGUI text;
    private static string info;
    bool gameEnded;
    private void Start()
    {
        hasGameEnded = false;
        panelScreen.SetActive(false);
    }
    public void Update()
    {
        if(hasGameEnded && !gameEnded)
        {
            gameEnded = true;
            text.text = info;
            panelScreen.SetActive(true);
        }
    }
    public static void EndGame(string type)
    {
        info = type;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        hasGameEnded = true;
        
    }
}
