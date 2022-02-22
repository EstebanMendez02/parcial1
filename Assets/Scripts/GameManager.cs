using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PlayerScript player;
    EnemyScript enemy;
    void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyScript>();
    }

    public PlayerScript GetPlayer => player;
    public EnemyScript GetEnemy => enemy;
}
