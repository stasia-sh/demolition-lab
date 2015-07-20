using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	//a Slingshot Singleton
	static public Slingshot S;

	public GameObject launchPoint;
	public GameObject prefabProjectile;
	public Vector3 launchPos;
	public GameObject projectile;
	public bool aimingMode;
	public float velocityMult=6f;

	void Awake () {
		S = this;
		Transform launchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);
		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter () {
		//print ("Slingshot: OnMouseEnter");
		launchPoint.SetActive (true);
	}

	void OnMouseExit () {
		//print ("Slingshot: OnMouseExit");
		launchPoint.SetActive (false);
	}

	void OnMouseDown () {
		//The player has pressed the mouse button over Slingshot
		aimingMode = true;
		//Instantiate a projectile
		projectile = Instantiate (prefabProjectile) as GameObject;
		//Start it at the launch point
		projectile.transform.position = launchPos;
		//Set it to isKinematic for now
		projectile.GetComponent<Rigidbody> ().isKinematic = true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//If the slingshot is not in aiming mode, dont run this script
		if (!aimingMode)
			return;
		//Get the current mouse position in 2D screeen coordinates
		Vector3 mousePos2D = Input.mousePosition;
		//Convert the mouse position to 3D world coordinates
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint (mousePos2D);

		//Find the delta from the launchPos tp the mousePos3D
		Vector3 mouseDelta = mousePos3D - launchPos;
		//Limit mouseDelta to hte radius of the Slingshot SphereCollider
		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;
		if (mouseDelta.magnitude > maxMagnitude) {
			mouseDelta.Normalize();
			mouseDelta *= maxMagnitude;
		}

		//Move the projectile to this new position
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		if (Input.GetMouseButtonUp (0)) {
			//The mouse has been released
			aimingMode = false;
			projectile.GetComponent<Rigidbody> ().isKinematic = false;
			projectile.GetComponent<Rigidbody> ().velocity = -mouseDelta * velocityMult;
			FollowCam.S.poi = projectile;
			projectile = null;
		}

	}
}
