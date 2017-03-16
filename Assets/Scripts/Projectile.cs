using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int m_damage;

	public int GetDamage()
	{
		return m_damage;
	}

	public void Hit()
	{
		Destroy(gameObject);
	}
}
