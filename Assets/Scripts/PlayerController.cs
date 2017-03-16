using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float m_VelocityCoef;

	private float m_Xmin, m_Xmax, SpriteHalfSize;

	// Use this for initialization
	void Start()
	{
		SpriteHalfSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
		m_Xmin = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).x;
		m_Xmax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
	}

	// Update is called once per frame
	void Update()
	{
		GetKeyboardInput();
	}

	void GetKeyboardInput()
	{
		Vector3 actualPosition = gameObject.transform.position;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * m_VelocityCoef * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * m_VelocityCoef * Time.deltaTime;
		}

		//Restrict position
		float newX = Mathf.Clamp(transform.position.x, m_Xmin + SpriteHalfSize, m_Xmax - SpriteHalfSize);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
