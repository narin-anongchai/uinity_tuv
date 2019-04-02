using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chase : MonoBehaviour
{

	public Transform player;
	public Animator anim;
	private AudioSource ZombieAudio;

	public int playerHealth = 100;

	public Text playerhp;

	private float MoveSpeed = 1f;
	//public Rigidbody wolf;
	void Start ()
	{
		anim = GetComponent<Animator> ();
		player = GameObject.FindWithTag ("Player").transform;
		ZombieAudio = GetComponent <AudioSource> ();
		//wolf = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		
		Vector3 direction = player.position - this.transform.position;
		//Debug.Log (direction);
		float angle = Vector3.Angle (direction, this.transform.forward);
		if (Vector3.Distance (player.position, this.transform.position) < 10 && angle < 210) {
			direction.y = 0;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			ZombieAudio.Play ();


			anim.SetBool ("isIdle", false);
			if (direction.magnitude > 1.5) {
				//wolf.AddForce(Vector3.forward * MoveSpeed * Time.deltaTime);
				//(Vector3.forward * MoveSpeed * Time.deltaTime);
				this.transform.Translate (0, 0, 0.015f);
				anim.SetBool ("isWalking", true);

				ZombieAudio.Play ();

				anim.SetBool ("isAttack", false);
			} else {
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isAttack", true);
			
			
				

	
			}
		} else {
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttack", false);
		}
	}
}
