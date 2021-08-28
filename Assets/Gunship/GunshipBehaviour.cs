using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshipBehaviour : MonoBehaviour
{
    [SerializeField] int globalHealth;
    [SerializeField] GameState gameState;
    [SerializeField] public int health;
    [SerializeField] int bulletAttack;
    [SerializeField] float timeBetweenAtk;
    [SerializeField] float timeBetweenMovementUpdate;
    [SerializeField] GameObject rocket;
    [SerializeField] AudioSource audioManagerOneShot;
    [SerializeField] AudioSource audioManagerGunshipHower;
    OneShotSoundsScript oneShotSounds;
    [SerializeField] Transform mycamera;
    [SerializeField] shootingscript shootingscript;
    
    public bool flinch = false;
    float timer3;
    float directionx;
    float directiony;
    float timer1;
    IEnumerator AttackRoutine()
    {
        while(true)
        {
            
            int counter = 15; //number of times it attacks
            yield return new WaitForSeconds(timeBetweenAtk);
            audioManagerOneShot.PlayOneShot(oneShotSounds.gunshipLockOn);
            //audioManager.PlayOneShot(gunshipLockOn);
            if(flinch==true) // stun time if rocket hits gunship
            {
                flinch = false;
                yield return new WaitForSeconds(timeBetweenAtk/8);
            }
            while (counter >= 0)
            {
                if (flinch == true) // stun time if rocket hits gunship
                {
                    flinch = false;
                    counter = 10;
                    yield return new WaitForSeconds(timeBetweenAtk/8);
                }
                counter = counter - 1;
                yield return new WaitForSeconds(0.1f);
                audioManagerOneShot.PlayOneShot(oneShotSounds.gunshipBullet);
                //audioManager.PlayOneShot(gunshipBullet);
                float chance = Random.value*100;
                if(chance>75)
                {
                    if (rocket.activeSelf == true)
                    {
                        audioManagerOneShot.PlayOneShot(oneShotSounds.rocketDeflected);
                        //audioManagerGunshipHower.PlayOneShot(rocketDeflected);
                        //audioManager.PlayOneShot(rocketDeflected);
                        rocket.SetActive(false);

                    }
                    else
                    {
                        int value = Mathf.RoundToInt(Random.Range(0, 5));
                        audioManagerOneShot.PlayOneShot(oneShotSounds.playerpain[value]);
                        gameState.PlayerHealth = gameState.PlayerHealth - bulletAttack;
                        float jitterx=Random.Range(-0.5f,0.5f);
                        float jittery = Random.Range(-0.5f, 0.5f);
                        mycamera.transform.position = new Vector3(0,jittery,-10);
                        
                    }
                }
                
            }
            audioManagerOneShot.PlayOneShot(oneShotSounds.gunshipLockOn);
            //audioManager.PlayOneShot(gunshipLockOn);
        }
        
    }

    private void OnEnable()
    {
        oneShotSounds = audioManagerOneShot.GetComponent<OneShotSoundsScript>();
        audioManagerGunshipHower.Play();
        //audioManager.PlayOneShot(gunshipAlert);

        audioManagerOneShot.PlayOneShot(oneShotSounds.gunshipAlert);
        health = globalHealth;
        StartCoroutine(AttackRoutine());
        timer1 = timeBetweenAtk;
        float x = Random.RandomRange(-5, 5);
        float y = Random.RandomRange(1.3f, 3);
        transform.SetPositionAndRotation(new Vector2(x, y), Quaternion.identity);
        timer3 = timeBetweenMovementUpdate;
        gameState.gunshipIsAlive = true;
    }

    private void OnDisable()
    {
        //audioManager.PlayOneShot(gunshipDestroyed);

        gameState.Score = gameState.Score + 3;
        audioManagerGunshipHower.Stop();
        gameState.gunshipIsAlive = false;
        gameState.PlayerHealth = 100;
        shootingscript.chaingun_ammo = shootingscript.chaingun_ammo + 100;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        oneShotSounds = audioManagerOneShot.GetComponent<OneShotSoundsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementUpdate();
        if(health<=0)
        {
            audioManagerOneShot.PlayOneShot(oneShotSounds.gunshipDestroyed);
            gameObject.SetActive(false);
        }

    }

    void MovementUpdate()
    {
        if(rocket.activeSelf==true)
        {
            //float gap_x = transform.position.x + rocket.transform.position.x;
            //float gap_y = transform.position.y + rocket.transform.position.y;
            float gunship_x = transform.position.x;
            float rocket_x = rocket.transform.position.x;
            float gunship_y = transform.position.y;
            float rocket_y = rocket.transform.position.y;

            if (gunship_x<rocket_x &&gunship_x>-8)
            {
                gunship_x = gunship_x - 0.05f;
            }
            if(gunship_x>rocket_x && gunship_x<8)
            {
                gunship_x = gunship_x + 0.05f;
            }
            if (gunship_y < rocket_y && gunship_y>4)
            {
                gunship_y = gunship_y - 0.05f;
            }
            if (gunship_y > rocket_y && gunship_y<0)
            {
                gunship_y = gunship_y + 0.05f;
            }
            float jitterx = Random.RandomRange(-0.025f, 0.025f);
            float jittery = Random.RandomRange(-0.025f, 0.025f);
            transform.position = new Vector3(gunship_x+jitterx, gunship_y+jittery, 0);
           
        }
        else
        {
            if (timer3 <= 0)
            {
                directionx = Random.RandomRange(-0.05f, 0.05f);
                directiony = Random.RandomRange(-0.02f, 0.02f);
                timer3 = 1f;
            }
            float jitterx = Random.RandomRange(-0.025f, 0.025f);
            float jittery = Random.RandomRange(-0.025f, 0.025f);

            float posx = transform.position.x;
            float posy = transform.position.y;
            if (posx > 7 && directionx > 0)
            {
                directionx = -1f * directionx;
            }
            if (posx < -7 && directionx < 0)
            {
                directionx = -1f * directionx;
            }
            if (posy > 3 && directiony > 0)
            {
                directiony = -1f * directiony;
            }
            if (posy < 1.3 && directiony < 0)
            {
                directiony = -1f * directiony;
            }

            transform.SetPositionAndRotation(new Vector2(transform.position.x + jitterx + directionx, transform.position.y + jittery + directiony), Quaternion.identity);
        }
        

        
    }
}
