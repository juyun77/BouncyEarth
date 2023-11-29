using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

public class change_Scene : MonoBehaviour {
    public static int scnum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Start_BT()
    {
        SceneManager.LoadScene("select_Sc");
    }
    public void Exit_BT()
    {
        Application.Quit();
    }
    public void select_Back_BT()
    {
        SceneManager.LoadScene("main_Sc");
    }
    public void select_Stage_BT()
    {
        SceneManager.LoadScene("selectLv_Sc");
    }
    public void select_Avoid_BT()
    {
        SceneManager.LoadScene("avoid_mode");
    }
    public void selectLv_Back_BT()
    {
        SceneManager.LoadScene("select_Sc");
    }
    public void avoid_Back_BT()
    {
        SceneManager.LoadScene("select_Sc");
    }
    public void avoid_Restart()
    {
        SceneManager.LoadScene("avoid_mode");
    }
    public void avoid_Quit()
    {
        SceneManager.LoadScene("main_Sc");
    }
    public void st1_st2()
    {
        SceneManager.LoadScene("main_Sc");
    }public void st1_Back()
    {
        SceneManager.LoadScene("selectLv_Sc");
    }
    public void go_st1()
    {
        SceneManager.LoadScene("stage1_Sc");
        earth.scene_number = 1;
    }
    public void go_st2()
    {
        SceneManager.LoadScene("stage2_Sc");
        earth.scene_number = 2;
    }
    public void go_st3()
    {
        SceneManager.LoadScene("stage3_Sc");
        earth.scene_number = 3;
    }
    public void go_st4()
    {
        SceneManager.LoadScene("stage4_Sc");
        earth.scene_number = 4;
    }
    public void go_st5()
    {
        SceneManager.LoadScene("stage5_Sc");
        earth.scene_number = 5;
    }
    public void go_st6()
    {
        SceneManager.LoadScene("stage6_Sc");
        earth.scene_number = 6;
    }
    public void go_st7()
    {
        SceneManager.LoadScene("stage7_Sc");
        earth.scene_number = 7;
    }
    public void go_st8()
    {
        SceneManager.LoadScene("stage8_Sc");
        earth.scene_number = 8;
    }
    public void go_st9()
    {
        SceneManager.LoadScene("stage9_Sc");
        earth.scene_number = 9;
    }
    public void go_st10()
    {
        SceneManager.LoadScene("stage10_Sc");
        earth.scene_number = 10;
    }

}
