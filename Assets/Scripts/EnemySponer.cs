using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySponer : MonoBehaviour
{
    private int enemyNumber;
    public GameObject enemy0;
    public GameObject enemy1;
    public GameObject enemy2;
    // Start is called before the first frame update
    void Awake()
    {
        enemyNumber = PlayerPrefs.GetInt("enemy");
        if(enemyNumber  == 0)
        {
            Instantiate(enemy0, transform.position, Quaternion.identity,this.transform);
        }
        else if (enemyNumber == 1)
        {
            Instantiate(enemy1, transform.position, Quaternion.identity, this.transform);
        }
        else if (enemyNumber == 2)
        {
            Instantiate(enemy2, transform.position, Quaternion.identity, this.transform);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


    
