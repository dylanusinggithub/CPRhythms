using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                Debug.Log(Mathf.Abs(transform.position.y));
            
                //GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.y) < 3.6f)//change these? to 
                {
                    GameManager.instance.NormalHit();
                    Debug.Log("Hit");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 3.85f)
                {
                    GameManager.instance.GoodHit();
                    Debug.Log("Good Hit");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 4f)
                {
                    GameManager.instance.PerfectHit();
                    Debug.Log("Perfect Hit");
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) < 4.1f)
                {
                    GameManager.instance.GoodHit();
                    Debug.Log("Good Hit");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                } else
                {
                    GameManager.instance.NormalHit();
                    Debug.Log("Hit");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}
