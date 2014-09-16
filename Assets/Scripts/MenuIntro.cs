using UnityEngine;
using System.Collections;

public class MenuIntro : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(100,100,300,100),"Start")){
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(100,300,300,100),"InAppPurchase")){
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(500,100,300,100),"Credit")){
			Application.LoadLevel(3);
		}
	}
}
