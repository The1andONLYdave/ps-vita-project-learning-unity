using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpriteController : MonoBehaviour {

	public List<Texture2D> animationStayRight;
	public List<Texture2D> animationStayLeft;
	public List<Texture2D> animationGoRight;
	public List<Texture2D> animationGoLeft;
	public List<Texture2D> animationJumpRight;
	public List<Texture2D> animationJumpLeft;
	public List<Texture2D> animationAttackRight;
	public List<Texture2D> animationAttackLeft;
	public float speed = 1;
	public AnimationType currentAnimationType = AnimationType.stayRight;
	public bool looping = true;
	private AnimationType oldAnimationType = AnimationType.stayRight;
	private float myTime =0;

	public enum AnimationType{
		stayRight,
		stayLeft,
		goRight,
		goLeft,
		jumpRight,
		jumpLeft,
		attackRight,
		attackLeft
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (oldAnimationType != currentAnimationType) {
			myTime=0;
			oldAnimationType=currentAnimationType;
		}
		switch (currentAnimationType) {
		
		case AnimationType.stayRight:
			SetTexture (animationStayRight);
			break;
		case AnimationType.stayLeft:
			SetTexture (animationStayLeft);
			break;
		case AnimationType.goRight:
			SetTexture (animationGoRight);
			break;
		case AnimationType.goLeft:
			SetTexture (animationGoLeft);
			break;
		case AnimationType.jumpRight:
			SetTexture (animationJumpRight);
			break;
		case AnimationType.jumpLeft:
			SetTexture (animationJumpLeft);
			break;
		case AnimationType.attackRight:
			SetTexture (animationAttackRight);
			break;
		case AnimationType.attackLeft:
			SetTexture (animationAttackLeft);
			break;

		}

	}

	void SetTexture(List<Texture2D> animationSprite){

		myTime+=Time.deltaTime;
		int index = (int)(myTime * speed);

		if (!looping) {
			if(index>=animationSprite.Count-1){
				index = animationSprite.Count -1;
			}		
		}
		index = index % animationSprite.Count;
		renderer.material.mainTexture = animationSprite [index];

	}
	public void SetAnimation(AnimationType animationType){
		currentAnimationType = animationType;
	}
}
