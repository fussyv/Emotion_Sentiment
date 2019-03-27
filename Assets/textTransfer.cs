using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textTransfer : MonoBehaviour
{
    public Text textForAnalyze;
    public InputField copier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            textForAnalyze.text = copier.text;
        }

    }
}
