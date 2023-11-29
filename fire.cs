using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    float speed = 100.0f;
    public Rigidbody2D rb;
    GameObject flare; // 불꽃이 새로 추가됨
    GameObject earth;
    Vector2 pos; // 처음 위치를 반환하기 위함
    AudioSource myAudio; // 소리나게하기위해 오디오 객체 불러옴

    void Start()
    {
        pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>(); // 오디오파일도~
        earth = GameObject.Find("earth");
        flare = GameObject.Find("flare"); // 불꽃 객체
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //y축으로 지속이동
        transform.position += new Vector3(0, 3f * Time.deltaTime * speed, 0); // 이 부분은 구름에서 봤을꺼라 생각함
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ceiling") // 천장 tile을 새로 만들어줬음
        {
            transform.position = pos;// 천장에 닿으면 초기위치로 이동
        }
    }

    private void OnCollisionStay2D(Collision2D collision) // 지구와 충돌할시
    {
        Vector3 posi = new Vector3(98, 219, 0); // 원래 위치로 보정
        
        if (collision.gameObject.tag == "earth")
        {
            earth.transform.position = posi;
            myAudio.Play(); //충돌할때마다(죽을때마다) 소리재생
        }
    }
}