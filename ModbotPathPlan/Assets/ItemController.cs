using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	GameObject item;
	ItemTrigger it;
	// Use this for initialization
	void Start () {
		item = GameObject.Find ("Item1");
		it = item.GetComponent<ItemTrigger> ();
		Debug.Log ("INITIAL: " + it.isActive);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			item.SetActive(!item.activeSelf);
			it.isActive = item.activeSelf;
			Debug.Log("set");
		}
	}

	public float getReduction(Vector3 p) {
		if (it.isActive && it.bd.Contains(p)) {
			Debug.Log (-3000.0f / Vector3.Distance(p, it.pos) + " "  + p.ToString ());
			return -3000.0f / Vector3.Distance(p, it.pos);
		}
		return 0.0f;
	}
}
