using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    //example의 값이 넘어오지 않는다. example에서 실행하는게 더 좋을듯하다.
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f; //카메라 위치
    Vector3 v3,v33;
    // Start is called before the first frame update
    void Start()
    {
        v3 = new Vector3();
        v33 = new Vector3();
    }

    private void LateUpdate()
    {
        v3.x = player.transform.position.x;
        v3.y = player.transform.position.y;
        v3.z = player.transform.position.z;
        transform.position = v3;
        v33.Set(0, 0, 20);
       // transform.LookAt(v33    );
    }
    // Update is called once per frame
    void Update()
    {
     }
}
