using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {
	public GameObject SmallB;
	public GameObject BigB;
	public float Speed;
	public float HP=100f;
	public Image UIHP;
	public Text UIText;
	public GameObject CanUI;
	public static bool CarBack;

	public Transform Sword;
	public GameObject SwordFire;
	public Transform Sword2;
	public GameObject SwordFire2;
	public GameObject Empty;
	public GameObject LightO;
	bool Attack;
	private AudioSource fon;
	public static bool Sound;
	void Start () {
		fon = GetComponent<AudioSource> ();
		Sound = false;
		CarBack = false;
		Time.timeScale = 1;
		gameObject.GetComponent<MainScript> ().enabled = true;
		Attack = false;
	}

	void Update () {
		if (Input.GetKey (KeyCode.Q)) {
			Attack = true;
		}
		if (Input.GetKey (KeyCode.W)) {
			Attack = false;
		}
		if (Attack) {
			BigB.SetActive (false);
			Debug.DrawRay (Sword.position, Sword.up * -1 * 100f);
			if (Input.GetKeyDown (KeyCode.X)) {
				fonning();
				BigB.SetActive (true);
				Ray ray = new Ray (Sword.position, Sword.up * -1 * 100f);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					GameObject fire = Instantiate<GameObject> (SwordFire);
					fire.transform.position = hit.point;
				}
			}
		}
		if (!Attack) {
			SmallB.SetActive (false);
			Debug.DrawRay (Sword2.position, Sword2.up * 100f);
			if (Input.GetKey (KeyCode.X)) {
				SmallB.SetActive (true);
				Ray ray2 = new Ray (Sword2.position, Sword2.up * 100f);
				RaycastHit hit2;
				if (Physics.Raycast (ray2, out hit2)) {
					GameObject fire2 = Instantiate<GameObject> (SwordFire2);
					fire2.transform.position = hit2.point;
					fire2.transform.SetParent (Empty.transform);
					}
				}
			}
		UIText.text = "" + HP;
		UIHP.fillAmount = HP / 100;
		if (HP <= 0) {
			Time.timeScale = 0;
			CanUI.SetActive (true);
			gameObject.GetComponent<MainScript> ().enabled = false;
		}
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.Translate (Vector3.forward * Speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.Translate (Vector3.back * Speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Rotate (Vector3.up * Speed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate (Vector3.up *-1* Speed * Time.deltaTime);
		}
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Damage") {
			CarBack = true;
			HP = HP - 50*0.1f;
		}
		if (col.tag == "SmallDamage") {
			HP = HP - 40*0.1f;
		}
		if (col.tag == "BigDamage") {
			LightO.SetActive (true);
			HP = 0;
		}
		if (col.tag == "Damage1") {
			HP = HP - 70*0.1f;
		}
	}
	void fonning(){
		fon.Play ();
	}
}
