using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class CharacterBattle : MonoBehaviour
{
    public bool thisIsPlayer;//こいつがプレイヤーか否か

    public GameObject battleManagerObj;//BattleManagerのオブジェクト
    private BattleManger battleMangerSc;//BattlManagerのスクリプト

    Animator animor;//アニメーター

    public CharacterStatus characterStatus;//CharacterStatusのスクリプト

    public EnemyStatus enemyStatus;        //EnemyStatusのスクリプト

    private int currentHP;//現在のHP
    private int maxHP;//最大のHP

    public Slider slider;//スライダー

    private int Atk;//攻撃力

    public GameObject opponent;//相手のオブジェクト
    private CharacterBattle opponentCB;//相手のCharacterBattleスクリプト

    private bool Avoid = false; //プレイヤーが回避中か否か
    private bool EnemyAttack = false; //敵の攻撃判定
    private bool Action = false; //アクション判定

    private int skill; //スキルの種類（0 = Atk, 1 = Mgc）

    public GameObject BulletObj; //弾_playerのゲームオブジェクト
    Vector3 bulletpoint_player; //弾_playerの位置
    public GameObject BulletObj_Enemy; //弾_Enemyのゲームオブジェクト
    Vector3 bulletpoint_Enemy; //弾_bulletの位置

    public Color flashColor;//点滅時の色
    public Color defaultColor;//デフォルトのボタンの色
    public GameObject buttonObj;//アクションボタンのオブジェクト
    private Image buttonImg;//ボタンのカラーをいじるため
    private bool isFlashing = false;//true:点滅する false:点滅しない
    private bool flash;//点滅の切り替え
    private bool isPressed = false;//ボタンが押されたか否か
    private float countTime;//点滅切り替えのタイマー

    public GameObject ActionText; //テキスト

    private Text actionText; //テキストの内容

    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip atkSe;
    [SerializeField]
    AudioClip mgcSe;
    [SerializeField]
    AudioClip dmgSe;
    [SerializeField]
    AudioClip crtSe;
    [SerializeField]
    AudioClip escSe;

    //魔法攻撃スイッチ
    private bool mgcFlag = true;

  

    private void Awake()
    {
       
    }
    private void Start()
    {
        //色々取得
        if (!thisIsPlayer)
        {
            battleManagerObj = GameObject.FindGameObjectWithTag("BattleManager");
            slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>(); ;
            opponent = GameObject.FindGameObjectWithTag("Player");
            ActionText = GameObject.FindGameObjectWithTag("ActionText");
            
        }
        else 
        {
            opponent = GameObject.FindGameObjectWithTag("Enemy");
        }
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        battleMangerSc = battleManagerObj.gameObject.GetComponent<BattleManger>();
        animor = GetComponent<Animator>();
        actionText = ActionText.GetComponent<Text>();

        opponentCB = opponent.GetComponent<CharacterBattle>();




        //データベースからHPの取得
        maxHP = characterStatus.MaxHPOutput();
        currentHP = characterStatus.HPOutput();


        //スライダーをMaxの状態にする
        slider.value = 1;

        //データベースから攻撃力の取得
        Atk = characterStatus.AtkOutput();

        //buttonImg = buttonObj.GetComponent<Image>();

        actionText.text = "";

        if (!thisIsPlayer)
        {
            maxHP = enemyStatus.MaxHPOutput();
            currentHP = enemyStatus.HPOutput();
            Atk = enemyStatus.AtkOutput();
        }
    }


    private void Update()
    {
        
        //スライドバーの表示
        slider.value = (float)currentHP / (float)maxHP;

        //HPが0より小さくなったら死ぬ
        if (currentHP < 0 && !thisIsPlayer)
        {
            // animor.SetTrigger("Dead");
            SceneManager.UnloadScene("BattleScene");
        }
        if (currentHP < 0 && thisIsPlayer)
        {
            // animor.SetTrigger("Dead");
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");

        }




    }

    //攻撃ボタン
    public void AtkButtonOn()
    {
        battleMangerSc.ComandDone();
        skill = 0;
        StartCoroutine(TextManager("タイミングよく押せ！", 1.0f));
    }

    //キャラクターの攻撃アクション
    public void CharacterAction()
    {
        
        if (!thisIsPlayer)
        {
            skill = Random.Range(0,2);
            StartCoroutine(TextManager("避けろ！", 1.0f));

        }
        if(skill == 0)
        {
            animor.SetTrigger("Atk");
            soundManager.PlaySe(atkSe);
        }
        if(skill == 1 && mgcFlag)
        {
            if (thisIsPlayer)
            {
                Instantiate(BulletObj, transform.position + bulletpoint_player, Quaternion.identity,battleManagerObj.transform);
                
                Debug.Log("Playerの魔法攻撃！");
                soundManager.PlaySe(mgcSe);

            }
            if (!thisIsPlayer)
            {
                Instantiate(BulletObj_Enemy, transform.position + bulletpoint_Enemy, Quaternion.identity, battleManagerObj.transform);
                Debug.Log("Enemyの魔法攻撃！");
                soundManager.PlaySe(mgcSe);
            }
            mgcFlag = false;
        }
        
    }

    //攻撃アニメーションの終わり
    public void AnimationDone()
    {
        //BattleManagerにアニメーションの終わりを告げる
        if (thisIsPlayer)
        {
            battleMangerSc.StartCoroutine("playerDone");
            
            //ダメージの計算
            if (Action)
            {
                Debug.Log("クリティカル！");
                Atk *= 2;
                opponentCB.Damage(Atk);
                soundManager.PlaySe(crtSe);
            }
            else
            {
                opponentCB.Damage(Atk);
                soundManager.PlaySe(dmgSe);
            }
            
        }
        else
        {
            
            battleMangerSc.enemyDone();
        }
        mgcFlag |= true;
    }

    //魔法ボタン
    public void MgcButtonOn()
    {
        battleMangerSc.ComandDone();
        skill = 1;
        StartCoroutine(TextManager("タイミングよく押せ！", 1.0f));
    }

    //ダメージの計算
    public void Damage(int x)
    {

        currentHP -= x;
        if (thisIsPlayer)
        {
            characterStatus.HPInput(currentHP);
        }
        else
        {
            enemyStatus.HPInput(currentHP);
        }

    }


    //キャラクターのアクション
    public void CharacterAvoidAction()
    {
        if (!battleMangerSc.returnPlayerTurn())
        {
            soundManager.PlaySe(escSe);
            animor.SetTrigger("Avoid");
            Debug.Log("回避！");
        }
        else
        {
            StartCoroutine("ActionTiming");
            Debug.Log("アクション！");
        }

    }

    //アクションの受付（コルーチン）
    IEnumerator ActionTiming()
    {
        Action = true;
        //停止
        yield return new WaitForSeconds(1);
        Action = false;
    }

    //プレイヤーの回避判定True
    public void PlayerAvoidTrue()
    {
        Avoid = true;
    }

    //プレイヤーの回避判定False
    public void PlayerAvoidFalse()
    {
        Avoid = false;
    }

    //敵の攻撃判定True
    public void AttackTrue()
    {
        EnemyAttack = true;
        if(opponentCB.Avoid == false)
        {
            //ダメージの計算

            opponentCB.Damage(Atk);
            soundManager.PlaySe(dmgSe);
        }
        EnemyAttack = false;
    }

    //敵の攻撃判定False
    public void AttackFalse()
    {
        EnemyAttack = false;
    }

    IEnumerator TextManager(string Btext, float wait)
    {
        if (thisIsPlayer)
        {
            actionText.text = Btext;
            yield return new WaitForSeconds(wait);
            
        }
        if (!thisIsPlayer)
        {
            actionText.text = Btext;
            yield return new WaitForSeconds(wait);
            actionText.text = "";
        }

    }
}