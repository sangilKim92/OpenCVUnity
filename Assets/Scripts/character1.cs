using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character1 : MonoBehaviour
{
    Vector3 v3;
    float x1, y1, z1;
    // Start is called before the first frame update
 
    void Start()
    {
        v3 = new Vector3();
    }
    public void Move(int x, int y, int distance)
    {
        x1= (float)(x - 340) * 7 / 200;//좌우 x,y,distance (553,258,229) 좌측 가운데  (553,144,248) 좌측 상단 (525,384,224) 좌측 하단
        y1 = (float)(y - 250) * 5 / 100;//높이 (343,250,258) 중앙  가운데 (340, 96, 250) 중앙 상단  (344, 393, 213) 중앙 하단
        z1 = (float)(distance - 220) * 7 / 180;//앞뒤 (107, 251, 236) 우측 가운데 (95 102, 251) 우측 상단 (94, 387, 216) 우측 하단 
        v3.Set(x1, y1, z1);
        Debug.Log(v3);
        transform.position=(v3); //얼굴 가까우면 400까지 증가한다고 생각하면 된다. 
        //x가 위아래 rotation +-25, y가 좌우 rotation +-20
    }
    // Update is called once per frame
    void Update()
    {

    }
}
