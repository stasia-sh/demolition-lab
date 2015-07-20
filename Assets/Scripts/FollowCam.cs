using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	//a FollowCam Singleton
	static public FollowCam S;

	//The point of interst
	public GameObject poi;
	//The desired Z pos of the camera
	public float camZ;

	public float easing = 0.05f;
	public Vector2 minXY;

	void Awake () {
		S = this;
		camZ = this.transform.position.z;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 destination;
		//If there is no poi, return to P[0,0,0]
		if (poi == null) {
			destination = Vector3.zero;
		} else {
			//get the position of the poi
			destination = poi.transform.position;

			//If poi is a projectile, check to see if it's at rest
			if (poi.tag == "Projectile") {
				//If it's sleeping (not moving)
				if (poi.GetComponent<Rigidbody> ().IsSleeping()) {
					//Return to default view in the next update
					poi = null;
					return;
				}
			}
		}
		//Get the position of the poi

		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);
		//Interpolate from the current Camera position towards destination
		destination = Vector3.Lerp (transform.position, destination, easing);
		//Retain a destination.z of CamZ
		destination.z = camZ;
		//Set the camera to the destination
		transform.position = destination;
		this.GetComponent<Camera> ().orthographicSize = destination.y + 10;
	}
}
