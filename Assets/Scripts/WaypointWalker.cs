using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointWalker : MonoBehaviour {

	public float speed=1;
	public List<Vector3> waypointPositions;
	int currentWaypoint = 0;
	Vector3 targetPositionDelta;
	Vector3 moveDirection = Vector3.zero;
	SpriteController spriteController;
	bool lookRight = true;
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
}
