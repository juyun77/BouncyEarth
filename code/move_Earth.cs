using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_Earth : MonoBehaviour
{

    public float speed = 300; // 공이 움직이는 스피드

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputx = Input.GetAxisRaw("Horizontal"); // 수평으로 움직이게 해준다

        transform.Translate(speed * inputx * Time.deltaTime, 0, 0); // 인풋값과 시간과 스피드를 계산하여 좌우로 움직인다.
    }
}