using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
	public int power = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(power >= 99)
		{
			gameObject.SetActive(true);
		}
		
		
		if(Input.GetKey(KeyCode.Q))
		{
			Debug.Log(power);
		}
      
    }
}
