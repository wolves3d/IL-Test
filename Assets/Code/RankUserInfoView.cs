using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankUserInfoView : MonoBehaviour
{
	public Text rankLabel;
	public Text userNameLabel;
	public Text scoreLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetValues(int rank, string name, int score)
	{
		rankLabel.text = rank.ToString();
		userNameLabel.text = name;
		scoreLabel.text = "Score: " + score.ToString();
	}
}
