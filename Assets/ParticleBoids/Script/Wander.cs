using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{

	public float m_radius = 20f;
	public float m_minHeight = 0f;
	public float m_maxHeight = 3f;
	public float m_minSpeed = .2f;
	public float m_maxSpeed = 2f;
	public float m_arrivedDistance = .1f;
	public int m_randomSeed = 1763;

	private Vector3 m_targetPosition;
	private bool m_hasArrived = false;
	private float m_speed = 1f;

	// Use this for initialization
	void Start ()
	{
		Random.InitState (m_randomSeed);
		m_targetPosition = PickANewTargetPosition ();
		InvokeRepeating ("CheckDistance", 0, .5f);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!m_hasArrived)
			transform.position = Vector3.Lerp (transform.position, m_targetPosition, Time.deltaTime * m_speed);
	}

	void CheckDistance ()
	{
		if (Vector3.Distance (m_targetPosition, transform.position) < m_arrivedDistance) {
			m_hasArrived = true;
			m_targetPosition = PickANewTargetPosition ();
			m_hasArrived = false;
		}

		if (Random.Range (0f, 1f) < .5f)
			m_speed = Random.Range (m_minSpeed, m_maxSpeed);
		
	}

	Vector3 PickANewTargetPosition ()
	{
		Vector3 pos = Random.insideUnitSphere * m_radius;
		pos = new Vector3 (pos.x, Random.Range (m_minHeight, m_maxHeight), pos.z);
		return pos;
	}
}
