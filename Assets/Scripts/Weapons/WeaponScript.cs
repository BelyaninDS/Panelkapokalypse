using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shotDelay;
    public float scatter;
    public int ammo;
    public int reloadTime;
    public bool isAutomatic;

    [HideInInspector]
    public bool isEquipped;

    private float shotTime;
    private float noAmmoTime;
    private float angle;
    private int currentAmmo;


    // Start is called before the first frame update
    void Start()
    {
        firePoint.SetParent(gameObject.transform);
        shotTime = Time.time;
        currentAmmo = ammo;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Расчет угла поворота оружия относительно горизонтальной плоскости
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - firePoint.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg;

        Debug.Log(currentAmmo);

        //Триггер события перезарядки
        if ((currentAmmo >= 0  &&  Input.GetButtonDown("Reload")) || (currentAmmo == 0 && Input.GetButtonDown("Fire1")))
        {
            noAmmoTime = Time.time;
            currentAmmo = -1;
        }
        //Перезарядка
        if (currentAmmo < 0 && Time.time > noAmmoTime + reloadTime)
            currentAmmo = ammo;


        //Стрельба
        if (Time.time > shotTime + shotDelay && isEquipped && currentAmmo > 0)
        {
            //Для автомата
            if (isAutomatic)
            {
                if (Input.GetButton("Fire1"))
                {
                    Shoot();
                }
            }
            else
            {
                //Для полуавтомата
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
            }
        }
    }


    //Выстрел
    void Shoot()
    {
        --currentAmmo;
        shotTime = Time.time;

        angle += Random.Range(-scatter/2, scatter/2);
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));

        angle *= Mathf.Deg2Rad;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = 10f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
    }


    //Вспомогательные функции для системы подбора оружия
    public void SetEquipped(bool value)
    {
        if (value)
            isEquipped = true;
        else
            isEquipped = false;
    }

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
