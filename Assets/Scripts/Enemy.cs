using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed, RotationSpeed;
    public Transform[] Points;

    public int MaxHP;



    private Transform currentPoint;
    private int index, HP;
    private Vector3 direction;

    public float rotationDistanceToPoint;
    private ResourceManager rm;

    // Start is called before the first frame update
    void Start()
    {
        rm = FindObjectOfType<ResourceManager>();
        //HP = MaxHP;
        index = 0;
        currentPoint = Points[index];

        
        direction = currentPoint.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SetHP(int NewHP)
    {
        HP = MaxHP = NewHP;
        //HP = NewHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            HP -= other.GetComponent<Bullet>().Damage;
            Destroy(other.gameObject);

            if (HP <= 0)
            {
                rm.EnemyKill();
                Destroy(gameObject);
                
            }
        }
    }

    public int GetPoint()
    {
        return index;
    }
    // Update is called once per frame
    void Update()
    {

       /*  direction = currentPoint.position - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime * RotationSpeed, 0);
        transform.rotation = Quaternion.LookRotation(newDirection); */

        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * Speed);

        if(transform.position == currentPoint.position)
        {
            index++;

            if(index >= Points.Length)
            {
                Destroy(gameObject);
                rm.MissEnemy();

            } else {
                currentPoint = Points[index];
            }
            
        }

        if(Vector3.Distance(transform.position, currentPoint.position) < rotationDistanceToPoint)
        {
            direction = Points[index+1].position - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime * RotationSpeed, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        
    }
}
