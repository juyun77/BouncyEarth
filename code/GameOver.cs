using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    GameObject life;
    GameObject life2;
    GameObject life3;
    GameObject life4;
    GameObject overPanel;
    GameObject stopScore;
    int scorePoint1 = 0;
    int scorePoint2 = 0;
    int scorePoint3 = 0;
    int scorePoint4 = 0;
    int scorePoint5 = 0;

    // Use this for initialization
    void Start()
    {

        this.life = GameObject.Find("life");
        this.life2 = GameObject.Find("life2");
        this.life3 = GameObject.Find("life3");
        this.life4 = GameObject.Find("life4");
        this.overPanel = GameObject.Find("Canvas").transform.Find("overPanel").gameObject;
        this.stopScore = GameObject.Find("Canvas").transform.Find("stText").gameObject;
    }
    public void decreaseLife()
    {
        if (this.life4.GetComponent<Image>().fillAmount == 0)
        {


            if (this.life.GetComponent<Image>().fillAmount == 0)
            {
                if (this.life2.GetComponent<Image>().fillAmount == 0)
                    this.life3.GetComponent<Image>().fillAmount -= 1f;
                this.life2.GetComponent<Image>().fillAmount -= 1f;
            }

            this.life.GetComponent<Image>().fillAmount -= 1f;

        }
        else

            this.life4.GetComponent<Image>().fillAmount = 0;
    }

    public void increseLife()
    {


        if (this.life3.GetComponent<Image>().fillAmount != 0)
        {
            if (this.life2.GetComponent<Image>().fillAmount != 0)
            {
                if (this.life.GetComponent<Image>().fillAmount == 0)
                    this.life.GetComponent<Image>().fillAmount += 1;
                else

                    this.life4.GetComponent<Image>().fillAmount += 1;
            }
            else

                this.life2.GetComponent<Image>().fillAmount += 1;

        }
        else if (this.life2.GetComponent<Image>().fillAmount == 0)
            this.life2.GetComponent<Image>().fillAmount += 1;
    }

    public void setGameOverUI()
    {
        if (this.life3.GetComponent<Image>().fillAmount == 0)
        {
            this.overPanel.SetActive(true);
            this.stopScore.GetComponent<increasing_Score>().EndCountScore();
            scorePoint1 = PlayerPrefs.GetInt("score1");
            scorePoint2 = PlayerPrefs.GetInt("score2");
            scorePoint3 = PlayerPrefs.GetInt("score3");
            scorePoint4 = PlayerPrefs.GetInt("score4");
            scorePoint5 = PlayerPrefs.GetInt("score5");
            endGame();

        }
    }

    public void die()
    {
        this.life.GetComponent<Image>().fillAmount -= 1f;
        this.life2.GetComponent<Image>().fillAmount -= 1f;
        this.life3.GetComponent<Image>().fillAmount -= 1f;
        this.life4.GetComponent<Image>().fillAmount -= 1f;
    }


    public void endGame()
    {
       // Text nowScore = overPanel.transform.Find("scoreText").GetComponent<Text>();
        Text bestScore1 = overPanel.transform.Find("score1").GetComponent<Text>();
        Text bestScore2 = overPanel.transform.Find("score2").GetComponent<Text>();
        Text bestScore3 = overPanel.transform.Find("score3").GetComponent<Text>();
        Text bestScore4 = overPanel.transform.Find("score4").GetComponent<Text>();
        Text bestScore5 = overPanel.transform.Find("score5").GetComponent<Text>();
        int resultScore = stopScore.GetComponent<increasing_Score>().scorePoint;

        if (resultScore >= scorePoint1)
        {
            scorePoint1 = resultScore;
            PlayerPrefs.SetInt("score1", scorePoint1);
        }
        else if (resultScore >= scorePoint2)
        {
            scorePoint2 = resultScore;
            PlayerPrefs.SetInt("score2", scorePoint2);

        }
        else if (resultScore >= scorePoint3)
        {
            scorePoint3 = resultScore;
            PlayerPrefs.SetInt("score3", scorePoint3);

        }
        else if (resultScore >= scorePoint4)
        {
            scorePoint4 = resultScore;
            PlayerPrefs.SetInt("score4", scorePoint4);

        }
        else if (resultScore >= scorePoint5)
        {
            scorePoint5 = resultScore;
            PlayerPrefs.SetInt("score5", scorePoint5);
        }
        bestScore1.text = "1위: " + PlayerPrefs.GetInt("score1").ToString();
        bestScore2.text = "2위: " + PlayerPrefs.GetInt("score2").ToString();
        bestScore3.text = "3위: " + PlayerPrefs.GetInt("score3").ToString();
        bestScore4.text = "4위: " + PlayerPrefs.GetInt("score4").ToString();
        bestScore5.text = "5위: " + PlayerPrefs.GetInt("score5").ToString();
       // nowScore.text = resultScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
