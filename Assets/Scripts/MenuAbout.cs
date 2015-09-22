using UnityEngine;
using System.Collections;

public class MenuAbout : MonoBehaviour {
    private float ctrlWidth = 240;
    private float ctrlHeight = 100;

    void OnGUI(){
        if (GUI.Button(new Rect((Screen.width - ctrlWidth) / 2, 0, ctrlWidth, ctrlHeight), "Start"))
        {
            Application.LoadLevel(1);
        }
        if (GUI.Button(new Rect((Screen.width - ctrlWidth) / 2, 120, ctrlWidth, ctrlHeight), "Back"))
        {
            Application.LoadLevel(0);
        }
	}
}
