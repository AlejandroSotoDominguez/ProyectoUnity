using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed = 2f;
    public float maxSpeed = 5f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public bool muerte;
    public bool fin;
    public int vidas;
    public int modVidas;
    public Text countText;
    public Text winText;
    public Text vidasText;

	private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    private SpriteRenderer spr;

    private int count;

    // Use this for initialization
    void Start () {
		rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        count = 0;
        vidas = 3;
        SetCountText ();
        winText.text = "";
        vidasText.text = "Vidas: "+vidas;
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded){
            jump = true;
        }
 
        if(muerte){
            
            if (vidas >= 2){
                rb2d.position = transform.localPosition = new Vector3(-5.48f, -0.27997f);
                muerte = false;
                vidasText.text = "Vidas: " + (vidas=vidas-1);

            }else{
                SceneManager.LoadScene("Scenes/SampleScene");
            }
        }
    }

	void FixedUpdate () {
		float h = Input.GetAxis("Horizontal");
		rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if(h < 0){
            spr.flipX = true;
        }else if(h > 0){
            spr.flipX = false;
        }

        if (jump){
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag ("Pick Up")){
            other.gameObject.SetActive (false);
            count = count + 10;
            SetCountText ();
        }

        if (other.gameObject.CompareTag ("Puerta") && count == 100){
            winText.text = "Nivel completado!";
            this.gameObject.SetActive(false);
        }
    }

    void SetCountText (){
        countText.text = "Puntos: " + count.ToString ();
    }





}
