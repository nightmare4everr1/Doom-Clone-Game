using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]float timeDelayToReachTarget;
    [SerializeField] float timeDelayExplodeByItself;
    [SerializeField] int rocket_damage;
    [SerializeField] GunshipBehaviour gunship;
    
    [SerializeField] AudioClip rocketTravel;
    
    [SerializeField]AudioSource audioManagerOneShot;
    [SerializeField] AudioSource audioManagerRocketTravel;
    [SerializeField] shootingscript shootingscript;
    OneShotSoundsScript oneShotSounds;
    public float timer1;
    public float timer2;
    bool canExplode = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        
    }

    IEnumerator RocketTravelRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        audioManagerRocketTravel.clip = rocketTravel;
        audioManagerRocketTravel.Play();
    }

    private void OnEnable()
    {
        oneShotSounds = audioManagerOneShot.GetComponent<OneShotSoundsScript>();
        Vector3 rocketLaunchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        rocketLaunchPosition = Camera.main.ScreenToWorldPoint(rocketLaunchPosition);
        rocketLaunchPosition.z = 0;
        rocketLaunchPosition.x = rocketLaunchPosition.x * 0.15f;
        rocketLaunchPosition.y = rocketLaunchPosition.y * 0.25f-2;
        transform.SetPositionAndRotation(new Vector2(rocketLaunchPosition.x, rocketLaunchPosition.y), Quaternion.identity);
        //audioManager.PlayOneShot(rocketFire);

        audioManagerOneShot.PlayOneShot(oneShotSounds.rocketFire);
        timer1 = timeDelayToReachTarget;
        StartCoroutine(RocketTravelRoutine());
        timer2 = timeDelayExplodeByItself;
        canExplode = false;
        
    }
    private void OnDisable()
    {
        
        audioManagerRocketTravel.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(shootingscript.currentweapon==3)
        {
            Vector3 AimPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            AimPosition = Camera.main.ScreenToWorldPoint(AimPosition);
            AimPosition.z = 0;

            Vector3 RockerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (AimPosition.x > RockerPosition.x)
            {
                RockerPosition.x = RockerPosition.x + 0.075f;
            }
            if (AimPosition.x < RockerPosition.x)
            {
                RockerPosition.x = RockerPosition.x - 0.075f;
            }
            if (AimPosition.y > RockerPosition.y)
            {
                RockerPosition.y = RockerPosition.y + 0.075f;
            }
            if (AimPosition.y < RockerPosition.y)
            {
                RockerPosition.y = RockerPosition.y - 0.075f;
            }

            transform.position = RockerPosition;
        }
        


        timer1 = timer1 - Time.deltaTime;
        timer2 = timer2 - Time.deltaTime;
        float scalesize = 4.5f * (1 - (timer2 / timeDelayExplodeByItself));
        transform.localScale = new Vector3(5-scalesize,5- scalesize, 1);
        if (timer1<=0)
        {
            canExplode = true;
        }
        if(timer2<=0)
        {
            gameObject.SetActive(false);
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (canExplode == true)
        {

            if (collision.gameObject.tag == "Gunship")
            {
                GunshipBehaviour gunship = collision.transform.gameObject.GetComponent<GunshipBehaviour>();
                gunship.health = gunship.health - rocket_damage;
                gameObject.SetActive(false);
                gunship.flinch = true;
                //audioManager.PlayOneShot(rocketExplode);

                audioManagerOneShot.PlayOneShot(oneShotSounds.rocketExplode);
                if (gunship.health > 0)
                    //audioManager.PlayOneShot(gunshipPain);
                    audioManagerOneShot.PlayOneShot(oneShotSounds.gunshipPain);
            }
            if (collision.gameObject.tag == "Antlion")
            {
                Antlion antlion = collision.transform.gameObject.GetComponent<Antlion>();
                antlion.Health = antlion.Health - rocket_damage;
                gameObject.SetActive(false);
                //audioManager.PlayOneShot(rocketExplode);
                audioManagerOneShot.PlayOneShot(oneShotSounds.rocketExplode);
            }

        }
    }
   
}
