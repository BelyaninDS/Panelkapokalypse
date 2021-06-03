using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon
{
    public Transform firePoint;             //Точка создания пули    
    public GameObject bulletPrefab;
    public float shotDelay;                 //Задержка между выстрелами (сек.)
    public float scatter;                   //Максимальная величина разброса (относительная, в градусах)
    public float reloadTime;
    public int magRoundsCount;

    public int isEquipped;

    public int currentAmmo;

    private float outOfAmmoTime;
    
    

    //Конструктор
    public Weapon(Transform firePoint, GameObject bulletPrefab, float shotDelay, float scatter, float reloadTime, int magRoundsCount)
    {
        this.firePoint.position = firePoint.position;
        this.bulletPrefab = bulletPrefab;
        this.shotDelay = shotDelay;
        this.scatter = scatter;
        this.reloadTime = reloadTime;
        this.magRoundsCount = magRoundsCount;
        this.currentAmmo = magRoundsCount;
    }

    //Перезарядка
    public void Reload()
    {
        //Фиксируем время срабатывания триггера перезарядки
        {
            outOfAmmoTime = Time.time;
            currentAmmo = -1;
        }
        
        if (currentAmmo < 0 && Time.time > outOfAmmoTime + reloadTime)
            currentAmmo = magRoundsCount;
    }

    //Выстрел одной пулей
    public void Shoot(float angle)
    {     
        --currentAmmo;
        float shotTime = Time.time;

        angle += Random.Range(-scatter / 2, scatter / 2);
        GameObject bulletInstance = GameObject.Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));
        angle *= Mathf.Deg2Rad;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = 10f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

        angle *= Mathf.Deg2Rad;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = 10f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
    }

    //Выстрел дробью
    void MultiShot(float angle)
    {
        float currentAngle = angle;                                     //Градусы
        GameObject[] bulletInstances = new GameObject[6];
        --currentAmmo;

        for (int i = 0; i < 6; i++)
        {
            currentAngle += Random.Range(-scatter / 2, scatter / 2);    //Градусы
            currentAngle = currentAngle + scatter / (i + 1 / 6);
            bulletInstances[i] = GameObject.Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, currentAngle));

            currentAngle *= Mathf.Deg2Rad;   //Радианы
            bulletInstances[i].GetComponent<Rigidbody2D>().velocity = 10f * new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0f);
        }
    }
}
