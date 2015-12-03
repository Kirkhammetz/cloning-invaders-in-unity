using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject EnemyPrefab;
	
	public float width = 10f;
	public float height = 5f;
	
	public float moveOffset = 0.5f;
	public float moveSpeed = 5f;
	
	float xmin,xmax,ymin,ymax;
	
	private bool movingRight = false;

	// Use this for initialization
	void Start () {
		Boundry();
		SpawnUntilFull(); 
	}
	
	// Update is called once per frame
	void Update () {
	 	if(movingRight){
			transform.position += new Vector3(moveSpeed * Time.deltaTime, 0);
	 	}else{
			transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0);
		}
				
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);
		if( leftEdgeOfFormation < xmin )
			movingRight = true;
		if( rightEdgeOfFormation > xmax )
			movingRight = false;
			
		if(AllMemberDead()){
			SpawnUntilFull ();
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0 ){
				return childPositionGameObject;
			}
		}
		return null; 
	}
	
	bool AllMemberDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0 ){
			 return false;
			}
		}
		return true;
	}
	
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
	}
	
	void Spawner(){
		foreach(Transform child in transform){
			child.name = "EnemyPosition";
			GameObject enemy = Instantiate(EnemyPrefab, child.transform.position , Quaternion.identity) as GameObject;
			enemy.name = "ENEMY";
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition ();
		if(freePosition != null){
			GameObject enemy = Instantiate(EnemyPrefab, freePosition.transform.position , Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull", 1f);
		}
		
	}
	
	void Boundry (){
		float distance = transform.position.z - Camera.main.transform.position.z;
		
		Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		Vector3 topBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance));
		
		xmin = leftBoundry.x + 0.5f;
		xmax = rightBoundry.x - 0.5f;
		ymin = leftBoundry.y + 0.5f;
		ymax = topBoundry.y - 0.5f;
	}
}
