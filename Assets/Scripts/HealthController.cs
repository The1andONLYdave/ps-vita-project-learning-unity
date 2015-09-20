using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public float currentHealth = 5;
	public float damageEffectPause = 0.2F;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ApplyDamage(float damage){

		if(currentHealth>0){
			currentHealth -= damage;
	
			if (currentHealth < 0) {
				currentHealth = 0;		
			}
	
			if (currentHealth ==0){
				//gameover
				RestartScene();
			}
			else{
				StartCoroutine(DamageEffect()); 
			}
 		}
	}

	void RestartScene(){
		Application.LoadLevel (Application.loadedLevel);
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
	}
}
