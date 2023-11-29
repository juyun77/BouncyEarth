using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartPrefab : MonoBehaviour {

    public GameObject heart;
    float span = 1f;//5초 마다 떨어지도록
    float delta = 0;


    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime; //앞프레임과 현재프레임의 시간 차이
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(heart) as GameObject;//하트 인스턴트 생성
            int px = Random.Range(53, 1500);//하트가 떨어지는 x의 범위(-600~700)
            go.transform.position = new Vector3(px, 900, 0);


        }

    }
}
