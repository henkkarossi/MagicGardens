using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewOnlyInEditor : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {

        if(!Application.isEditor)
        gameObject.SetActive(false);
	}
}
