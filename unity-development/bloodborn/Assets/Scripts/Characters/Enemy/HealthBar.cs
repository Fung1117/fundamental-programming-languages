using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public Enemy enemy;
    Vector3 localScale;


	// Use this for initialization
	void Start () {
		localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        localScale.x = (float)enemy.GetStat().bp / enemy.GetStat().max_bp;
        transform.localScale = localScale;
	}
}
