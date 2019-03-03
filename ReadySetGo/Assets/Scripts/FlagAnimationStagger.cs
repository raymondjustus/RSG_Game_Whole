using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimationStagger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Animator>().Play("WavingFlag", -1, Random.Range(0.0f, 1.0f));
	}
}
