using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    bool check_direction = false; // 구름 방향 바꿔주기위해서
    public float speed = 100.0f; // 구름 속도

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (check_direction) // 이게 실행이 안됨 왜냐 초기값은 false니깐
        {
            transform.position += new Vector3(-1f * Time.deltaTime * speed, 0, 0); // 이 부분은 반대방향으로
            if (transform.localPosition.x <= 130) // 다시 반대로
                check_direction = false;
        }
        else // 이 부분 먼저 실행
        {
            transform.position += new Vector3(1f * Time.deltaTime * speed, 0, 0); // 우측 방향으로 계속 더해줌 프레임시간곱해줘서 이값은 계쏙 바뀜
            if (transform.localPosition.x >= 937) // 맵에서 x값이 7보다 커지면
                check_direction = true; // 다시 방향을 반대로 바꾸는 boolean값 바꿔줌
        }

    }
}
