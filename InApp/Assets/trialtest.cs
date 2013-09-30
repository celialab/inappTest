using UnityEngine;
using System.Collections;

public class trialtest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		  GUI.Box(new Rect(0, 0, Screen.width, Screen.height), 
			"Trial: "+inappControl.isTrial +
			"\ninapp: "+inappControl.InappBuyed);		
	}
}
