using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
	public Transform[] hotbar;
	private int index = 1;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
		if (scrollAxis > 0f)
			index++;
		
		if (scrollAxis < 0f)
			index--;
		
		if(index > hotbar.Length)
			index = 1;
		
		if(index < 1)
			index = hotbar.Length;
    }
}
