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
        countText.text = "Count: " + score.ToString();
        if(score == 16 && stop == false)
        {
            winText.text = "Stand by... \n  Teleporting shortly...";
            StartCoroutine("sceneChange");
            stop = true;
            winText.text = "";
        }
        if(score >= 32)
        {
            winText.text = "Congratulations! \n   You Win!";
        }
    }
    void setLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    IEnumerator sceneChange() 
    {
        Debug.Log("hi");
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(3);
        
        this.transform.position = new Vector3(0,.5f,49);
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            Instantiate(ParticleFX,other.transform.position,new Quaternion(0,0,0,0)).GetComponent<ParticleSystem>().Play();
            // ParticleFX.GetComponent<ParticleSystem>().Play();
            other.gameObject.SetActive(false);
            score++;
        }
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
        // Debug.Log(Data.score);
        rigid.AddForce(movement*speed);
        setCountText();
        setLivesText();
        if(lives <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }
    
}
