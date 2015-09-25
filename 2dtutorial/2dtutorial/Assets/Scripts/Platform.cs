using UnityEngine;
using System.Collections;

public class Platform {

	public int startIndex;
	public int length;
	public int height;
	public bool isDanger;
	public GameObject addOn;

	public Platform (int myStart, int myLenght, int myHeight, bool myIsDanger)
	{
		startIndex = myStart;
		length = myLenght;
		height = myHeight;
		isDanger = myIsDanger;
	}

}
