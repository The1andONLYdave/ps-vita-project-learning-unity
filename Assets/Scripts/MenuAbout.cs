using UnityEngine;
using System.Collections;

public class MenuAbout : MonoBehaviour {

	void OnGUI(){
		if(GUI.Button(new Rect(100,100,300,100),"Start")){
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(100,300,300,100),"Back")){
			Application.LoadLevel(0);
		}
	}
}
