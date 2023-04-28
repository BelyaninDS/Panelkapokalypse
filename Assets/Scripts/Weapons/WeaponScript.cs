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
    public int ammo;
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
    


    // Start is called before the first frame update
    void Start()
    {
        firePoint.SetParent(gameObject.transform);

        shotSoundHandler = GetComponent<AudioSource>();
        shotSoundHandler.clip = shotSound;

        shotTime = Time.time;
        currentAmmo = ammo;
    }


    // Update is called once per frame
    void Update()
    {
        //Расчет угла поворота оружия относительно горизонтальной плоскости
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - firePoint.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg;    //Градусы

        //Если оружие держит НЕ противник (Бот)
        if (!holdByEnemy)
        {
            //Триггер события перезарядки
            if ((currentAmmo >= 0 && Input.GetButtonDown("Reload")) || (currentAmmo == 0 && Input.GetButtonDown("Fire1")))
            {
                noAmmoTime = Time.time;
                currentAmmo = -1;
            }
            //Перезарядка
            if (currentAmmo < 0 && Time.time > noAmmoTime + reloadTime)
                currentAmmo = ammo;


            //Стрельба
            if ((Time.time > shotTime + shotDelay || currentAmmo == ammo) && isEquipped && currentAmmo > 0)
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
    }


    //Выстрел
    void Shoot()
    {
        //Для дробовика
        if (isShotgun)
        {
            float currentAngle = angle - scatter/2;    //Градусы

            GameObject[] bulletInstances = new GameObject[6];
            --currentAmmo;
            shotTime = Time.time;
            shotSoundHandler.Play();
            
                     
            for (int i = 0; i < 6; i++)
            {     
                currentAngle = angle + Random.Range(-scatter / 2, scatter / 2);
                bulletInstances[i] = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, currentAngle));               

                currentAngle *= Mathf.Deg2Rad;   //Радианы
                bulletInstances[i].GetComponent<Rigidbody2D>().velocity = 15f * new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));
                bulletInstances[i].GetComponent<AudioSource>().clip = shotSound;
            }
        }
        //Остальное оружие
        else
        {
            --currentAmmo;
            shotTime = Time.time;
            shotSoundHandler.Play();

            angle += Random.Range(-scatter / 2, scatter / 2);         
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));

            angle *= Mathf.Deg2Rad;
            bulletInstance.GetComponent<Rigidbody2D>().velocity = 15f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
            bulletInstance.GetComponent<AudioSource>().clip = shotSound;
        }
    }

    //Подбор оружия
    /*void OnTriggerStay2D(Collider2D other)
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
    }*/

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
