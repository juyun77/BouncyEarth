using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;

//플레이어(Earth)에 들어가는 코드
public class st610 : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
        GameObject goal;

    public float move_speed; // 좌우 이동속도
    public float jump_speed_init; // 땅에 닿을 때 점프하는 속도.
    public float jump_gravity; // 중력 가속도
    public float jump_speed; // 낙하 속도(음수) & 점프 속도(양수)

    public bool btn_state = true; // true: 번개 나타남, false: 번개 사라짐
    GameObject Grid;

    public bool isLeftwall = false;
    public bool isRightwall = false;
    public int isGrounded = 0; // 0 : 공중, 1이상 : 지면
    public int superjump = 0;
    public int destroy = 0;
    public int direction; // 1: up(상승중), 2: down(하강중)
    public Vector2 startPos;

    private void Init() //초기값 설정 함수.
    {
        //영구 고정값
        move_speed = 0.02f;
        jump_speed_init = 0.045f; //초기 점프 속도와 중력 가속도 값을 조절해서 점프 최대높이 및 점프 시간을 조절할 수 있다.
        jump_gravity = 0.0025f;

        //변수
        jump_speed = 0.001f; // 공중에서 하강중인 속도. 
        direction = 2;
        startPos = transform.position;
        Grid = GameObject.Find("Grid");
    }

    private void Start()
    {
        Init();

    }

    private void Update()
    {
        float inputx = Input.GetAxisRaw("Horizontal"); //누르는 동안 좌, 우 방향키별로 -1, 1 값을 준다. 떼고있으면 0  
        if (!(isLeftwall && inputx == -1) && !(isRightwall && inputx == 1)) // 벽에 이미 박은 상태에서 벽 방향으로 이동하지 않으면,
            if (direction <= 2) //  3, 4는 벽타기 점프 도중.(이 아니면)
                transform.Translate(move_speed * inputx, 0, 0); // 좌우로 이동시켜라
        if (isLeftwall && inputx == 1)
            isLeftwall = false;
        if (isRightwall && inputx == -1)
            isRightwall = false;


        CheckCeiling();
        CheckLeftWall(); // 공이 왼쪽 벽면과 닿았는지 체크
        CheckRightWall();

        Vector2 pos = gameObject.transform.position; //gameObject의 position을 알아내어 pos에 저장.
        CheckGround(); // 공이 지면인지 공중인지 체크
        JumpProcess();

        if(Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("selectLv_Sc");
        }


    }

    void CheckGround() //지면 체크 함수.
    {
        Vector2 MyPos = transform.position;
        isGrounded = 0; // 공중
        superjump = 0;
        int button = 0;
        int brick = 0;
        destroy = 0;

        RaycastHit2D hit_down;
        isGrounded = 0;
        for (int i = -1; i <= 1; i++)
        {
            Debug.DrawRay(MyPos + new Vector2(i * 0.025f, 0), this.transform.TransformDirection(Vector2.down) * 0.05f, Color.yellow);
            hit_down = Physics2D.Raycast(MyPos + new Vector2(i * 0.025f, 0), this.transform.TransformDirection(Vector2.down) * 0.05f);
            if (hit_down.transform.gameObject.tag != null && hit_down.transform.gameObject.tag == "Ground" && hit_down.distance <= 0.1f) //raycast로 물체에 맞을 경우 그 정보는 hit에 저장됨. hit의 layer가 8번("Ground") 일경우...
                isGrounded++;
            else if (hit_down.transform.gameObject.tag != null && hit_down.transform.gameObject.tag == "fly" && hit_down.distance <= 0.1f)
                superjump++;
            else if (hit_down.transform.gameObject.tag != null && hit_down.transform.gameObject.tag == "thunder" && hit_down.distance <= 0.1f)
                destroy++;
            else if (hit_down.transform.gameObject.tag != null && hit_down.transform.gameObject.tag == "button" && hit_down.distance <= 0.1f)
            {
                button++;
                isGrounded++;
            }

            if (hit_down.transform.gameObject.tag != null && hit_down.transform.gameObject.name == "blackhole" && hit_down.distance <= 0.1f) //raycast로 물체에 맞을 경우 그 정보는 hit에 저장됨. hit의 layer가 8번("Ground") 일경우...
            {
                Destroy(gameObject);// 여기에 씬 전환코드 삽입!
                if(earth.scene_number == 6)
                {
                    SceneManager.LoadScene("stage7_Sc");
                    earth.scene_number = 7;
                } else if(earth.scene_number == 7)
                {
                    SceneManager.LoadScene("stage8_Sc");
                    earth.scene_number = 8;
                }
                else if (earth.scene_number == 8)
                {
                    SceneManager.LoadScene("stage9_Sc");
                    earth.scene_number = 9;
                }
                else if (earth.scene_number == 9)
                {
                    SceneManager.LoadScene("stage10_Sc");
                    earth.scene_number = 10;
                }
                else if (earth.scene_number == 10)
                {
                    SceneManager.LoadScene("main_Sc");
                    earth.scene_number = 1;
                }
            }

        }

        if (destroy > isGrounded + superjump)
        {
            transform.position = startPos;
            Init();
        }
        else if (button > 0)
        {
            if (btn_state)
            {
                btn_state = false; // 상태 비활성화
                Grid.transform.Find("thunder").gameObject.SetActive(false);
            }
            else
            {
                btn_state = true;
                Grid.transform.Find("thunder").gameObject.SetActive(true);
            }
        }
    }

    public void CheckCeiling() // 천장 체크 함수.
    {
        Vector2 MyPos = transform.position;
        RaycastHit2D hit_up;
        int ceiling = 0;
        destroy = 0;

        for (int i = -1; i <= 1; i++)
        {
            Debug.DrawRay(MyPos + new Vector2(i * 0.025f, 0), this.transform.TransformDirection(Vector2.up) * 0.05f, Color.white);
            hit_up = Physics2D.Raycast(this.transform.position, this.transform.TransformDirection(Vector2.up) * 0.05f);
            if (hit_up.transform.gameObject.tag != null && hit_up.transform.gameObject.tag == "Ground" && hit_up.distance <= 0.1f)
                ceiling++;
            else if (hit_up.transform.gameObject.tag != null && hit_up.transform.gameObject.tag == "thunder" && hit_up.distance <= 0.1f)
                destroy++;

            if (hit_up.transform.gameObject.tag != null && hit_up.transform.gameObject.name == "blackhole" && hit_up.distance <= 0.1f) //raycast로 물체에 맞을 경우 그 정보는 hit에 저장됨. hit의 layer가 8번("Ground") 일경우...
            {
                Destroy(gameObject);  // 여기에 씬 전환코드 삽입!
                if (earth.scene_number == 6)
                {
                    SceneManager.LoadScene("stage7_Sc");
                    earth.scene_number = 7;
                }
                else if (earth.scene_number == 7)
                {
                    SceneManager.LoadScene("stage8_Sc");
                    earth.scene_number = 8;
                }
                else if (earth.scene_number == 8)
                {
                    SceneManager.LoadScene("stage9_Sc");
                    earth.scene_number = 9;
                }
                else if (earth.scene_number == 9)
                {
                    SceneManager.LoadScene("stage10_Sc");
                    earth.scene_number = 10;
                }
                else if (earth.scene_number == 10)
                {
                    SceneManager.LoadScene("main_Sc");
                    earth.scene_number = 1;
                }
            }
        }


        if (ceiling > destroy)
        {
            jump_speed = 0.0025f;
            direction = 2;
        }
        else if (destroy > ceiling)
        {
            transform.position = startPos;
            Init();
        }
    }


    void CheckLeftWall() // 좌측벽 체크 함수.
    {
        Vector2 MyPos = transform.position;
        RaycastHit2D hit_left;
        int leftwall = 0;
        destroy = 0;
        isLeftwall = false;

        for (int i = -1; i <= 1; i++)
        {
            Debug.DrawRay(MyPos + new Vector2(0, i * 0.025f), this.transform.TransformDirection(Vector2.left) * 0.05f, Color.cyan, 0.5f);
            hit_left = Physics2D.Raycast(MyPos + new Vector2(0, i * 0.025f), this.transform.TransformDirection(Vector2.left) * 0.05f);
            if (hit_left.transform.gameObject.tag != null && hit_left.transform.gameObject.tag == "Ground" && hit_left.distance <= 0.05f)
                leftwall++;
            else if (hit_left.transform.gameObject.tag != null && hit_left.transform.gameObject.tag == "thunder" && hit_left.distance <= 0.1f)
                destroy++;

            if (hit_left.transform.gameObject.tag != null && hit_left.transform.gameObject.name == "blackhole" && hit_left.distance <= 0.1f) //raycast로 물체에 맞을 경우 그 정보는 hit에 저장됨. hit의 layer가 8번("Ground") 일경우...
            {
                Destroy(gameObject);  // 여기에 씬 전환코드 삽입!
                if (earth.scene_number == 6)
                {
                    SceneManager.LoadScene("stage7_Sc");
                    earth.scene_number = 7;
                }
                else if (earth.scene_number == 7)
                {
                    SceneManager.LoadScene("stage8_Sc");
                    earth.scene_number = 8;
                }
                else if (earth.scene_number == 8)
                {
                    SceneManager.LoadScene("stage9_Sc");
                    earth.scene_number = 9;
                }
                else if (earth.scene_number == 9)
                {
                    SceneManager.LoadScene("stage10_Sc");
                    earth.scene_number = 10;
                }
                else if (earth.scene_number == 10)
                {
                    SceneManager.LoadScene("main_Sc");
                    earth.scene_number = 1;
                }
            }

        }

        if (leftwall > destroy)
        {
            isLeftwall = true;
            if (direction >= 3)
                direction = 2;
            Vector2 pos_RightOfLeftWall = new Vector2(this.transform.position.x + 0.01f, this.transform.position.y);
            this.transform.position = pos_RightOfLeftWall;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction = 3;
                jump_speed = jump_speed_init;
            }
        }
        else if (destroy < leftwall)
        {
            transform.position = startPos;
            Init();
        }
    }

    void CheckRightWall() // 우측벽 체크 함수.
    {
        Vector2 MyPos = transform.position;
        RaycastHit2D hit_right;
        int rightwall = 0;
        destroy = 0;
        isRightwall = false;

        for (int i = -1; i <= 1; i++)
        {
            Debug.DrawRay(MyPos + new Vector2(0, i * 0.025f), this.transform.TransformDirection(Vector2.right) * 0.05f, Color.red, 0.5f);
            hit_right = Physics2D.Raycast(MyPos + new Vector2(0, i * 0.025f), this.transform.TransformDirection(Vector2.right) * 0.05f);
            if (hit_right.transform.gameObject.tag != null && hit_right.transform.gameObject.tag == "Ground" && hit_right.distance <= 0.05f)
                rightwall++;
            else if (hit_right.transform.gameObject.tag != null && hit_right.transform.gameObject.tag == "thunder" && hit_right.distance <= 0.1f)
                destroy++;

            if (hit_right.transform.gameObject.tag != null && hit_right.transform.gameObject.name == "blackhole" && hit_right.distance <= 0.1f) //raycast로 물체에 맞을 경우 그 정보는 hit에 저장됨. hit의 layer가 8번("Ground") 일경우...
            {
                Destroy(gameObject); // 여기에 씬 전환코드 삽입!
                if (earth.scene_number == 6)
                {
                    SceneManager.LoadScene("stage7_Sc");
                    earth.scene_number = 7;
                }
                else if (earth.scene_number == 7)
                {
                    SceneManager.LoadScene("stage8_Sc");
                    earth.scene_number = 8;
                }
                else if (earth.scene_number == 8)
                {
                    SceneManager.LoadScene("stage9_Sc");
                    earth.scene_number = 9;
                }
                else if (earth.scene_number == 9)
                {
                    SceneManager.LoadScene("stage10_Sc");
                    earth.scene_number = 10;
                }
                else if (earth.scene_number == 10)
                {
                    SceneManager.LoadScene("main_Sc");
                    earth.scene_number = 1;
                }
            }
        }

        if (rightwall > destroy)
        {
            isRightwall = true;
            if (direction >= 3)
                direction = 2;
            Vector2 pos_RightOfLeftWall = new Vector2(this.transform.position.x - 0.01f, this.transform.position.y);
            this.transform.position = pos_RightOfLeftWall;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction = 4;
                jump_speed = jump_speed_init;
            }
        }
        else if (rightwall < destroy)
        {
            transform.position = startPos;
            Init();
        }
    }

    private void JumpProcess() //파라미터로 오브젝트의 현재 y위치값을 받는다.
    {
        Vector2 MyPos = transform.position;

        if (direction == 1) // up 상승
        {
            MyPos.y += jump_speed; //(상승)속도만큼 y위치 증가.
            if (jump_speed <= 0.0f) //속도가 음수가 되면 하강하는 것이므로...
            {
                direction = 2; //방향을 하강으로 바꾼다.
            }
            else
            {
                jump_speed -= jump_gravity; //중력가속도에 의해 속도가 아래방향으로 증가(위쪽 방향으로는 감소)
            }
        }
        else if (direction == 2) // down 하강
        {
            MyPos.y -= jump_speed; //(하강)속도만큼 y위치 감소. (이 때 jump_speed는 음수값)
            if (isGrounded + superjump == 0) // 공중
            {
                if (jump_speed < 0.05f)
                    jump_speed += jump_gravity; //중력가속도에 의해 속도가 아래방향으로 증가
            }
            else  // 지면
            {
                direction = 1; // 땅에 닿았으니 다시 상승!
                if (isGrounded > superjump) // 그냥 땅에 닿았을때
                {
                    jump_speed = jump_speed_init; // 처음 공이 튈때 속도도 지정된 값으로 초기화해준다.
                }
                else // 슈퍼점프할떄
                {
                    jump_speed = jump_speed_init * 1.6f;
                }
            }
        }
        else if (direction == 3) // 우측 상승 (왼쪽 벽에서 벽타기)
        {
            isLeftwall = false;
            MyPos.y += jump_speed;
            MyPos.x += move_speed * 1.5f;
            if (jump_speed <= 0.0f)
            {
                direction = 2;
            }
            else
            {
                jump_speed -= jump_gravity;
            }
        }
        else if (direction == 4) // 좌측 상승 (오른쪽 벽에서 벽타기)
        {
            isRightwall = false;
            MyPos.y += jump_speed;
            MyPos.x -= move_speed * 2f;
            if (jump_speed <= 0.0f)
            {
                direction = 2;
            }
            else
            {
                jump_speed -= jump_gravity;
            }
        }

        gameObject.transform.position = MyPos; //다시 gameObject의 position에 넣어준다.
    }
}