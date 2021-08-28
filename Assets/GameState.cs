using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject gunship;
    public int PlayerHealth;
    public int Score;
    float AntlionRespawnTimer = 2;
    [SerializeField] GameObject antlion;
    [SerializeField] Text healthbox;
    [SerializeField] Text score;
    [SerializeField] Image antlionhealthbar;
    RectTransform myrect;
    Antlion antlionscript;
    [SerializeField] AudioClip antliondiesound;
    [SerializeField] GameObject crate;
    AudioSource myaudio;
    bool coroutinestarted;
    bool deadsoundplayed;
    public bool gunshipIsAlive = false;
    // Start is called before the first frame update
    void Start()
    {
        antlionscript = antlion.GetComponent<Antlion>();
        myrect = antlionhealthbar.GetComponent<RectTransform>();
        myaudio = this.GetComponent<AudioSource>();
        deadsoundplayed = true;
        coroutinestarted = false;
    }

    void UpdateAntlionHealthBar()
    {
        float x = (antlionscript.Health / antlionscript.GlobalHealth);
        if (x < 0)
            x = 0;
        myrect.anchoredPosition = new Vector3(x*50-50, -10, 0);
        myrect.sizeDelta = new Vector2(x * 100, 10.85f);

    }
    IEnumerator GunshipSpawner()
    {
        yield return new WaitForSeconds(30f);
        gunship.SetActive(true);
    }
    IEnumerator CrateSpawner()
    {
        //code execute
        coroutinestarted = true;
        yield return new WaitForSeconds(10f);
        crate.SetActive(true);
        coroutinestarted = false;
        //code execute
    }

    // Update is called once per frame
    void Update()
    {
        if(gunshipIsAlive==false)
        {
            StartCoroutine(GunshipSpawner());
            gunshipIsAlive = true;
        }

        if(crate.activeSelf==false && coroutinestarted==false)
        {
            StartCoroutine(CrateSpawner());
        }
        if(antlion.activeSelf==false &&gunship.activeSelf==false)
        {
            if(deadsoundplayed==false)
            {
                deadsoundplayed = true;
                myaudio.PlayOneShot(antliondiesound);
                Debug.Log("DEADEADAEDAEDAED");
            }
            AntlionRespawnTimer = AntlionRespawnTimer - Time.deltaTime;
            if(AntlionRespawnTimer<=0)
            {
                antlion.SetActive(true);
                AntlionRespawnTimer = 2;
            }
        }
        healthbox.text = PlayerHealth.ToString();
        score.text = Score.ToString();
        if(PlayerHealth<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        UpdateAntlionHealthBar();
    }
}
