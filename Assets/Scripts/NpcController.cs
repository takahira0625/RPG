using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(Flowchart))]
public class NcpController : MonoBehaviour
{

    [SerializeField]
    string message = "";
    private bool isTalking = false;
    GameObject playerObj;
    PlayerScript player;
    Flowchart flowChart;
    public GameObject[] enemys; //“G‚½‚¿
    private EnemyScript[] enemySc;
    private Fade fade;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj = GameObject.FindGameObjectWithTag("Player");
        fade = GameObject.Find("Panel").GetComponent<Fade>();
        player = playerObj.GetComponent<PlayerScript>();
        flowChart = GetComponent<Flowchart>();
        enemySc = new EnemyScript[enemys.Length];

        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i] = enemys[i].gameObject.GetComponent<EnemyScript>();
        }
        StartCoroutine(Talk());
        Debug.Log("‰ï˜bŠJŽn");
    }

    
    /*
    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("‚Ô‚Â‚©‚Á‚½");
            StartCoroutine(Talk());
            
        }
    }
    */
    IEnumerator Talk()
    {
        player.EventTrue();
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
        fade.FadeInOut();
        this.gameObject.SetActive(false);
        isTalking = false;
        player.EventFalse();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i].EventActive();
        }

    }


}