using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float m_VelocityCoef;
	public GameObject m_Projectile;
	public float m_ProjectileVelocity,m_FireRate;
	private float m_Xmin, m_Xmax, SpriteHalfSize;
	public int m_Life;

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

		if (m_Life<=0)
		{
			GameObject.FindObjectOfType<LevelManager>().LoadLevel("Lose");
		}
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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.000000001f, m_FireRate);
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}

		//Restrict position
		float newX = Mathf.Clamp(transform.position.x, m_Xmin + SpriteHalfSize, m_Xmax - SpriteHalfSize);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	private void Fire()
	{
		float SpriteMidHeight = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
		GameObject proj = Instantiate(m_Projectile, transform.position + new Vector3(0, SpriteHalfSize + SpriteMidHeight + 0.01f, 0), new Quaternion());
		proj.GetComponent<Rigidbody2D>().velocity = Vector2.up * m_ProjectileVelocity;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		Projectile myCollidedObject = collision.gameObject.GetComponent<Projectile>() as Projectile;

		if (myCollidedObject != null)
		{
			m_Life -= myCollidedObject.GetDamage();
			myCollidedObject.Hit();
		}

	}
}
