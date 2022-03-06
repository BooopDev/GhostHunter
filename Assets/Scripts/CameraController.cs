using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private Animator anim;
    public GraphicRaycaster menu;
    public GraphicRaycaster instructions;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void MoveCamera(string clip)
    {
        if(clip == "Instructions")
        {
            menu.enabled = false;
            instructions.enabled = true;
        }
        else
        {
            menu.enabled = true;
            instructions.enabled = false;
        }
        anim.Play(clip);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
