using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public GameObject stop;
    public GameObject play;
    public GameObject settingCanvas;


    // Update is called once per frame
    void Update () {

        
	}
    public void Stop()
    {
        Time.timeScale = 0;//일시정지
        stop.SetActive(true);

        if (settingCanvas) settingCanvas.gameObject.SetActive(true);
    
    }

    public void Play()
    {
        Time.timeScale = 1;//다시시작
        play.SetActive(true);
        if (settingCanvas) settingCanvas.gameObject.SetActive(false);
    }

public void Quit()
    {
        Application.Quit();
    }
}
