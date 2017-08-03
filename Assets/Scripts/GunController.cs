using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public bool isFiring;

	public BulletController bullet;
	public float bulletVelocity;

	public float fireRate;
	private float shotDelay;

	public Transform firePoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFiring) {
			shotDelay -= Time.deltaTime;
			if (shotDelay <= 0) {
				shotDelay = fireRate;
				BulletController newBullet = Instantiate (bullet, firePoint.position, firePoint.rotation);
				newBullet.velocity = bulletVelocity;
			} else {
				shotDelay = 0;
			}
		}
	}
}
