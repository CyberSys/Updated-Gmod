using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
	public Transform[] hotbar;
	private int index = 0;
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
		
		if(index > hotbar.Length - 1)
			index = 0;
		
		if(index < 0)
			index = hotbar.Length - 1;
		
		for(int i = 0; i < hotbar.Length; i++){
			if(i == index)
				hotbar[i].gameObject.SetActive(true);
			if(i != index)
				hotbar[i].gameObject.SetActive(false);
		}
    }
}
