using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public int m_Life;
	public GameObject m_Projectile;
	public float m_ShootingVelocity;
	public float m_ShotPerSecond = 0.5f;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float probability = Time.deltaTime * m_ShotPerSecond;

		if (probability > Random.value)
		{
			Shoot();
		}
		

		if (m_Life <= 0)
		{
			Destroy(gameObject);
		}
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

	private void Shoot()
	{
		float SpriteMidHeight = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
		
		GameObject proj = Instantiate(m_Projectile, (transform.position + (Vector3.down * (SpriteMidHeight+0.1f)) + (Vector3.down * m_Projectile.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2)), new Quaternion()) as GameObject;
		proj.GetComponent<SpriteRenderer>().color = Color.red;
		proj.GetComponent<Rigidbody2D>().velocity = Vector2.down * m_ShootingVelocity;

	}



}
