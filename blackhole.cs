using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackhole : MonoBehaviour
{
    GameObject earth;
    public float speed = 10.0f; // 회전속도
   // AudioSource myAudio; // 소리나게하기위해 오디오 객체 불러옴

    // Use this for initialization
    void Start()
    {
       // myAudio = GetComponent<AudioSource>(); // 오디오파일도~
        earth = GameObject.Find("earth");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, this.speed * 2);
    }

    //private void OnCollisionStay2D(Collision2D collision) // 지구와 충돌할시
    //{
    //    if (collision.gameObject.tag == "earth")
    //    {
    //        myAudio.Play(); //충돌할때마다 소리재생
    //    }
    //}
}