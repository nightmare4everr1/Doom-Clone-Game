using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainGunSprite : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] attackSprite;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] shootingscript shootingscript;
    private int currentFrame;
    private float timer;
    private float framerate = 0.1f;
    private SpriteRenderer mysprite;
    public bool attackAnimation = false;
    public int counter = 6;

    private void Awake()
    {
        mysprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 gunOffset = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        gunOffset = Camera.main.ScreenToWorldPoint(gunOffset);
        gunOffset.z = 0;
        gunOffset.y = gunOffset.y * 0.05f - 4f;
        if (gunOffset.y > -3.8f)
        {
            gunOffset.y = -3.8f;
        }
        gunOffset.x = gunOffset.x * 0.15f;
        mysprite.transform.position = gunOffset;

        

        timer = timer + Time.deltaTime;
        if (timer > shootingscript.chaingun_firedelay &&attackAnimation==true)
        {
            timer = 0;
            currentFrame = (currentFrame + 1) % attackSprite.Length;
            mysprite.sprite = attackSprite[currentFrame];
        }
        if(attackAnimation==false)
        {
            mysprite.sprite = defaultSprite;
        }
    }
}
