using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float Radius, FireDelay;
    public LayerMask EnemyLayer;
    public Transform BulletPrefab;

    public int Gamage;

    private float timeToFire;
    private Transform gun, enemy, firePoint;


    // Start is called before the first frame update
    void Start()
    {
        timeToFire = FireDelay;
        gun = transform.GetChild(0);
        firePoint = gun.GetChild(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToFire > 0)
            timeToFire -= Time.deltaTime;
        else if (enemy)
            Fire();

        if(enemy)
        {
            Vector3 lookAt = enemy.position;
            lookAt.y = gun.position.y; 
            gun.rotation = Quaternion.LookRotation(gun.position - lookAt);

            if(Vector3.Distance(transform.position, enemy.position) > Radius)
            enemy = null; //обнуляем врага вне радиуса
        } else if (enemy == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, EnemyLayer);

            if(colliders.Length > 0)
                enemy = colliders[0].transform;
        }
    }

    private void Fire()
    {
        Transform bullet = Instantiate(BulletPrefab, firePoint.position, Quaternion.identity);
        bullet.LookAt(enemy.GetChild(0));
        bullet.GetComponent<Bullet>().Damage = Gamage;

        timeToFire = FireDelay;
    }
}
