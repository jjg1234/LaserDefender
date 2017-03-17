using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject EnemyPrefab;
	private Vector3 m_posMin, m_posMax;
	private bool m_IsMovingRight = false;
	public float m_Speed = 1;
	public float m_PopRate;

	public void AdaptGizmoToFormation()
	{
		float SpriteHalfSize = EnemyPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;

		//Getting VectorMin and Max to draw the Gizmo cube around the formation
		m_posMin = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
		m_posMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

		foreach (Transform group in transform)
		{
			foreach (Transform child in group)
			{
				if (child.position.x < m_posMin.x)
				{
					m_posMin.x = child.position.x;

				}
				if (child.position.y < m_posMin.y)
				{
					m_posMin.y = child.position.y;
				}

				if (child.position.x > m_posMax.x)
				{
					m_posMax.x = child.position.x;

				}
				if (child.position.y > m_posMax.y)
				{
					m_posMax.y = child.position.y;
				}
			}
		}

		m_posMin -= new Vector3(SpriteHalfSize, SpriteHalfSize, 0);
		m_posMax += new Vector3(SpriteHalfSize, SpriteHalfSize, 0);


		//Vector3 FormationCenter = Vector3.Lerp(posMin, posMax, 0.5f);
		//m_FormationSize = posMax;
		//Gizmos.DrawCube(FormationCenter, new Vector2(Vector2.Distance(new Vector2(posMin.x,0),new Vector2(posMax.x,0)), Vector2.Distance(new Vector2(0,posMin.y), new Vector2(0,posMax.y))));
	}

	private static void DrawRectangleGizmo(Vector3 _lowLeft, Vector3 _HighRight)
	{
		Gizmos.DrawLine(_lowLeft, new Vector3(_lowLeft.x, _HighRight.y, 0));
		Gizmos.DrawLine(_lowLeft, new Vector3(_HighRight.x, _lowLeft.y, 0));
		Gizmos.DrawLine(_HighRight, new Vector3(_lowLeft.x, _HighRight.y, 0));
		Gizmos.DrawLine(_HighRight, new Vector3(_HighRight.x, _lowLeft.y, 0));
	}

	private void OnDrawGizmos()
	{
		AdaptGizmoToFormation();
		//DrawRectangleGizmo(m_posMin - new Vector3(SpriteHalfSize, SpriteHalfSize, 0), m_posMax + new Vector3(SpriteHalfSize, SpriteHalfSize, 0));
		DrawRectangleGizmo(m_posMin, m_posMax);

		//Gizmos.DrawWireCube(m_FormationCenter, m_FormationSize);
	}

	// Use this for initialization
	void Start()
	{
		AdaptGizmoToFormation();
		RespawnAll();
	}

	/// <summary>
	/// respawn All Enemies at Once
	/// </summary>
	private void RespawnAll()
	{
		foreach (Transform group in transform)
		{
			foreach (Transform child in group)
			{
				GameObject enemy = Instantiate(EnemyPrefab, child.position, new Quaternion()) as GameObject;
				enemy.transform.parent = child;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (m_IsMovingRight)
		{
			transform.position += new Vector3(m_Speed * Time.deltaTime, 0, 0);
			if (transform.position.x >= m_posMax.x)
			{
				m_IsMovingRight = false;
			}
		}
		else
		{
			transform.position -= new Vector3(m_Speed * Time.deltaTime, 0, 0);

			if (transform.position.x <= m_posMin.x)
			{
				m_IsMovingRight = true;
			}
		}
		//Vector3.Lerp(new Vector3(-100, -100, 0), new Vector3(100, 100, 0), Time.fixedTime);
		//transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_posMin.x, m_posMax.x), Mathf.Clamp(transform.position.y, m_posMin.y, m_posMax.y), 0);
		if (AllMembersAreDead())
		{
			Respawn();
		}

	}

	private void Respawn()
	{
		Debug.Log("Respawn Asked");

		InvokeRepeating("PopEnemyUnit", 0.000000001f, m_PopRate);
	}

	private bool AllMembersAreDead()
	{
		int AliveCount = 0;

		foreach (Transform group in transform)
		{
			foreach (Transform child in group)
			{
				AliveCount += child.childCount;
			}
		}
		return AliveCount == 0;

	}

	private void PopEnemyUnit()
	{
		Transform freePos = GetNextFreePosition();

		if (freePos != null)
		{
			GameObject enemy = Instantiate(EnemyPrefab, freePos.position, new Quaternion()) as GameObject;
			enemy.transform.parent = freePos;
		}
		else
		{
			CancelInvoke("PopEnemyUnit");
		}
	}


	private Transform GetNextFreePosition()
	{
		foreach (Transform group in transform)
		{
			foreach (Transform child in group)
			{
				if (child.childCount == 0)
				{
					return  child;
				}

			}
		}

		return null;
	}
}
