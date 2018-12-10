using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButAnimationStagger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Animator>().Play("RedButterfly", -1, Random.Range(0.0f, 1.0f));
	}
}
