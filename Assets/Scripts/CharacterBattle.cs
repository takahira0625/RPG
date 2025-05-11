using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class CharacterBattle : MonoBehaviour
{
    public bool thisIsPlayer;//�������v���C���[���ۂ�

    public GameObject battleManagerObj;//BattleManager�̃I�u�W�F�N�g
    private BattleManger battleMangerSc;//BattlManager�̃X�N���v�g

    Animator animor;//�A�j���[�^�[

    public CharacterStatus characterStatus;//CharacterStatus�̃X�N���v�g

    public EnemyStatus enemyStatus;        //EnemyStatus�̃X�N���v�g

    private int currentHP;//���݂�HP
    private int maxHP;//�ő��HP

    public Slider slider;//�X���C�_�[

    private int Atk;//�U����

    public GameObject opponent;//����̃I�u�W�F�N�g
    private CharacterBattle opponentCB;//�����CharacterBattle�X�N���v�g

    private bool Avoid = false; //�v���C���[����𒆂��ۂ�
    private bool EnemyAttack = false; //�G�̍U������
    private bool Action = false; //�A�N�V��������

    private int skill; //�X�L���̎�ށi0 = Atk, 1 = Mgc�j

    public GameObject BulletObj; //�e_player�̃Q�[���I�u�W�F�N�g
    Vector3 bulletpoint_player; //�e_player�̈ʒu
    public GameObject BulletObj_Enemy; //�e_Enemy�̃Q�[���I�u�W�F�N�g
    Vector3 bulletpoint_Enemy; //�e_bullet�̈ʒu

    public Color flashColor;//�_�Ŏ��̐F
    public Color defaultColor;//�f�t�H���g�̃{�^���̐F
    public GameObject buttonObj;//�A�N�V�����{�^���̃I�u�W�F�N�g
    private Image buttonImg;//�{�^���̃J���[�������邽��
    private bool isFlashing = false;//true:�_�ł��� false:�_�ł��Ȃ�
    private bool flash;//�_�ł̐؂�ւ�
    private bool isPressed = false;//�{�^���������ꂽ���ۂ�
    private float countTime;//�_�Ő؂�ւ��̃^�C�}�[

    public GameObject ActionText; //�e�L�X�g

    private Text actionText; //�e�L�X�g�̓��e

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

    //���@�U���X�C�b�`
    private bool mgcFlag = true;

  

    private void Awake()
    {
       
    }
    private void Start()
    {
        //�F�X�擾
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




        //�f�[�^�x�[�X����HP�̎擾
        maxHP = characterStatus.MaxHPOutput();
        currentHP = characterStatus.HPOutput();


        //�X���C�_�[��Max�̏�Ԃɂ���
        slider.value = 1;

        //�f�[�^�x�[�X����U���͂̎擾
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
        
        //�X���C�h�o�[�̕\��
        slider.value = (float)currentHP / (float)maxHP;

        //HP��0��菬�����Ȃ����玀��
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

    //�U���{�^��
    public void AtkButtonOn()
    {
        battleMangerSc.ComandDone();
        skill = 0;
        StartCoroutine(TextManager("�^�C�~���O�悭�����I", 1.0f));
    }

    //�L�����N�^�[�̍U���A�N�V����
    public void CharacterAction()
    {
        
        if (!thisIsPlayer)
        {
            skill = Random.Range(0,2);
            StartCoroutine(TextManager("������I", 1.0f));

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
                
                Debug.Log("Player�̖��@�U���I");
                soundManager.PlaySe(mgcSe);

            }
            if (!thisIsPlayer)
            {
                Instantiate(BulletObj_Enemy, transform.position + bulletpoint_Enemy, Quaternion.identity, battleManagerObj.transform);
                Debug.Log("Enemy�̖��@�U���I");
                soundManager.PlaySe(mgcSe);
            }
            mgcFlag = false;
        }
        
    }

    //�U���A�j���[�V�����̏I���
    public void AnimationDone()
    {
        //BattleManager�ɃA�j���[�V�����̏I����������
        if (thisIsPlayer)
        {
            battleMangerSc.StartCoroutine("playerDone");
            
            //�_���[�W�̌v�Z
            if (Action)
            {
                Debug.Log("�N���e�B�J���I");
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

    //���@�{�^��
    public void MgcButtonOn()
    {
        battleMangerSc.ComandDone();
        skill = 1;
        StartCoroutine(TextManager("�^�C�~���O�悭�����I", 1.0f));
    }

    //�_���[�W�̌v�Z
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


    //�L�����N�^�[�̃A�N�V����
    public void CharacterAvoidAction()
    {
        if (!battleMangerSc.returnPlayerTurn())
        {
            soundManager.PlaySe(escSe);
            animor.SetTrigger("Avoid");
            Debug.Log("����I");
        }
        else
        {
            StartCoroutine("ActionTiming");
            Debug.Log("�A�N�V�����I");
        }

    }

    //�A�N�V�����̎�t�i�R���[�`���j
    IEnumerator ActionTiming()
    {
        Action = true;
        //��~
        yield return new WaitForSeconds(1);
        Action = false;
    }

    //�v���C���[�̉�𔻒�True
    public void PlayerAvoidTrue()
    {
        Avoid = true;
    }

    //�v���C���[�̉�𔻒�False
    public void PlayerAvoidFalse()
    {
        Avoid = false;
    }

    //�G�̍U������True
    public void AttackTrue()
    {
        EnemyAttack = true;
        if(opponentCB.Avoid == false)
        {
            //�_���[�W�̌v�Z

            opponentCB.Damage(Atk);
            soundManager.PlaySe(dmgSe);
        }
        EnemyAttack = false;
    }

    //�G�̍U������False
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