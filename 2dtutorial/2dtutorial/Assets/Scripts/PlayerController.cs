using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float maxSpeed = 4;
	public float jumpForce = 550;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	[HideInInspector]
	public bool lookingRight = true;

	public GameObject laserPrefab;
	public Transform spawnPoint;
	public float laserSpeed = 500;
	
	public AudioSource jumpSource;
	public AudioSource shootSource;

	private Rigidbody2D rb2d;
	private Animator anim;
	private bool isGrounded = false;
	private bool jump = false;
	private bool isAttacking = false;
    private bool _isMoveLeft = false;
    private bool _isMoveRight = false;
    private int moveSpeed;


    // Use this for initialization
    void Start () {

		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		JumpSound jumpSound = anim.GetBehaviour<JumpSound>();
		jumpSound.audioSource = jumpSource;

		anim.GetBehaviour<ShootSound>().audioSource = shootSource;
	}

    public void moveLeft() {

        _isMoveLeft = true;
       


    }
    public void moveLeftRelease()
    {

        _isMoveLeft = false;


    }

    public void moveRight()
    {

        _isMoveRight = true;



    }
    public void moveRightRelease()
    {

        _isMoveRight = false;


    }

    public void jumper(){
        if (isGrounded)
            jump = true;
    }

    public void weapon(){
        if (!isAttacking)
            isAttacking = true;
    }

    // Update is called once per frame
    void Update () {
	}


	void FixedUpdate()
	{
		//float hor = Input.GetAxis("Horizontal");
        if (_isMoveLeft)
        {
            moveSpeed = -1;
        }
        else if (_isMoveRight)
        {
            moveSpeed = 1;
        }
        else moveSpeed = 0;

		anim.SetFloat("Speed",Mathf.Abs(moveSpeed));

		rb2d.velocity = new Vector2(moveSpeed * maxSpeed, rb2d.velocity.y);

		isGrounded = Physics2D.OverlapCircle ( groundCheck.position, 0.15F, whatIsGround);

		anim.SetBool("IsGrounded",isGrounded);

		if ((_isMoveRight == true && !lookingRight)||(_isMoveLeft ==true  && lookingRight))
			Flip ();

		if(jump)
		{
			rb2d.AddForce(new Vector2(0,jumpForce));
			jump = false;
		}

		if (isAttacking)
		{
			anim.SetTrigger("Attack");
			GameObject laser = (GameObject) Instantiate (laserPrefab, spawnPoint.position,Quaternion.identity);

			if (lookingRight)
				laser.GetComponent<Rigidbody2D>().AddForce(Vector3.right * laserSpeed);
			else
				laser.GetComponent<Rigidbody2D>().AddForce(Vector3.left * laserSpeed);

			isAttacking = false;
		}

    }

	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}
}







