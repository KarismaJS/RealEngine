using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }
    public void ChangeTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ChangeStage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void ChangeStage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void ChangeStage3()
    {
        SceneManager.LoadScene("Stage3");
    }
    public void ChangeStage4()
    {
        SceneManager.LoadScene("Stage4");
    }
    public void ChangeStage5()
    {
        SceneManager.LoadScene("Stage5");
    }
}
