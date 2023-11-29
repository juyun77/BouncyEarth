using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackPrefab : MonoBehaviour {
    public GameObject blackhole;
    float span = 10f;//10초 마다 떨어지도록
    float delta = 0;
    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime; //앞프레임과 현재프레임의 시간 차이
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(blackhole) as GameObject;//블랙홀 인스턴트 생성
            int px = Random.Range(104, 1500);//블랙홀이 떨어지는 x의 범위(53~1000)
            go.transform.position = new Vector3(px, 1000, 0);

        }

    }
}
