using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float gravity = 6;
	public float speed = 10;
	public float jumpPower = 10;

	bool inputJump = false;
	float velocity = 0;
	Vector3 moveDirection = Vector3.zero;
	CharacterController characterController;
	SpriteController spriteController;
	bool lookRight = true;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();
		spriteController = GetComponent<SpriteController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		InputCheck();
		Move();
		SetAnimation();
	}

	void InputCheck(){
		velocity = Input.GetAxis ("Horizontal") * speed;

		if (velocity > 0) {
						lookRight = true;
				}
		if (velocity < 0) {
						lookRight = false;
				}

		if ((Input.GetKeyDown(KeyCode.Space))||(Input.GetKeyDown(KeyCode.JoystickButton0))) { //ps vita x?
			inputJump=true;
				}
		else{
			inputJump=false;	
		}
	}
	void Move(){
		if (characterController.isGrounded) {
			if(inputJump){
				moveDirection.y=jumpPower;
			}		
		}
		moveDirection.x = velocity;
		moveDirection.y -= (gravity*Time.deltaTime);
		characterController.Move (moveDirection * Time.deltaTime);
	}
	void SetAnimation(){

		if (velocity > 0) {
			spriteController.SetAnimation(SpriteController.AnimationType.goRight);
		}
		if (velocity < 0) {
			spriteController.SetAnimation(SpriteController.AnimationType.goLeft);
		}
		if (velocity == 0) {
			if (lookRight){
				spriteController.SetAnimation (SpriteController.AnimationType.stayRight);
			}
			else{ 
				spriteController.SetAnimation(SpriteController.AnimationType.stayLeft);
			}
		}
		if (!characterController.isGrounded) {
			if (lookRight){
				spriteController.SetAnimation (SpriteController.AnimationType.jumpRight);
			}
			else{ 
				spriteController.SetAnimation(SpriteController.AnimationType.jumpLeft);
			}
		}
	}

}
