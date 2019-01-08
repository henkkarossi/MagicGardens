using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    Image img;
    float change;
    public float fadeSpeed;

    private void Awake()
    {
        img = gameObject.GetComponent<Image>();
    }

    void Update ()
    {
		if(img.color.a > 0)
        {
            change += -0.1f;
        }
        if (img.color.a < 1)
        {
            change += 0.1f;
        }

        img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + change * fadeSpeed * Time.deltaTime);
    }
}
