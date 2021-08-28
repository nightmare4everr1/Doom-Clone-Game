using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Antlion : MonoBehaviour
{
    public float GlobalHealth;
    public float Health;
    public float TimeBetweenATK;
    public float TimeBetweenMovement;
    public int Damage;
    public float timer1;
    public float timer2;
    public float timer3 = 1f;
    float directionx;
    float directiony;
    [SerializeField]GameState gameState;
    public bool AntlionAlive;
    AudioSource myaudio;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myaudio = this.GetComponent<AudioSource>();
        AntlionAlive = true;
        timer1 = TimeBetweenMovement;
        timer2 = TimeBetweenATK;
    }
    private void OnDisable()
    {
        gameState.PlayerHealth = 100;
        AntlionAlive = false;
    }

    private void OnEnable()
    {

        float x = Random.RandomRange(-5, 5);
        float y = Random.RandomRange(1.3f, 3);
        transform.SetPositionAndRotation(new Vector2(x, y), Quaternion.identity);
        TimeBetweenATK = TimeBetweenATK * 0.975f;
        TimeBetweenMovement = TimeBetweenMovement * 0.975f;
        GlobalHealth = GlobalHealth + 5;
        Health = GlobalHealth;
        timer1 = TimeBetweenMovement;
        timer2 = TimeBetweenATK;
        transform.localScale = new Vector3(5, 5, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
        timer1 = timer1 - Time.deltaTime;
        timer2 = timer2 - Time.deltaTime;
        timer3 = timer3 - Time.deltaTime;
        if(timer3<=0)
        {
            directionx = Random.RandomRange(-0.075f, 0.075f);
            directiony = Random.RandomRange(-0.02f, 0.02f);
            timer3 = 1f;
        }
        float jitterx = Random.RandomRange(-0.05f, 0.05f);
        float jittery = Random.RandomRange(-0.05f, 0.05f);

        float posx = transform.position.x;
        float posy = transform.position.y;
        if(posx>7 &&directionx>0)
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
        if (posy < 1.3 && directiony <0)
        {
            directiony = -1f * directiony;
        }

        transform.SetPositionAndRotation(new Vector2(transform.position.x + jitterx+directionx, transform.position.y + jittery +directiony), Quaternion.identity);
        
        if(Health<=0)
        {
            gameState.Score = gameState.Score + 1;
            float x = Random.RandomRange(-5, 5);
            float y = Random.RandomRange(1.3f, 3);
            this.gameObject.SetActive(false);
        }
       

        if(timer1<=0)
        {
            //float x = Random.RandomRange(-5, 5);
            //float y = Random.RandomRange(0, 3);
            //transform.SetPositionAndRotation(new Vector2(x,y), Quaternion.identity);
            timer1 = TimeBetweenMovement;
        }
        if(timer2<=0)
        {
            gameState.PlayerHealth = gameState.PlayerHealth - Damage;
            myaudio.Play();
            timer2 = TimeBetweenATK;
            this.GetComponent<spritescript_demon>().IsAttacking = true;
        }
        
    }
}
