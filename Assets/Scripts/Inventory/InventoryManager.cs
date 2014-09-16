using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	static public Inventory inventory;

	public bool create=false;
	public float money = 0;

	void Awake(){
				//if (create) {
				if (inventory==null) {
						inventory = (Inventory)ScriptableObject.CreateInstance (typeof(Inventory));
						inventory.money = money;
				} 
				else {
						money=inventory.money;
				}
		}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
