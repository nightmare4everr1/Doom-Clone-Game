using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootingscript : MonoBehaviour
{
    public Camera camera;
    RaycastHit hit;
    Ray ray;
    float weapon_firedelay;
    float weapon_switchdelay;
    int weapon_damage;
    int weapon_ammo;
    int weapon_reserve;
    bool WeaponIsReady;
    bool shotgunisreloading;
    [SerializeField] int magnum_damage;
    [SerializeField] int magnum_ammo;
    [SerializeField] int magnum_reserve;
    [SerializeField] float magnum_firedelay;
    [SerializeField] float magnum_switchdelay;
    [SerializeField] int shotgun_damage;
    [SerializeField] int shotgun_ammo;
    [SerializeField] int shotgun_reserve;
    [SerializeField] float shotgun_firedelay;
    [SerializeField] float shotgun_switchdelay;
    [SerializeField] int rpg_damage;
    [SerializeField] int rpg_ammo;
    [SerializeField] int rpg_reserve;
    [SerializeField] float rpg_firedelay;
    [SerializeField] float rpg_switchdelay;
    [SerializeField] float rpg_speed;
    [SerializeField] int chaingun_damage;
    [SerializeField] public int chaingun_ammo;
    [SerializeField] int chaingun_reserve;
    [SerializeField] public float chaingun_firedelay;
    [SerializeField] float chaingun_switchdelay;

    [SerializeField] Text weapontext;
    [SerializeField] Text ammotext;
    [SerializeField] Text reservetext;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject rocketLauncher;
    [SerializeField] Transform mycamera;
    spritescript shotgun_script;
    RocketLauncherSprite rocketLauncherSprite;
    [SerializeField]ChainGunSprite chainGunSprite;
    [SerializeField] AudioSource audioManagerOneShot;
    OneShotSoundsScript oneShotSounds;
    public int currentweapon = 0;
    float timer1;
    float timer2;
    bool weaponalreadyreloading;
    void Start()
    {
        StartCoroutine(PlayerCameraResetCoroutine());
        oneShotSounds = audioManagerOneShot.GetComponent<OneShotSoundsScript>();
        rocketLauncherSprite = rocketLauncher.GetComponent<RocketLauncherSprite>();
        shotgun_script = shotgun.GetComponent<spritescript>();
        EquipShotgun();
        WeaponIsReady = true;
        shotgunisreloading = false;
        weaponalreadyreloading = false;
    }

    void EquipRPG()
    {
        weapon_damage = rpg_damage;
        weapon_firedelay = rpg_firedelay;
        weapon_switchdelay = rpg_switchdelay;
        weapon_ammo = rpg_ammo;
        weapon_reserve = rpg_reserve;
        currentweapon = 3;
        timer1 = 0;
        timer2 = rpg_switchdelay;
        weapontext.text = "RPG";
        ammotext.text = weapon_ammo.ToString();
        reservetext.text = weapon_reserve.ToString();
        shotgun_script.gameObject.SetActive(false);
        rocketLauncherSprite.gameObject.SetActive(true);
        chainGunSprite.gameObject.SetActive(false);
    }

    void EquipChainGun()
    {
        weapon_damage = chaingun_damage;
        weapon_firedelay = chaingun_firedelay;
        weapon_switchdelay = chaingun_switchdelay;
        weapon_ammo = chaingun_ammo;
        weapon_reserve = chaingun_reserve;
        currentweapon = 4;
        timer1 = 0;
        timer2 = chaingun_switchdelay;
        weapontext.text = "Chain Gun";
        ammotext.text = weapon_ammo.ToString();
        reservetext.text = weapon_reserve.ToString();
        shotgun_script.gameObject.SetActive(false);
        rocketLauncherSprite.gameObject.SetActive(false);
        chainGunSprite.gameObject.SetActive(true);


    }
    void EquipMagnum()
    {
        weapon_damage = magnum_damage;
        weapon_firedelay = magnum_firedelay;
        weapon_switchdelay =magnum_switchdelay ;
        weapon_ammo = magnum_ammo;
        weapon_reserve = magnum_reserve;
        currentweapon = 1;
        timer1 = 0;
        timer2 = magnum_switchdelay;
        weapontext.text = "Magnum";
        ammotext.text = weapon_ammo.ToString();
        reservetext.text = weapon_reserve.ToString();
        shotgun_script.gameObject.SetActive(false) ;
        rocketLauncherSprite.gameObject.SetActive(false);
        chainGunSprite.gameObject.SetActive(false);

    }
    void EquipShotgun()
    {
        weapon_damage = shotgun_damage;
        weapon_firedelay = shotgun_firedelay;
        weapon_switchdelay = shotgun_switchdelay;
        weapon_ammo = shotgun_ammo;
        weapon_reserve = shotgun_reserve;
        currentweapon = 2;
        timer1 = 0;
        timer2 = shotgun_switchdelay;
        weapontext.text = "shotgun";
        ammotext.text = weapon_ammo.ToString();
        reservetext.text = weapon_reserve.ToString();
        shotgun_script.gameObject.SetActive(true);
        rocketLauncherSprite.gameObject.SetActive(false);
        chainGunSprite.gameObject.SetActive(false);
        WeaponIsReady = true;
    }
    IEnumerator TestCoroutine()
    {
        //code execute
        yield return new WaitForSeconds(2f);
        //code execute

        while(true)
        {
            yield return null;
        }
    }

    IEnumerator PlayerCameraResetCoroutine()
    {
        while(true)
        {
            float posx = mycamera.transform.position.x;
            float posy = mycamera.transform.position.y;
            float resetspeed = 0.005f;

            yield return new WaitForSeconds(0.5f);
            if (posx>0)
            {
                posx = posx - resetspeed;
            }
            if(posx<0)
            {
                posx = posx + resetspeed;
            }
            if(posy>0)
            {
                posy = posy - resetspeed;
            }
            if(posy<0)
            {
                posy=posy+resetspeed;
            }

            if (Mathf.Abs(posx) < resetspeed)
                posx = 0;
            if (Mathf.Abs(posy) < resetspeed)
                posy = 0;

            mycamera.transform.position = new Vector3(0, posy,-10);
        }
        //code execute
       
        //code execute
    }

    IEnumerator ReloadMagnumCoroutine()
    {
        //code execute
        weaponalreadyreloading = true;
        audioManagerOneShot.PlayOneShot(oneShotSounds.magnumreload);
        //myaudio.PlayOneShot(magnumreload);
        yield return new WaitForSeconds(2f);
        if(weaponalreadyreloading==true)
        {
            int x = 6 - magnum_ammo;
            if (x > magnum_reserve)
            {
                magnum_ammo = magnum_ammo + magnum_reserve;
                magnum_reserve = 0;
            }
            else
            {
                magnum_ammo = 6;
                magnum_reserve = magnum_reserve - x;
            }
            UpdateWeaponStats(magnum_ammo, magnum_reserve);
            WeaponIsReady = true;
            weaponalreadyreloading = false;
        }
        
        //code execute

    }

    void UpdateWeaponStats(int ammo,int reserve)
    {
        ammotext.text = ammo.ToString();
        reservetext.text =reserve.ToString();
        weapon_ammo = ammo;
        weapon_reserve = reserve;

    }

    IEnumerator ReloadShotgunCoroutine()
    {
        //code execute
        weaponalreadyreloading = true;
        WeaponIsReady = false;
        shotgunisreloading = true;
        shotgun_script.ReloadAnimation = true;
        while (shotgun_ammo < 8 && shotgun_reserve != 0 && shotgunisreloading == true)
        {
            yield return new WaitForSeconds(0.5f);
            WeaponIsReady = true;
            if (shotgunisreloading == true)
            {
                shotgun_ammo = shotgun_ammo + 1;
                shotgun_reserve = shotgun_reserve - 1;
                audioManagerOneShot.PlayOneShot(oneShotSounds.shotgunreload);
                //myaudio.PlayOneShot(shotgunreload);
                UpdateWeaponStats(shotgun_ammo, shotgun_reserve);
            }
            
            
        }

        shotgunisreloading = false;
        WeaponIsReady = true;
        weaponalreadyreloading = false;
        shotgun_script.ReloadAnimation = false;
        //code execute

    }

    

    private void Update()
    {
        if(timer1>0)
        {
            timer1 = timer1 - Time.deltaTime;
        }
        if (timer2 > 0)
        {
            timer2 = timer2 - Time.deltaTime;
        }
        
        if(Input.GetButtonUp("Fire1") && currentweapon==4)
        {
            chainGunSprite.attackAnimation = false;
        }

        if(Input.GetButton("Fire1") && timer1 <= 0 && timer2 <= 0 && WeaponIsReady == true)
        {
            if (weapon_ammo <= 0)
            {
                audioManagerOneShot.PlayOneShot(oneShotSounds.emptyclip);
                //myaudio.PlayOneShot(emptyclip);
                ReloadWeapon();
                return;
            }
            if (currentweapon==4)
            {
                audioManagerOneShot.PlayOneShot(oneShotSounds.chaingunfire);
                chainGunSprite.attackAnimation = true;
                timer1 = chaingun_firedelay;
                chaingun_ammo = chaingun_ammo - 1;
                UpdateWeaponStats(chaingun_ammo, chaingun_reserve);
            }
            ray = camera.ScreenPointToRay(Input.mousePosition);
            //Debug.Log(ray);
            if (Physics.Raycast(ray, out hit) && currentweapon == 4)
            {
                if (hit.transform.tag == "Antlion")
                {
                    Antlion objectHit = hit.transform.gameObject.GetComponent<Antlion>();
                    objectHit.Health = objectHit.Health - weapon_damage;
                    //myaudio.PlayOneShot(antlionpain);
                    audioManagerOneShot.PlayOneShot(oneShotSounds.antlionpain);
                    Debug.Log("Successfully SHOT");

                    objectHit.timer1 = objectHit.timer1;
                    if (objectHit.timer1 > objectHit.TimeBetweenMovement)
                    {
                        objectHit.timer1 = objectHit.TimeBetweenMovement;
                    }

                    objectHit.timer2 = objectHit.timer1;
                    if (objectHit.timer2 > objectHit.TimeBetweenATK)
                    {
                        objectHit.timer2 = objectHit.TimeBetweenATK;
                    }

                    // Do something with the object that was hit by the raycast.
                }
                if (hit.transform.tag == "Gunship")
                {
                    GunshipBehaviour objectHit = hit.transform.gameObject.GetComponent<GunshipBehaviour>();
                    objectHit.health = objectHit.health - weapon_damage;
                    //myaudio.PlayOneShot(antlionpain);
                    audioManagerOneShot.PlayOneShot(oneShotSounds.rocketDeflected);
                    Debug.Log("Successfully SHOT");

                    // Do something with the object that was hit by the raycast.
                }

                if (hit.transform.tag == "Crate")
                {
                    CrateLoot();

                }
            }
        }

        if (Input.GetButtonDown("Fire1") &&timer1<=0 && timer2<=0 &&WeaponIsReady==true)
        {
            
            Debug.Log(weapon_ammo);
            if(weapon_ammo<=0)
            {
                audioManagerOneShot.PlayOneShot(oneShotSounds.emptyclip);
                //myaudio.PlayOneShot(emptyclip);
                ReloadWeapon();
                return;
            }
            
            ammotext.text = weapon_ammo.ToString();
            if (currentweapon == 1)
            {
                magnum_ammo = magnum_ammo - 1;
                timer1 = magnum_firedelay;
                //myaudio.Play();

                audioManagerOneShot.PlayOneShot(oneShotSounds.mangumfire);
                UpdateWeaponStats(magnum_ammo, magnum_reserve);

            }
            if (currentweapon == 2)
            {
                shotgunisreloading = false;
                shotgun_script.ReloadAnimation = false;
                timer1 = shotgun_firedelay;
                shotgun_ammo = shotgun_ammo - 1;
                UpdateWeaponStats(shotgun_ammo,shotgun_reserve);
               // myaudio.PlayOneShot(shotgunfire);
                audioManagerOneShot.PlayOneShot(oneShotSounds.shotgunfire);
                shotgun_script.counter=0;
            }
            if(currentweapon==3)
            {

                if(rocket.activeSelf==false)
                {
                    Debug.Log("ROCKET LAUCH");
                    rpg_ammo = rpg_ammo - 1;
                    rocket.SetActive(true);
                    UpdateWeaponStats(rpg_ammo, rpg_reserve);
                }
                
            }

            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) &&currentweapon!=3)
            {
                if (hit.transform.tag=="Antlion")
                {
                    Antlion objectHit = hit.transform.gameObject.GetComponent<Antlion>();
                    objectHit.Health = objectHit.Health - weapon_damage;
                    //myaudio.PlayOneShot(antlionpain);
                    audioManagerOneShot.PlayOneShot(oneShotSounds.antlionpain);
                    Debug.Log("Successfully SHOT");

                    objectHit.timer1 = objectHit.timer1 + 0.5f;
                    if (objectHit.timer1 > objectHit.TimeBetweenMovement)
                    {
                        objectHit.timer1 = objectHit.TimeBetweenMovement;
                    }
                        
                    objectHit.timer2 = objectHit.timer1 + 0.2f;
                    if (objectHit.timer2 > objectHit.TimeBetweenATK)
                    {
                        objectHit.timer2 = objectHit.TimeBetweenATK;
                    }
                        
                    // Do something with the object that was hit by the raycast.
                }
                if (hit.transform.tag == "Gunship")
                {
                    GunshipBehaviour objectHit = hit.transform.gameObject.GetComponent<GunshipBehaviour>();
                    objectHit.health = objectHit.health - weapon_damage;
                    //myaudio.PlayOneShot(antlionpain);
                    audioManagerOneShot.PlayOneShot(oneShotSounds.rocketDeflected);
                    Debug.Log("Successfully SHOT");

                    // Do something with the object that was hit by the raycast.
                }

                if (hit.transform.tag=="Crate")
                {
                    CrateLoot();
                 
                }
               

            }
        }

        void CrateLoot()
        {
            hit.transform.gameObject.SetActive(false);
            //myaudio.PlayOneShot(pickcratesound);
            audioManagerOneShot.PlayOneShot(oneShotSounds.pickcratesound);
            int x=Random.Range(0, 15);
            if (x >= 0 && x < 5)
            {
                magnum_reserve = magnum_reserve + 9;
                if (currentweapon == 1)
                {
                    reservetext.text = magnum_reserve.ToString();
                }

            }
            else if (x >= 5 && x < 10)
            {
                shotgun_reserve = shotgun_reserve + 12;
                if (currentweapon == 2)
                {
                    reservetext.text = shotgun_reserve.ToString();
                }
            }
            else if (x >= 10)
            {
                rpg_ammo = rpg_ammo + 3;
                if(currentweapon==3)
                {
                    ammotext.text = rpg_ammo.ToString();
                }

            }
        }


        void ReloadWeapon()
        {

            if (weaponalreadyreloading == true)
                return;

            if (currentweapon==1)
            {
                WeaponIsReady = false;
                StartCoroutine(ReloadMagnumCoroutine());
                
            }
            if(currentweapon==2)
            {

                StartCoroutine(ReloadShotgunCoroutine());
            }

        }

        if(Input.GetButtonDown("Weapon1"))
        {
            EquipMagnum();
            CancelReload();
        };
        if (Input.GetButtonDown("Weapon2"))
        {
            CancelReload();
            EquipShotgun();
        }
        if (Input.GetButtonDown("Weapon3"))
        {
            CancelReload();
            EquipRPG();
        }
        if (Input.GetButtonDown("Weapon4"))
        {
            CancelReload();
            EquipChainGun();
        }
        if (Input.GetButtonDown("Reload"))
        {
            ReloadWeapon();
        }

    }

    void CancelReload()
    {
         weaponalreadyreloading = false;
         shotgunisreloading = false;
         WeaponIsReady = true;
  
    }

}
