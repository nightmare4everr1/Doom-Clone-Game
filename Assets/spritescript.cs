using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritescript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] frameArray;
    private int currentFrame;
    private float timer;
    private float framerate=0.1f;
    private SpriteRenderer mysprite;
    public bool ReloadAnimation = false;
    public int counter=6;

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

        Vector3 gunOffset = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
        gunOffset = Camera.main.ScreenToWorldPoint(gunOffset);
        gunOffset.z = 0;
        gunOffset.y = gunOffset.y*0.05f -4f;
        if(gunOffset.y>-3.8f)
        {
            gunOffset.y = -3.8f;
        }
        gunOffset.x = gunOffset.x * 0.15f;
        mysprite.transform.position = gunOffset;

        if (ReloadAnimation == true)
        {
            mysprite.sprite = frameArray[3];
        }

        timer = timer + Time.deltaTime;
        if(timer>0.1f && counter<5)
        {
            timer = 0;
            counter = counter + 1;
            currentFrame = (currentFrame + 1) % frameArray.Length;
            mysprite.sprite = frameArray[currentFrame];
            return;
        }        
    }
}
