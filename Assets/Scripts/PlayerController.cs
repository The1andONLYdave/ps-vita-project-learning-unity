using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float gravity = 6;
	public float speed = 10;
	public float jumpPower = 10;
	public SendDamageCollider sendDamageColliderR;
	public SendDamageCollider sendDamageColliderL;
	bool inputJump = false;
	float velocity = 0;
	Vector3 moveDirection = Vector3.zero;
	CharacterController characterController;
	SpriteController spriteController;
	bool lookRight = true;
	bool isSlaying = false;
	float slayingTime = 0.2F;
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
		Fight();

	}

	void InputCheck(){
		velocity = Input.GetAxis ("Horizontal") * speed;

		if (velocity > 0) {
						lookRight = true;
				}
		if (velocity < 0) {
						lookRight = false;
				}

		if ((Input.GetButtonDown ("Jump"))||(Input.GetKeyDown(KeyCode.JoystickButton0))) { //ps vita x?
			inputJump=true;
				}
		else{
			inputJump=false;	
		}
		if (((Input.GetButtonDown ("Fire1")) || (Input.GetKeyDown (KeyCode.JoystickButton2)))&&!isSlaying) { //ps vita square
			isSlaying=true;		
			StartCoroutine(ResetIsSlaying());
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
	void Fight(){
		if (isSlaying) {
			if(lookRight){
				sendDamageColliderR.attacking=true;
				sendDamageColliderL.attacking=false;
			}	
			else{
				sendDamageColliderL.attacking=true;
				sendDamageColliderR.attacking=false;
			}
		}
		else{
			sendDamageColliderL.attacking=false;
			sendDamageColliderR.attacking=false;
		}
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
		if (isSlaying) {
			if(lookRight){
				spriteController.SetAnimation (SpriteController.AnimationType.attackRight);
			}
			else{
				spriteController.SetAnimation (SpriteController.AnimationType.attackLeft);
			}
		}
	}
	IEnumerator ResetIsSlaying(){
		yield return new WaitForSeconds(slayingTime);
		isSlaying = false;
	}
}
