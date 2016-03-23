using UnityEngine;
using System.Collections;

public class ItemTrigger : MonoBehaviour {

	public bool isActive;
	public Bounds bd;
	public Vector3 pos;

	// Use this for initialization
	void Start () {
		isActive = true;
		bd = this.gameObject.GetComponent<SphereCollider> ().bounds;
		pos = this.gameObject.transform.position;
		Debug.Log ("REAL POS: " + pos.ToString());
		//this.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
