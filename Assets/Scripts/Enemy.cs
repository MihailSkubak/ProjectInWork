using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	public int Rotation1=2;
	Transform player;
	CharacterController controller;
	RaycastHit ray;
	float sms=2;
	int diststart=350;
	public float distpl;
	public GameObject smt;
	public GameObject Ragdoll;
	public float HP = 100;
	public Image UIHP;
	public Text UIText;
	public GameObject pl;
	public float dist;
	public Transform target;
	public float Speed;
	public GameObject shrine2;
	void Start(){
		player = GameObject.Find ("Player").transform;
		controller=GetComponent<CharacterController>();
	}
	void Update () {
		Vector3 ygol = (player.position - transform.position).normalized;
		if (Physics.Raycast (transform.position, transform.forward, out ray, 3)) {
			Debug.DrawLine (transform.position, ray.point, Color.red);
			ygol += ray.normal * sms;
		} else {
			Debug.DrawRay (transform.position, transform.forward * 2, Color.yellow);
		}
		distpl = Vector3.Distance (player.position, transform.position);
		if (diststart > distpl) {
			if (dist < distpl) {
				Quaternion naclon = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (ygol), Rotation1 * Time.deltaTime);
				naclon.z = 0f;
				naclon.x = 0f;
				transform.rotation = naclon;
				controller.Move (transform.forward * Speed * Time.deltaTime);
				gameObject.GetComponent<Animator> ().SetTrigger ("Run");
				controller.Move (Vector3.down*10.0f*Time.deltaTime);
			}
		}

		//dist = Vector3.Distance (pl.transform.position,transform.position);
		UIText.text = "" + HP;
		UIHP.fillAmount = HP / 100;
		if (HP <= 0) {
			Ragdoll.transform.position = new Vector3 (smt.transform.position.x, smt.transform.position.y, smt.transform.position.z);
			Instantiate(Ragdoll);
			//Instantiate (Ragdoll, transform.position, transform.rotation);
			HP=100;
			gameObject.transform.position=new Vector3(shrine2.transform.position.x,shrine2.transform.position.y,shrine2.transform.position.z);
		}
		if (distpl > 9.7f) {
			gameObject.transform.Translate (Vector3.forward * Speed * Time.deltaTime);
			gameObject.GetComponent<Animator> ().SetTrigger ("Run");
		}
	if (distpl < 9.7f) {
		transform.LookAt (target);
		gameObject.GetComponent<Animator> ().SetTrigger ("Attack");
	}
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "BulletSmall") {
			HP = HP - 10*0.5f;
		}
		if (col.tag == "BulletBig") {
			HP = HP - 30*0.5f;
		}
	}
}
