using UnityEngine;
using System.Collections;

public class CoroutineFade : MonoBehaviour {
    public GameObject cube;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("f"))
        {
            StartCoroutine("Fade");
        }
	}

    // Fade cube
    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = cube.GetComponent<Renderer>().material.color;
            //cube.GetComponent<Color>();
            c.a = f;
            cube.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
