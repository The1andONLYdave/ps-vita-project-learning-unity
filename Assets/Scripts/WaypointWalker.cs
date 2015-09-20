using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointWalker : MonoBehaviour {

	public float speed=1;
	public int currentHealth=1;
	public List<Vector3> waypointPositions;
	public float damageEffectPause = 0.2F;
	public string tag = "Player";
	public int damageValue = 1;

	public GameObject deathPrefabRight;
	public GameObject deathPrefabLeft;

	int currentWaypoint = 0;
	Vector3 targetPositionDelta;
	Vector3 moveDirection = Vector3.zero;
	SpriteController spriteController;
	bool lookRight = true;
	bool isHit = false;
	// Use this for initialization
	void Start () {
		spriteController = GetComponent<SpriteController> ();

	}
	
	// Update is called once per frame
	void Update () {
		WaypoinWalk();
		Move();
		SetAnimation();
	}
	void WaypoinWalk(){
		Vector3 targetPosition = waypointPositions [currentWaypoint];
		targetPositionDelta = targetPosition - transform.position;

		if (targetPositionDelta.sqrMagnitude <= 1) {
						currentWaypoint++;
						if (currentWaypoint >= waypointPositions.Count) {
								currentWaypoint = 0;
						}
		} 
		else {
			if(targetPositionDelta.x>0){
				lookRight=true;
			}
			else{
				lookRight=false;
			}
		}
	}

	void Move(){
		moveDirection=targetPositionDelta.normalized*speed;
		transform.Translate (moveDirection*Time.deltaTime,Space.World);
	}

	void SetAnimation(){
		if (lookRight){
			spriteController.SetAnimation (SpriteController.AnimationType.goRight);
		}
		else{ 
			spriteController.SetAnimation(SpriteController.AnimationType.goLeft);
		}
	}
	void ApplyDamage(int damage){
				if (!isHit) {
						
						isHit=true;
						currentHealth -= damage;

						if (currentHealth <= 0) {
								currentHealth = 0;		
								Die ();
						} else {
								StartCoroutine (DamageEffect ()); 
						}
				}

	}

	IEnumerator DamageEffect(){
		GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(damageEffectPause);
		GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(damageEffectPause);
		GetComponent<Renderer>().enabled = false;
		yield return new WaitForSeconds(damageEffectPause);
		GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(damageEffectPause);
		GetComponent<Renderer>().enabled = true;
		isHit = false;
	}

	void Die(){
		if (lookRight) {
			Destroy(Instantiate (deathPrefabRight,transform.position,Quaternion.identity),5);		
		} 
		else {
			Destroy(Instantiate (deathPrefabLeft,transform.position,Quaternion.identity),5);		
		}

		Destroy (gameObject);
	}
	
	void OnTriggerEnter(Collider other){
		if (!isHit) {
						if (other.gameObject.tag == tag) {
								//only when object have applydamage, else no error because !requirereceiver
								other.gameObject.SendMessage ("ApplyDamage", damageValue, SendMessageOptions.DontRequireReceiver);
						}
		}
	}
}
