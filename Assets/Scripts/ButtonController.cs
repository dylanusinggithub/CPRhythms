using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public Animator animator;
    public Animator patient;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImage;
            animator.SetBool("Pressed", true);
            patient.SetBool("Pressy", true);
        }

        if(Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
            animator.SetBool("Pressed", false);
            patient.SetBool("Pressy", false);
        }
    }
}
