using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{

    private new Renderer renderer;
    //public Player player;
    public float speed;
    public float offset;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = Time.time * speed;
        renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}