using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float m_VelocityCoef;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetKeyboardInput();
	}

	void GetKeyboardInput()
	{
		Vector3 actualPosition = gameObject.transform.position;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			gameObject.transform.position  =  new Vector3(Mathf.Clamp(actualPosition.x - m_VelocityCoef * Time.deltaTime,0,100), actualPosition.y, actualPosition.z);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			gameObject.transform.position = new Vector3(Mathf.Clamp(actualPosition.x + m_VelocityCoef * Time.deltaTime, 0, 100), actualPosition.y, actualPosition.z);
		}
	}
}
