using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Enemy EnemyPrefab;
    public float DelayToSpawn;
    public Transform[] Points;

    public int EnemyHP;


    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = DelayToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Enemy enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            enemy.Points = Points;
            enemy.SetHP(EnemyHP);
            EnemyHP += 10;


            timer = DelayToSpawn;
        }
    }
}
