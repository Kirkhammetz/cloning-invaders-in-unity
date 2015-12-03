using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject spawnPoint, shoot;
	
	public float shotsPerSeconds = 0.5f;

	public int health = 150;

	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Destroy(gameObject);
			}
		};
	}
	
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSeconds;
		if(Random.value < probability){
			Fire ();
		}
		
	}
	
	void Fire(){
		GameObject enemyShoot = Instantiate(shoot, transform.position, Quaternion.identity ) as GameObject;
		enemyShoot.GetComponent<Rigidbody2D>().velocity = new Vector3(0f,-5f,0f);
	}
}
