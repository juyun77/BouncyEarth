using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunderz : MonoBehaviour
{

    //public float speed = 4.0f; // 회전 속도
    GameObject earth;
    AudioSource myAudio; // 소리나게하기위해 오디오 객체 불러옴
    Vector3 pos;
    bool thrill = false;
    public float speed = 0.0f; // 번개 속도
    float ypos;

    // Use this for initialization
    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // 오디오파일도~
        earth = GameObject.Find("earth");
    }

    // Update is called once per frame
    void Update()
    {
        if (thrill == true)
        {
            ypos = -speed;
            transform.Translate(0, Time.deltaTime * ypos, 0);
            if (transform.position.y < 200)
            {
                thrill = false;
            }
        }
        else if (thrill == false)
        {
            ypos = speed;
            transform.Translate(0, Time.deltaTime * ypos, 0);
            if (transform.position.y > 300)
            {
                thrill = true;
            }
        }
        //this.transform.Rotate(0, 0, this.speed * 0.5f); //극악 난이도 번개
        //pos = this.transform.position;
        //transform.position += new Vector3(0, 1f * Time.deltaTime * speed, 0);
    }

    private void OnCollisionStay2D(Collision2D collision) // 지구와 충돌할시
    {
        if (collision.gameObject.tag == "earth")
        {
            myAudio.Play(); //충돌할때마다 소리재생
        }
    }
}