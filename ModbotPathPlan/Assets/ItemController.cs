using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	GameObject item;
	// Use this for initialization
	void Start () {
		item = GameObject.Find ("Item1");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			item.SetActive(!item.activeSelf);
			Debug.Log("set");
		}
	}
}
