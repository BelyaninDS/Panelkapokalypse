using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioClip shotSound;
    public float shotDelay;
    public float scatter;
    public int magRoundsCount;
    public int reloadTime;

    public bool isAutomatic;
    public bool isShotgun;

    [HideInInspector]
    public bool isEquipped;                //Флаг проверки, экипирован ли текущий объект кем-либо
    [HideInInspector]
    public bool holdByEnemy;               //Флаг проверки, держит ли оружие противник
    [HideInInspector]
    public int currentAmmo;                //Текущий остаток боезапаса в магазине

    private AudioSource shotSoundHandler;
    private float shotTime;
    private float noAmmoTime;
    private float angle;
    public Weapon weapon;

    private 

    // Start is called before the first frame update
    void Start()
    {
        weapon = new Weapon(firePoint, bulletPrefab, shotDelay, scatter, reloadTime, magRoundsCount);

        weapon.firePoint.SetParent(gameObject.transform);

        shotSoundHandler = GetComponent<AudioSource>();
        shotSoundHandler.clip = shotSound;

        shotTime = Time.time;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Расчет угла поворота оружия относительно горизонтальной плоскости
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - firePoint.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg;    //Градусы


        //Если оружие держит НЕ противник (Бот)
        if (!holdByEnemy && isEquipped)
        {
            //Триггер события перезарядки
            if ((weapon.currentAmmo >= 0 && Input.GetButtonDown("Reload")) || (weapon.currentAmmo == 0 && Input.GetButtonDown("Fire1")))
                weapon.Reload();
                
            //Стрельба
            //Для автомата
            if (isAutomatic)
            {
                if (Input.GetButton("Fire1"))       
                {
                    weapon.Shoot(angle);
                    shotSoundHandler.Play();
                }
                else
                {
                    //Для полуавтомата
                    if (Input.GetButtonDown("Fire1"))
                    {
                        weapon.Shoot(angle);
                        shotSoundHandler.Play();
                    }
                }
                
            }
        }
    }

    //Подбор оружия
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && gameObject.GetComponent<WeaponScript>().isEquipped == false && Input.GetButtonDown("Interact"))
        {
            other.GetComponentInChildren<WeaponScript>().SetEquipped(false);
            Instantiate(other.GetComponentInChildren<WeaponScript>().gameObject, transform.position, Quaternion.identity);
            other.GetComponentInChildren<WeaponScript>().DestroyWeapon();

            //Создаем новое оружие в холдере, даем статус "экипировано"
            other.GetComponentInChildren<WeaponHolder>().weaponPrefab = gameObject;
            gameObject.GetComponent<WeaponScript>().SetEquipped(true);
            other.GetComponentInChildren<WeaponHolder>().UpdateWeapon();

            Destroy(gameObject);
        }
    }

        //Вспомогательные функции для системы подбора оружия
        public void SetEquipped(bool value)
    {
        isEquipped = value;
    }

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
