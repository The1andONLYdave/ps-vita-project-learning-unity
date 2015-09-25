using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public float startHealth = 5;
	public int startLifePoints = 3;
	//GUI
	public Image healthGui;
	public Text lifePointsText;
	public Text messageText;

	private float health = 5;
	private float maxHealth = 5;
	private int lifePoints = 3;

	private Animator anim;
	private PlayerController playerController;
	private bool isDead = false;
	private bool isDamageable = true;
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		playerController = GetComponent<PlayerController>();

		//Der Level-Index muss dem Spiel entsprechend angepasst werden, 
		//wenn es z.B. eine Begruessungszene oder ein Hauptmenue gibt
		if (Application.loadedLevel == 0)
		{
			health = startHealth;
			lifePoints = startLifePoints;
		}
		else
		{
			health = PlayerPrefs.GetFloat("Health");
			lifePoints = PlayerPrefs.GetInt("LifePoints");
		}

		messageText.text = "";
		UpdateView();

	}
	
	void ApplyDamage(float damage)
	{
		if(isDamageable)
		{
			health -= damage;

			health = Mathf.Max (0,health);

			if (!isDead)
			{
				if (health == 0)
				{
					isDead  = true;
					Dying();
				}
				else
				{
					if (isDamageable)
					{
						Damaging();
					}
				}

				isDamageable = false;
				Invoke("ResetIsDamageable",1);
			}
		}
	}

	public void AddHealth(float extraHealth)
	{
		health += extraHealth;

		health = Mathf.Min (health,maxHealth);
		UpdateView();

	}

	void ResetIsDamageable()
	{
		isDamageable = true;
	}

	void Dying()
	{
		anim.SetBool("Dying", true);
		playerController.enabled = false;

		lifePoints --;
		UpdateView();

		if (lifePoints <=0)
		{
			//StartGame
			messageText.text = "Game Over";
			Invoke("StartGame",3);
		}
		else
		{
			//Restart Level
			Invoke("RestartLevel",1);
		}
	}

	void StartGame()
	{
		Application.LoadLevel(0);
	}

	void RestartLevel()
	{
		health = startHealth;
		isDead = false;
		anim.SetBool("Dying", false);
		playerController.enabled = true;

		if (!playerController.lookingRight)
		{
			playerController.Flip ();
		}
		//Level neu genierieren und Spieler zuruecksetzen
	}

	void Damaging()
	{
		anim.SetTrigger("Damage");
		UpdateView();
	}

	void OnDestroy()
	{
		PlayerPrefs.SetFloat("Health",health);
		PlayerPrefs.SetInt("LifePoints",lifePoints);
	}

	void UpdateView()
	{
		healthGui.fillAmount = health / maxHealth;
		lifePointsText.text = lifePoints.ToString ();
	}
}









