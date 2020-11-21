using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 16.0f;
    public GameObject dialogBox1;
    public GameObject dialogBox2;
    public GameObject dialogBox3;
    public GameObject dialogBox4;

    private float timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox1.SetActive(false);
        dialogBox2.SetActive(false);
        dialogBox3.SetActive(false);
        dialogBox4.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay <12 || )
            {
                dialogBox1.SetActive(false);
                DisplayDialog2();
            }
            if (timerDisplay <8)
            {
                dialogBox3.SetActive(false);
                DisplayDialog3();
            }
            if (timerDisplay <4)
            {
                dialogBox3.SetActive(false);
                DisplayDialog4();
            }
            if (timerDisplay <0)
            {
                dialogBox4.SetActive(false);
            }
        }
    }

    public void DisplayDialog1()
    {
        timerDisplay = displayTime;
        dialogBox1.SetActive(true);
    }
    public void DisplayDialog2()
    {
        timerDisplay = displayTime;
        dialogBox2.SetActive(true);
    }
    public void DisplayDialog3()
    {
        timerDisplay = displayTime;
        dialogBox3.SetActive(true);
    }
    public void DisplayDialog4()
    {
        timerDisplay = displayTime;
        dialogBox4.SetActive(true);
    }
}
