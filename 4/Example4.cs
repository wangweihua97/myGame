//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example4 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void OnValueChange(Vector2 param)
    {
        Debug.Log(param);
    }
}
