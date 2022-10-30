using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarEnemy : MonoBehaviour {
	public int Rotation1=2;
	Transform player;
	CharacterController controller;
	RaycastHit ray;
	float sms=2;
	int diststart=450;//305
	public float distpl;
	public GameObject pl;
	public Transform target;
	UnityEngine.AI.NavMeshAgent nav;
	public float Speed;
	public float HP = 100;
	public Image UIHP;
	public Text UIText;
	public float dist;
	public GameObject shrine;
	void Start () {
		player = GameObject.Find ("Player").transform;
		controller=GetComponent<CharacterController>();
	}
	void Update () {
		Vector3 ygol = (player.position - transform.position).normalized;
		if (Physics.Raycast (transform.position, transform.forward, out ray, 15)) {
			Debug.DrawLine (transform.position, ray.point, Color.red);
			ygol += ray.normal * sms;
		} else {
			Debug.DrawRay (transform.position, transform.forward * 10, Color.yellow);
		}
		distpl = Vector3.Distance (player.position, transform.position);
		if (diststart > distpl) {
			if (dist < distpl) {
				Quaternion naclon = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (ygol), Rotation1 * Time.deltaTime);
				naclon.z = 0f;
				naclon.x = 0f;
				transform.rotation = naclon;
				controller.Move (transform.forward * Speed * Time.deltaTime);
				controller.Move (Vector3.down*10.0f*Time.deltaTime);
			}
		}



		//dist = Vector3.Distance (pl.transform.position,transform.position);
		UIText.text = "" + HP;
		UIHP.fillAmount = HP / 100;
		if (HP <= 0) {
			//Destroy (gameObject);
			HP=100;
			gameObject.transform.position=new Vector3(shrine.transform.position.x,shrine.transform.position.y,shrine.transform.position.z);
		}
		//transform.LookAt(target);
		if (dist > 80.5f) {
			MainScript.CarBack = false;
		}
		if (MainScript.CarBack==true) {
			//gameObject.transform.Translate (Vector3.back * Speed * Time.deltaTime);
		} else {
			gameObject.transform.Translate (Vector3.forward * Speed * Time.deltaTime);
		}
		//}

	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "BulletSmall") {
			HP = HP - 10*0.4f;
		}
		if (col.tag == "BulletBig") {
			HP = HP - 30*0.4f;
		}
	}
}
