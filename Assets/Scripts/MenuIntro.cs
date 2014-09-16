using UnityEngine;
using System.Collections;

public class MenuIntro : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(10,10,100,50),"Start")){
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(10,70,100,50),"InAppPurchase")){
			Application.LoadLevel(2);
		}
	}
}
