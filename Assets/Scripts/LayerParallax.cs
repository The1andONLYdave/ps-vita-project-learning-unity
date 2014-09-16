using UnityEngine;
using System.Collections;

public class LayerParallax : MonoBehaviour {

	public CharacterController cc;
	public float speedFactor=1;

	Vector3 velocity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		velocity.x = cc.velocity.x * speedFactor;
		transform.Translate (velocity * Time.deltaTime);
	}
}
