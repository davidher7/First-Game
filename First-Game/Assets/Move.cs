using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float playerSpeed;

	// Update is called once per frame
	void Update () 
    {

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        transform.Translate(x, y, 0);
		
	}
}
