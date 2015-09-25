using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public string seed;
	public bool useRandomSeed;

	public int quantity = 10;
	public int minLength = 3;
	public int maxLength = 5;
	public int minHeight = 0;
	public int maxHeight = 5;
	[Range(0,1)]
	public float complexity = 0.5F;

	public GameObject platformTile;
	public GameObject platformGround;
	public GameObject player;
	public GameObject levelExit;

	[Tooltip("The size must be lower than 'Quantity'.")]
	public List<GameObject> addOnObjects;

	private System.Random pseudoRandom;

	private List<Platform> platforms = new List<Platform>();

	private GameObject levelGo;
	// Use this for initialization
	void Start () {
		//Platform p = new Platform(1,5,2,true);
		GenerateLevel();
	}
	
	public void GenerateLevel()
	{
		//Zufallsgenerator initiieren
		InitRandomObject();
		//Die Platformen erstellen (wo beginnt jedePlattform,...
		CreateDefaultPlatforms();
		//IsDanger festlegen
		SetIsDanger();
		//AddOn-Objekte zuweisen
		//Level erzeugen
		SetAddOnObjects();
		//Level bauen
		BuildLevel();
	}

	public void BuildLevel()
	{

		if (levelGo != null)
			Destroy(levelGo);

		levelGo = new GameObject("Level");
		//Plattformen erstellen
		PlacePlatforms();
		//Spieler platzieren
		PlacePlayer();
		//Levelausgang platzieren
		PlaceLevelExit();
	}

	void PlacePlatforms()
	{
		foreach(Platform current in platforms)
		{
			for(int i = current.startIndex; i < current.startIndex + current.length; i++)
			{
				Vector3 pos = new Vector3(i, current.height,0);
				if (!(i == current.startIndex  + current.length - 1 && current.isDanger))
				{
					GameObject curTile = (GameObject)Instantiate(platformTile,pos, Quaternion.identity);
					curTile.transform.parent = levelGo.transform;

					if(i == current.startIndex)
						AddCollider(ref curTile, current);
				}


			}

			PlaceAddOn (current);

			PlaceGroundTiles(current);
		}
	}

	void PlaceAddOn (Platform current)
	{
		if (current.addOn != null)
		{
			Vector3 addOnPos = new Vector3( current.startIndex +1 , current.height + 1, 0);
			GameObject curAddOn = (GameObject)Instantiate(current.addOn,addOnPos,Quaternion.identity);
			curAddOn.transform.parent = levelGo.transform;
		}
	}

	void AddCollider(ref GameObject go, Platform current)
	{
		BoxCollider2D coll = go.AddComponent<BoxCollider2D>();

		Vector2 size;
		size.x = current.length - (current.isDanger ? 1:0);
		size.y = current.isDanger ? 1: current.height + 5;

		coll.size = size;

		Vector2 offset;

		offset.x = size.x/2 - 0.5F;
		offset.y = current.isDanger ? 0 : -size.y/2 + 0.5F;

		coll.offset = offset;

	}

	void PlacePlayer()
	{
		Vector3 playerPos = new Vector3(0,platforms[0].height + 1, 0);
		player.transform.position = playerPos;
	}

	void PlaceLevelExit()
	{
		Vector3 finishPos = new Vector3 (platforms[platforms.Count - 1].startIndex + platforms[platforms.Count - 1].length, 
		                                 platforms[platforms.Count - 1].height,
		                                 0);
		levelExit.transform.position =  finishPos;
	}

	void PlaceGroundTiles(Platform current)
	{
		Vector3 tilePos = new Vector3(0,0,0);

		if(!current.isDanger)
		{
			for(int i = current.startIndex; i < current.startIndex + current.length; i++)
			{
				for (int c = current.height -1; c > -5; c--)
				{
					tilePos.x = i;
					tilePos.y = c;
					GameObject curTile = (GameObject)Instantiate(platformGround,tilePos, Quaternion.identity);
					curTile.transform.parent = levelGo.transform;
				}
			}
		}
	}

	void InitRandomObject()
	{
		if (useRandomSeed)
			seed = Time.time.ToString();

		pseudoRandom = new System.Random(seed.GetHashCode());

	}

	void CreateDefaultPlatforms()
	{
		int currentStartIndex = 0;
		platforms.Clear();

		int height = GetRandomNumber(minHeight, maxHeight  + 1);

		for (int i = 0; i < quantity; i++)
		{
			int heightDirection = GetRandomNumber (0,3);
			switch(heightDirection)
			{
				case 0:
					height --;
					break;
				case 2:
					height ++;	
					break;
			}

			height = Mathf.Max (height,minHeight);
			height = Mathf.Min (height, maxHeight);

			Platform current = new Platform(currentStartIndex, GetRandomNumber(minLength,maxLength + 1),height,false);
			platforms.Add(current);
			currentStartIndex += current.length;	

		}

	}

	void SetIsDanger()
	{
		List<int> indices = GetRandomNumbers(0, quantity, Mathf.RoundToInt(complexity * quantity));

		foreach(int current in indices)
		{
			platforms[current].isDanger = true;
			Debug.Log(current);
		}
	}

	void SetAddOnObjects()
	{
		List<int> indices = GetRandomNumbers(1, quantity, addOnObjects.Count);
		int counter = 0;
		foreach(int current in indices)
		{
			platforms[current].addOn = addOnObjects[counter];
			Debug.Log(current + " " + addOnObjects[counter].name);
			counter ++;
		}
	}


	List<int> GetRandomNumbers(int min, int max, int quantity)
	{
		List<int> result = new List<int>();
		List<int> buffer = new List<int>();

		for (int i = min ; i < max; i++)
		{
			buffer.Add(i);
		}

		for (int i = 0; i < quantity; i++)
		{
			int temp = GetRandomNumber(0, buffer.Count);
			result.Add(buffer[temp]);
			buffer.RemoveAt(temp);
		}

		return result;
	}

	int GetRandomNumber (int min, int max)
	{
		return pseudoRandom.Next(min, max);
	}


}



