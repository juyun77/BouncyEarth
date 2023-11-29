using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    GameObject earth; // 지구공
    int hit = 0; // 블럭이 깨지게 하기 위해서 선언한 변수
    public int b_hit = 0;

    // Use this for initialization
    void Start()
    {
        earth = GameObject.Find("earth"); // 지구
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "earth") // 지구에 닿았을때
        {
            hit++; // hit를 증가시켜줌. 처음 값은 0이므로 1이됨 두번 닿으면 2가됨
            Debug.Log("처음 충돌!");
            if (hit == 2) // hit이 2일때 
            {
                Debug.Log("두번째 충돌!");
                //myAudio.Play(); // 벽돌이 파괴될 때 소리재생 //소리 재생은 되는데 객체가 바로 지워지기때문에 안들림 고로 empty object를 이용했음
                Destroy(this.gameObject); // 벽돌 없애줌
                hit = 0; // 그리고 hit수 초기화(다음 벽돌을 위해서)
            }
        }
    }
}