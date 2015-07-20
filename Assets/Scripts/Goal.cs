using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	//A static field accessible by code anywhere
	static public bool goalMet = false;

	void OnTriggerEnter (Collider other) {
		//When the trigger is hit by something
		//Check to see if it's a Projectile
		if (other.gameObject.tag == "Projectile") {
			print ("You won!");
			//If so, set goalMet to true
			Goal.goalMet = true;
			//Also set the alpha of the color to higher opacity
			Color c = GetComponent<Renderer> ().material.color;
			c.a = 1;
			GetComponent<Renderer> ().material.color = c;

		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
