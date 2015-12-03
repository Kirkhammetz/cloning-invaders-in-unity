using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public int score = 0;
	
	private Text text;
	
	void Start(){
		text = GetComponent<Text>();
	}
	
	void Update(){
		text.text = score.ToString();
	}
	
	public void addScore(int points){
		score += points;
	}
	
	public void reset(){
		score = 0;
		text.text = score.ToString();
	}
}
