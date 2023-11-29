using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveHeart : MonoBehaviour
{
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        this.player = GameObject.Find("earth");
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-3, -5, 0);

        if (transform.position.y < 65) //바닥에 닫으면 사라짐.
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
        GameObject check = GameObject.Find("GameManager");
        check.GetComponent<GameOver>().increseLife();
    }
}
