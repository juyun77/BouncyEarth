using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    GameObject earth;
    AudioSource myAudio; // 소리나게하기위해 오디오 객체 불러옴

    // Use this for initialization
    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // 오디오파일도~
        earth = GameObject.Find("Earth");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision) // 지구와 충돌할시
    {
        if (collision.gameObject.tag == "Player")
        {
            myAudio.Play(); //충돌할때마다 소리재생
        }
    }
}