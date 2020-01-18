using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollParallax : MonoBehaviour
{
    [SerializeField]
    private float scrollX;


    private float width;
    private float iniPos;
    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.position.x;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = (Camera.main.transform.position.x * (1 - scrollX));
        float distance = (Camera.main.transform.position.x * scrollX);
        transform.position = new Vector3(iniPos + distance, this.transform.position.y, this.transform.position.z);
        if(tempX > iniPos + width)
        {
            iniPos += width;
        }
        else if(tempX < iniPos - width)
        {
            iniPos -= width;
        }
        
    }
}
