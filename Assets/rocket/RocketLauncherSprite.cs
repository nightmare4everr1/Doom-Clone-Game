using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherSprite : MonoBehaviour
{
    private SpriteRenderer mysprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        mysprite = gameObject.GetComponent<SpriteRenderer>();
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
    }
}
