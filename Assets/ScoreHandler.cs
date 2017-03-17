using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {

	private int m_Score = 0; 

	public void AddToScore(int _value)
	{
		m_Score += _value;
		GameObject myScoreHandler = GameObject.FindGameObjectWithTag("ScoreText") as GameObject;
		myScoreHandler.GetComponent<ScoreHandler>().DisplayScore(m_Score);
	}

	public void DisplayScore(int _value)
	{
		GetComponent<Text>().text = ""+ _value;
	}
	

		
}
