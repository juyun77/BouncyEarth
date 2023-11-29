using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class earth : MonoBehaviour
{
    public Rigidbody2D rb; // 객체 몸체부분 그리고 Rigidbody랑 연결된 부분을 모든 함수 내에서 쓰기 위해서 전역변수로 설정
    public float fSpeed; // 좌우속도
    public float jumpPower; // 공이 튀기는 힘. 수정 가능하게 하기 위해서 public으로 선언
    private Vector2 Direction = new Vector2(0, 0); // FixedUpdate에 사용하기 위한 방향벡터
    private Vector3 Posi1; // 계속 사용하게될 위치벡터(초기위치)
    private Vector3 Posi2; // 새로 사용하게될 위치벡터(옮겨주기위해서)
    Vector3 pos_right;
    Vector3 pos_left;
    Vector3 pos_down;

    //게임 오브젝트들 선언부
    GameObject block; // 벽돌1
    //GameObject block1; // 벽돌2
    GameObject goal; // 목적지
    GameObject thunder; // 번개
    GameObject Grid; // 그리드
    GameObject color_button; // 버튼 부모
    GameObject red_button; // 빨간색 버튼
    GameObject green_button; // 초록색 버튼
    //AudioSource myAudio; // 소리나게하기위해 오디오 객체 불러옴

    int hit = 0; // 블럭이 깨지게 하기 위해서 선언한 변수
    public static int scene_number=1;
    bool use_right = false;
    bool use_left = false;
    bool use_bottom = false;

    void Start() // 처음에 유니티 실행되면 1번만 실행됨 이때 특별히 객체를 불러와야됨 rigidbody나 audio같은 객체들!!
    {
        rb = GetComponent<Rigidbody2D>(); // rigidbody를 rb에 저장
        //myAudio = GetComponent<AudioSource>(); // 오디오파일도~
        block = GameObject.Find("block"); // 블럭들(2번이상 충돌해야깨짐)
        //block1 = GameObject.Find("block1");
        goal = GameObject.Find("goal"); // 목적지
        thunder = GameObject.Find("thunder"); // 번개
        Grid = GameObject.Find("Grid"); // 그리드
        red_button = GameObject.Find("red_button"); // 빨간색 버튼
        green_button = GameObject.Find("green_button"); // 초록색 버튼
        color_button = GameObject.Find("color_button"); // 버튼 부모
                                                        //scene_number = SceneManager.GetActiveScene().buildIndex; // 몇번째 씬인지 값 받아오기 위함 처음 씬 index는 0번!
                                                        // 씬 번호가 0번부터 시작하는데 Scene들이랑 헷갈리지 않게 1 미리 증가시켜둠!
                                                        //green_button.SetActive(false);
        if (scene_number == 1) // 1번째 씬
        {
            Posi1 = new Vector3(98, 219, 0); // 지구공 위치를 초기화 시키기 위해서 위치변수 저장해둠
        }
        if (scene_number == 2) // 2번째 씬
        {
            Posi1 = new Vector3(92, 177, 0); // 지구공 위치를 초기화 시키기 위해서 위치변수 저장해둠
        }
        if (scene_number == 3) // 3번째 씬
        {
            Posi1 = new Vector3(935, 721, 0); // 지구공 위치를 초기화 시키기 위해서 위치변수 저장해둠
        }
        if (scene_number == 4) // 4번째 씬
        {
            Posi1 = new Vector3(73, 89, 0); // 지구공 위치를 초기화 시키기 위해서 위치변수 저장해둠
        }
        if (scene_number == 5) // 5번째 씬
        {
            Posi1 = new Vector3(73, 89, 0); // 지구공 위치를 초기화 시키기 위해서 위치변수 저장해둠
        }
        Debug.Log("씬 번호 " + scene_number);
    }
    void FixedUpdate() // rigidbody를 사용하기 위함 - 참고로 Update는 불규칙한 충돌이므로 물리엔진 충돌검사 등이 제대로 안될 수 있대요~
    {
        //키 검사 -키 중에 화살표 좌: -1 , 우: 1을 리턴하고 눌리지 않으면 0을 리턴함
        float axis = Input.GetAxis("Horizontal"); // 좌우 입력받음
        Vector2 pos = transform.position;  // 현재위치를 받아서 pos에 저장
        if (axis != 0)
        {
            Direction.x = axis; // 방향의x를 axis로
            pos += Direction * fSpeed * Time.deltaTime; // 위치를 방향 * 속도 * 타임으로(속도를 곱해주는이유는 프레임당이기때문)
            transform.position = pos; // 위치를 진짜 바꿔주는 함수 transform.position기억!
        }
        if (use_right)
        {
            float speed = 400.0f;
            rb.gravityScale = 0;
            this.transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
            if (axis != 0)
            {
                use_right = false;
                rb.gravityScale = 1;
            }
        }
        if (use_left)
        {
            float speed = 400.0f;
            rb.gravityScale = 0;
            this.transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            if (axis != 0)
            {
                use_left = false;
                rb.gravityScale = 1;
            }
        }
        if (use_bottom)
        {
            float speed = 25.0f;
            rb.gravityScale = 0;
            this.transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
            if (axis != 0)
            {
                use_bottom = false;
                rb.gravityScale = 1;
            }
        }
    }

    /// <summary>
    /// Trigger와 Collision의 차이점이 존재. 직접 충돌을 구현하고 싶으면 Collision을 이용해야됨!
    /// Trigger를 써야 될 때와 Collision을 써야될 경우가 따로 존재!!
    /// 그리고 Enter,Stay,Exit가 각각 존재.. 잘이용하면됨
    /// </summary>
    /// 
    //안으로 들어갈때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "reStart") // floor는 낙하를 의미함 tile명 보면 floor는 맵의 바깥을 둘러싸고있음
        {
            //Debug.Break(); //일시 정지
            // myAudio.Play(); //충돌할때마다(죽을때마다) 소리재생
            this.transform.position = Posi1; // 지구공 위치를 처음으로 옮김
            pos_left = new Vector3(0,0,0);
            pos_right = new Vector3(0,0,0);
            pos_down = new Vector3(0, 0, 0);

        }
        if (collision.gameObject.tag == "goal") // 만약 목적지로 들어갔다면
        {
            Debug.Log("통과성공!"); // 통과성공 로그띄움
            if (scene_number == 1)
            {
                SceneManager.LoadScene("stage2_Sc");// 씬 전환
                scene_number = 2;
            }
            else if (scene_number == 2)
            {
                SceneManager.LoadScene("stage3_Sc"); // 씬 전환
                scene_number = 3;
            }
            else if (scene_number == 3)
            {
                SceneManager.LoadScene("stage4_Sc"); // 씬 전환
                scene_number = 4;
            }
            else if (scene_number == 4)
            {
                SceneManager.LoadScene("stage5_Sc"); // 씬 전환
                scene_number = 5;
            }
            else if (scene_number == 5)
            {
                SceneManager.LoadScene("stage6_Sc"); // 씬 전환
                scene_number = 6;
            }
            else if (scene_number == 6)
            {
                SceneManager.LoadScene("stage7_Sc"); // 씬 전환
                scene_number = 7;
            }
            else if (scene_number == 7)
            {
                SceneManager.LoadScene("stage8_Sc"); // 씬 전환
                scene_number = 8;
            }
            else if (scene_number == 8)
            {
                SceneManager.LoadScene("stage9_Sc"); // 씬 전환
                scene_number = 9;
            }
            else if (scene_number == 9)
            {
                SceneManager.LoadScene("stage10_Sc"); // 씬 전환
                scene_number = 10;
            }

        }
    }

    //직접 충돌했을때! 여기가 KeyPoint임
    private void OnCollisionStay2D(Collision2D collision)
    {
        /// <summary>
        /// Input.Axis 안쓰는 이유 : Input.GetAxis로 받으면 방향키가 연속입력만 허용됨 ex) 왼쪽 벽에 맞을 때 우측 입력이 안먹음
        /// </summary>

        //float axis = Input.GetAxis("Horizontal");

        if (collision.gameObject.tag == "ground") // 땅에 박으면
        {
            rb.velocity = new Vector3(0, jumpPower, 0); // 몸체의 velocity(속도)인데 위치라고 봐도 무방. 유니티에서 transform.position말고 또 객체이동에 쓰이는 함수'
            rb.gravityScale = 80.0f;
            // y값만 바꿔준다. rigidbody에 중력값이 들어가있기때문에 다시 떨어졌다가 또 이 함수가 다시 실행됨
        }
        if (collision.gameObject.tag == "cloud") // 구름에 닿을때
        {
            rb.velocity = new Vector3(0, jumpPower * 0.8f, 0); // 점프 속도를 감소시켜줌. 이건 그냥 너무 높이띄면 게임이 쉬워질거같아서
        }
        if (collision.gameObject.tag == "fly") // 파리모양에 닿았을때
                rb.velocity = new Vector3(2, 725, 0); // 슈퍼점프를 해줌
        if (collision.gameObject.tag == "groundL") // 왼쪽 벽에 닿았을때
        {
            if (Input.GetKey(KeyCode.RightArrow)) // 그리고 우측 키가 눌렸을 때
            {
                rb.velocity = new Vector3(2, 4, 0); // 몸체를 오른쪽 방향 그리고 위로 가게 해줌
                //rb.AddForce(Vector2.up * -9.8f); // 중력값 보정. 몸체에 힘을 더해주기 위해서 AddForce이용!
                Debug.Log("좌측 충돌, 오른쪽 키 입력!!"); // 로그로 직접 디버깅해봄
            }
            if (Input.GetKey(KeyCode.LeftArrow)) // 그리고 왼쪽 키가 눌렸을 때
            {
                rb.gravityScale = 1.2f;
                Debug.Log("좌측 충돌, 왼쪽 키 입력!!");
            }
        }
        if (collision.gameObject.tag == "groundR") // 우측 벽에 닿았을 때
        {
            if (Input.GetKey(KeyCode.LeftArrow)) // 그리고 왼쪽 키가 눌렸을 때
            {
                rb.velocity = new Vector3(-2, 4, 0); // 물체를 왼쪽 방향 그리고 위로 가게 해줌
                //rb.AddForce(Vector2.up * -9.8f); // 위와 동일
                Debug.Log("우측 충돌, 왼쪽 키 입력!!");
            }
            if (Input.GetKey(KeyCode.RightArrow)) // 그리고 우측 키가 눌렸을 때
            {
                //rb.AddForce(Vector2.up * -9.8f); // 중력값 보정. 몸체에 힘을 더해주기 위해서 AddForce이용!
                rb.gravityScale = 1.2f;
                Debug.Log("우측 충돌, 오른쪽 키 입력!!"); // 로그로 직접 디버깅해봄
            }
        }
        if (collision.gameObject.tag == "thunder") // floor는 낙하를 의미함 tile명 보면 floor는 맵의 바깥을 둘러싸고있음
        {
            this.transform.position = Posi1; // 지구공 위치를 처음으로 옮김
        }
        if (collision.gameObject.tag == "direction_R")
        {
            Posi2 = collision.transform.position; //부딪힌 블록(오른쪽 화살표 블록) 위치를 저장
            pos_right = new Vector3(3, 0, 0); // 오른쪽으로 3만큼
            this.transform.position = Posi2 + pos_right; // 지구공 위치를 화살표 블록에서 오른쪽으로 3만큼 이동시킴.
            rb.gravityScale = 0;
            use_right = true;
        }
        if (collision.gameObject.tag == "direction_L")
        {
            Posi2 = collision.transform.position; //부딪힌 블록(오른쪽 화살표 블록) 위치를 저장
            pos_left = new Vector3(-250, 0, 0); // 왼쪽으로 3만큼
            this.transform.position = Posi2 + pos_left; // 지구공 위치를 화살표 블록에서 왼쪽으로 3만큼 이동시킴.
            rb.gravityScale = 0;
            use_left = true;
        }
        if (collision.gameObject.tag == "direction_B")
        {
            Posi2 = collision.transform.position; //부딪힌 블록(오른쪽 화살표 블록) 위치를 저장
            pos_down = new Vector3(0, -3, 0); // 아래쪽으로 3만큼
            this.transform.position = Posi2 + pos_down; // 지구공 위치를 화살표 블록에서 아래쪽으로 3만큼 이동시킴.
            rb.gravityScale = 0;
            use_bottom = true;
        }
        if (collision.gameObject.tag == "red_button")
        {
            Debug.Log("충돌!");
            rb.velocity = new Vector3(0, jumpPower, 0);
            Grid.transform.Find("thunder").gameObject.SetActive(false);
            color_button.transform.Find("green_button").gameObject.SetActive(true);
            color_button.transform.Find("red_button").gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "green_button")
        {
            rb.velocity = new Vector3(0, jumpPower, 0);
            Grid.transform.Find("thunder").gameObject.SetActive(true);
            color_button.transform.Find("red_button").gameObject.SetActive(true);
            color_button.transform.Find("green_button").gameObject.SetActive(false);
        }
    }
}