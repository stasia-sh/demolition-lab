using UnityEngine;
using System.Collections;

public class CloudCrafter : MonoBehaviour {

	public int numClouds = 40;
	public GameObject[] cloudPrefabs;
	public Vector3 cloudPosMin;
	public Vector3 cloudPosMax;
	public float cloudScaleMin = 1;
	public float cloudScaleMax = 5;
	public float cloudSpeedMult = 0.5f;

	public bool __________________;

	public GameObject[] cloudInstances;

	void Awake() {
		cloudInstances = new GameObject[numClouds];
		GameObject anchor = GameObject.Find ("CloudAnchor");
		GameObject cloud;
		for (int i=0; i<numClouds; i++) {
			int prefabNum = Random.Range(0, cloudPrefabs.Length);
			cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;

			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
			cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

			float scaleU = Random.value;
			float scaleVal = Mathf.Lerp (cloudScaleMin, cloudScaleMax, scaleU);

			cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
			cPos.z = 100 - 90*scaleU;

			cloud.transform.position = cPos;
			cloud.transform.localScale = Vector3.one * scaleVal;

			cloud.transform.parent = anchor.transform;

			cloudInstances[i] = cloud;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject cloud in cloudInstances) {
			float scaleVal = cloud.transform.localScale.x;
			Vector3 cPos = cloud.transform.position;

			cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

			if (cPos.x <= cloudPosMin.x)
			{
				cPos.x = cloudPosMax.x;
			}
			cloud.transform.position = cPos;
		}
	}
}
