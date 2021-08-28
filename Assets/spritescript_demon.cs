using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritescript_demon : MonoBehaviour
{
    [SerializeField] Sprite[] frameArray;
    private int currentFrame;
    private float timer;
    private SpriteRenderer mysprite;
    public bool IsAttacking = false;
    public bool IsMoving = true;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        mysprite = gameObject.GetComponent<SpriteRenderer>();
        
    }
    private void OnEnable()
    {
        StartCoroutine(MoveAttackRoutine());
    }
    IEnumerator MoveAttackRoutine()
    {
        Antlion antlion = GetComponent<Antlion>();
        float scalesize;
        while(true)
        {
            scalesize = 0;
            counter = 0;
            currentFrame = 0;
            while (IsAttacking == false)
            {
                scalesize = 5*(1 - (antlion.timer2 / antlion.TimeBetweenATK));
                yield return new WaitForSeconds(0.1f);
                currentFrame = (currentFrame + 1) % 4;
                mysprite.sprite = frameArray[currentFrame];
                transform.localScale = new Vector3(5+scalesize, 5+scalesize,5+scalesize);
            }
            currentFrame = 4;
            while (IsAttacking == true &&counter<4)
            {
                yield return new WaitForSeconds(0.1f);
                currentFrame = (currentFrame + 1) % 8;
                mysprite.sprite = frameArray[currentFrame];
                counter++;
            }
            IsAttacking = false;
            transform.localScale = new Vector3(5, 5, 1);
            
        }
        

    }
    // Update is called once per frame


    void Update()
    {
        
    }
}
