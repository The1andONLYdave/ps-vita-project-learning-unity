using UnityEngine;
using System.Collections;

public class MenuIntro : MonoBehaviour {
    private float ctrlWidth  = 240;
    private float ctrlHeight = 100;

	void OnGUI(){
		if(GUI.Button(new Rect((Screen.width - ctrlWidth) / 2 ,  0, ctrlWidth, ctrlHeight),"Start")){
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect((Screen.width - ctrlWidth) / 2, 120, ctrlWidth, ctrlHeight), "InAppPurchase")){
			Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect((Screen.width - ctrlWidth) / 2, 240, ctrlWidth, ctrlHeight),"Credit")){
			Application.LoadLevel(3);
		}
	}
}
