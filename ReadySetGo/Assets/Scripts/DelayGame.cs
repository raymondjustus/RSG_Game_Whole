using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayGame : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(Wait());

    }
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator Wait()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;// new WaitForSeconds(5);
        Time.timeScale = 1;
        print("Wait Done");
    }
}
