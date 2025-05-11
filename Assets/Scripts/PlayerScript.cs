using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class PlayerScript : MonoBehaviour
{
    public FloatingJoystick inputMove; //�ړ��pJoyStick
    float moveSpeed = 70.0f; //�ړ����鑬�x
    Rigidbody2D playerRb; //Player��Rigidbody

    public GameObject controllerObj;//SceneManager�̃I�u�W�F�N�g
    private MainScene MainSceneSc;//SceneManager�̃X�N���v�g
    public GameObject JoyStickObj; //�W���C�X�e�B�b�N

    private float axisX;
    private float axisY;
    private int enemyNumber;
    private bool eventFlag = false;
    private bool stickFlag = false;
    private float axisX_save;
    private float axisY_save;
    Flowchart flowChart;
    public GameObject[] enemys; //�G����
    private EnemyScript[] enemySc;
    [SerializeField]
    string message = "";
    private bool isTalking = false;


    void OnEnable()
    {
        var ab = JoyStickObj.GetComponent<FloatingJoystick>();
        ab.OnPointerUp(null);
        Debug.Log("�V�[���J�n");
    }
    // Start is called before the first frame update

    void Awake()
    {
        playerRb = this.gameObject.GetComponent<Rigidbody2D>();
        MainSceneSc = controllerObj.gameObject.GetComponent<MainScene>();
        flowChart = GetComponent<Flowchart>();
        enemySc = new EnemyScript[enemys.Length];

        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i] = enemys[i].gameObject.GetComponent<EnemyScript>();
        }
        
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (eventFlag == false)
        {
            //�X�e�B�b�N�ł̈ړ�
            axisX = inputMove.Horizontal; //���������̌���
            axisY = inputMove.Vertical;   //���������̌���

            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector3(axisX, axisY) * moveSpeed);
            
            
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if(eventFlag == false)
        {
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                
                enemyNumber = 0;
                PlayerPrefs.SetInt("enemy", enemyNumber);
                Debug.Log("��������");
                MainSceneSc.SubButton();
                
                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("Enemy1"))
            {
                enemyNumber = 1;
                PlayerPrefs.SetInt("enemy", enemyNumber);
                Debug.Log("��������");
              
                MainSceneSc.SubButton();
               
                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("Enemy2"))
            {
                enemyNumber = 2;
                PlayerPrefs.SetInt("enemy", enemyNumber);
                Debug.Log("��������");
               
                MainSceneSc.SubButton();
                
                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("MoveMap1"))
            {
                
                Debug.Log("��������");
                SceneManager.LoadScene("MapScene2");

            }
            else if (other.gameObject.CompareTag("MapEvent1"))
            {

                Debug.Log("��������");
                StartCoroutine(Talk("event1"));

            }
            else if (other.gameObject.CompareTag("MapEvent2"))
            {

                Debug.Log("��������");
                StartCoroutine(Talk("event2"));

            }
        }
    }

    public void ResetAxis()
    {
        axisX = 0.0f; //���������̌���
        axisY = 0.0f;   //���������̌���
    }

    public void StickFlag()
    {
        stickFlag = true;
        inputMove.HandleRange = 0;
        axisX_save = axisX;
        axisY_save = axisY;
        
    }

    public void CompareAxis()
    {
        if(axisX != axisX_save || axisY != axisY_save)
        {
            stickFlag = false;
            inputMove.HandleRange = 1;
        }
    }

    public void EventTrue()
    {
        playerRb.velocity = Vector2.zero;
        moveSpeed = 0;
        eventFlag = true;
    }

    public void EventFalse()
    {
        moveSpeed = 100;
        eventFlag = false;
    }

    IEnumerator Talk(string word)
    {
        message = word;
        EventTrue();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i].EventNotActive();
        }
        if (isTalking)
        {
            yield break;
        }

        isTalking = true;


        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        
        isTalking = false;
        EventFalse();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i].EventActive();
        }
        if(message == "event2")
        {
            SceneManager.LoadScene("EndingScene");
        }

    }
}
