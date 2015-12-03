using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public int health = 250;
	public int speed;
	public float fireRate = 2f;
	public GameObject spawnPoint, shoot;

	private Rigidbody2D rb;
	
	
	float xmin,xmax,ymin,ymax;
	
	void Awake(){
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Start(){
		Boundry ();
	}
	
	void Update(){
		transform.position = new Vector3( Mathf.Clamp(transform.position.x, xmin, xmax), Mathf.Clamp(transform.position.y, ymin, ymax), transform.position.z );
		
		// shoot
		if( Input.GetKeyDown(KeyCode.Space) ){
		//method, delayFrom Start, repeat rate
			InvokeRepeating("Shoot", 0.000001f, fireRate);
		}
		if( Input.GetKeyUp(KeyCode.Space) ){
			CancelInvoke();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		rb.velocity = new Vector3(h, v , 0f) * speed;
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
	
	void Shoot(){
		// constant rate fire
		GameObject shootInstance = Instantiate(shoot, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
		shootInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 5f, 0f);
	}
	
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile && missile.tag == "EnemyShot"){
			health -= missile.GetDamage();
			missile.Hit();
			if(health <= 0){
				Destroy(gameObject);
			}
		};
	}
	
}
