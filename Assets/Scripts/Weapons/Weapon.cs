using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon 
{
    public Transform firePoint;             //Точка создания пули
    public GameObject bulletPrefab;         //Пуля
    public AudioClip shotSound;             //Звук выстрела
    public float shotDelay;                 //Задержка между выстрелами (сек.)
    public float scatter;                   //Максимальная величина разброса (относительная, в градусах)
    public bool isAutomatic;                //Автоматическое
    public bool isShotgun;                  //Дробовик

    //Конструктор
    public Weapon(Transform firePoint, GameObject bulletPrefab, AudioClip shotSound, float shotDelay, float scatter, bool isAutomatic, bool isShotgun)
    {
        this.firePoint.position = firePoint.position;
        this.bulletPrefab = bulletPrefab;
        this.shotSound = shotSound;
        this.shotDelay = shotDelay;
        this.scatter = scatter;
        this.isAutomatic = isAutomatic;
        this.isShotgun = isShotgun;
    }

}
