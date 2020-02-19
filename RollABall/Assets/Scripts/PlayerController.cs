using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private int score;
    private int lives;
    public float speed;
    private Rigidbody rigid;
    public Text countText;
    public Text livesText;
    public Text winText; 
    public GameObject ParticleFX;
    private bool stop;
    
    // Start is called before the first frame update\
    void Start()
    {
        lives = 3;
        rigid = GetComponent<Rigidbody>();
        winText.text = "";
        stop = false;
    }
    void setCountText()
    {
        //changes level if all items on first platform are picked up
        countText.text = "Count: " + score.ToString();
        //runs coroutine ones to teleport player after first level is clear
        if(score == 16 && stop == false)
        {
            winText.text = "Stand by... \n  Teleporting shortly...";
            StartCoroutine("sceneChange");
            stop = true;
            
        }
        
        // sets win text if all items are picked up
        if(score >= 32)
        {
            winText.text = "Congratulations! \n   You Win!";
        }
    }
    void setLivesText()
    {
        //Sets lives text object
        livesText.text = "Lives: " + lives.ToString();
    }
    //sets delay before teleportation
    IEnumerator sceneChange() 
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(3);
        winText.text = "";
        this.transform.position = new Vector3(0,.5f,49);
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            //creates clone of the particle system when pickup is destroyed and plays particle effect
            Instantiate(ParticleFX,other.transform.position,new Quaternion(0,0,0,0)).GetComponent<ParticleSystem>().Play();
            other.gameObject.SetActive(false);
            score++;
        }
        //decreases lives when wall is hit and resets position
        if(other.gameObject.CompareTag("Wall"))
        {
            lives--;
            this.transform.position = Vector3.zero;
            this.rigid.velocity = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);
        rigid.AddForce(movement*speed);
        setCountText();
        setLivesText();
        //changes scenes when lives reach 0
        if(lives <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }
    
}
