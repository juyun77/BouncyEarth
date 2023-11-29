using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class increasing_Score : MonoBehaviour {

    public int scorePoint = 0;
    private Text time;


    private void Awake()
    {
        time = GetComponent<Text>();
        time.text = "SCORE: 0";
        StartCoroutine("PlusScoreRoutine");
    }

    public void PlusScore(int plusPoint)
    {
        scorePoint += plusPoint;
        time.text = "SCORE: " + scorePoint.ToString();

    }
    IEnumerator PlusScoreRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            PlusScore(10);

        }

    }
    public void EndCountScore()//게임이 끝났을때
    {

        StopAllCoroutines(); //점수를 멈춰줌
        time.text = "SCORE : " + scorePoint.ToString(); //점수표시
    }
}
