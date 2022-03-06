using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Toggle m_MenuToggle;
	private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    [HideInInspector]
    public static bool m_Paused;
    public GameObject[] menuItems;


    void Awake()
    {
        m_Paused = false;
        Cursor.visible = false;
        m_MenuToggle = GetComponent <Toggle> ();
	}


    private void MenuOn ()
    {
        foreach(GameObject item in menuItems)
        {
            item.SetActive(true);
        }
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        m_Paused = true;
    }


    public void MenuOff ()
    {
        foreach (GameObject item in menuItems)
        {
            item.SetActive(false);
        }
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        m_Paused = false;
    }


    public void OnMenuStatusChange ()
    {
        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape) && !GameOver.hasGameEnded)
		{
		    m_MenuToggle.isOn = !m_MenuToggle.isOn;
            OnMenuStatusChange();
        }
	}

    public void Resume()
    {
        m_MenuToggle.isOn = false;
        OnMenuStatusChange();
    }
    public GameObject endPanel;

    public void Restart()
    {
        m_MenuToggle.isOn = false;
        OnMenuStatusChange();
        endPanel.SetActive(false);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
