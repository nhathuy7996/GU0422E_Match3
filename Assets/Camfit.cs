using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfit : MonoBehaviour
{
    [SerializeField] Transform[] obj = new Transform[2]; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        float distance = Mathf.Abs(obj[0].position.x - obj[1].position.x);

        Camera.main.orthographicSize = (distance / Camera.main.aspect) / 2f;

        float minX = obj[0].position.x < obj[1].position.x ? obj[0].position.x : obj[1].position.x;
        Vector3 pos = Camera.main.transform.position;
        pos.x = distance / 2f + minX;

        Camera.main.transform.position = pos;
    }
}
