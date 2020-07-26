using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class face_tracker : MonoBehaviour
{
    int face_x = 0;
    int face_y = 0;
    int distance = 0;
    Mat subimage,rgbaMat,eqMat;
    Vector3 v3;
    WebCamTexture web;
    Texture2D texture;
    WebCamDevice webdevice;
    CascadeClassifier face_classifier;
    OpenCvSharp.Rect[] faces;
    Point lb, tr;
    character1 character;
    public GameObject gameobject;
    float x1, y1, z1;
    // Start is called before the first frame update
    void Start()
    {
        v3 = new Vector3();
        
        for (int cameraindex = 0; cameraindex < WebCamTexture.devices.Length; cameraindex++) {
            Debug.Log(WebCamTexture.devices[cameraindex].name);
            if (WebCamTexture.devices[cameraindex].isFrontFacing == true)
            {
                
                webdevice = WebCamTexture.devices[cameraindex];
                web = new WebCamTexture(webdevice.name, 640, 840, 60);
                //gameObject.GetComponent<MeshRenderer>().material.mainTexture = web;
                web.Play();//카메라 시작하였음     
                break;
            }
        //WebCam Textures는 Live 비디오 입력이 렌더링 되는 텍스쳐를 나타냅니다.이름,너비,높이,프레임 값을 인자로 준다. device를 이용해 카메라 리스트를 불러올수 있다.
         }
        face_classifier = new CascadeClassifier();
        face_classifier.Load("C:\\Users\\User\\Documents\\New Unity Project\\Assets\\OpenCV+Unity\\Demo\\Face_Detector\\haarcascade_frontalface_default.xml");//face detector load해야함                                                                                                                                                      
        character = new character1();
    }
    public void face_detector()
    {
        texture = new Texture2D(640, 840, TextureFormat.RGBA32, false);
        //texture.SetPixels(web.GetPixels());
        rgbaMat = new Mat(640, 840, MatType.CV_8UC1);
        eqMat = new Mat(640, 840, MatType.CV_8UC1);
        //equalizeHist해도 안의 내용이 바뀌는건 모르겠지만 내용은 아무것도 안변함. 그렇다면 할 필요가 있나???
        //rgbaMat =new Mat(OpenCvSharp.Unity.TextureToMat(web),imreadModes.Unchanged);
        rgbaMat = OpenCvSharp.Unity.TextureToMat(web);
        eqMat=rgbaMat.CvtColor(ColorConversionCodes.BGR2GRAY, 0);//이것도 마찬가지로 이미지는 변했을지 몰라도 data는 변한게 없음 색깔을 하나로 변환시켜 채널을 1개로 바꾸는 것
        eqMat.EqualizeHist();//명암을 확실히 한다. 
        faces = face_classifier.DetectMultiScale(eqMat, 1.03, 5, 0, null, null);
        //eqMAt matrix을 가지고 있는 이미지 scaleFactor: 이미지를 얼만큼 줄일지 정하는 것 적게 잡을수록 코스트가 커진다.
        //minNeighbors 이미지 안의 후보 얼굴중 얼마나 많은 neighbor을 가지고 있어야 하는가 높을수록 적게 찾고 quality가 높다.
        //minsize 이것보다 작은건 무시 maxsize 이것보단 큰 건 무시
        Debug.Log("faces 갯수:" + faces.Length + "  face data는 : " + faces);
        //face를 찾았는데 그걸 그림으로 못그리고 있는중
        if(faces.Length!=0)
        {
            OpenCvSharp.Scalar scal = new OpenCvSharp.Scalar(255, 255, 255);         
            lb = new Point(faces[0].X + faces[0].Width, faces[0].Y + faces[0].Height); //얼굴 오른쪽 위 끝
            tr = new Point(faces[0].X, faces[0].Y);//얼굴 왼쪽 아래
            Cv2.Rectangle(rgbaMat, tr,lb, scal, 1, LineTypes.Link8, 0);
            face_x = faces[0].X + (faces[0].Width / 2);
            face_y = faces[0].Y + (faces[0].Height / 2);//얼굴 중심점 xy좌표
            distance = (int)Mathf.Sqrt((faces[0].Width * faces[0].Width) + (faces[0].Height * faces[0].Height));//얼굴 대각선 길이

            Debug.Log(face_x + " " + face_y + " " + distance);
            x1 = -(float)(face_x - 340) * 7 / 200;//좌우 x,y,distance (553,258,229) 좌측 가운데  (553,144,248) 좌측 상단 (525,384,224) 좌측 하단
            y1 = -(float)(face_y - 250) * 5 / 100;//높이 (343,250,258) 중앙  가운데 (340, 96, 250) 중앙 상단  (344, 393, 213) 중앙 하단
            z1 = (float)(distance - 220) * 7 / 180;//앞뒤 (107, 251, 236) 우측 가운데 (95 102, 251) 우측 상단 (94, 387, 216) 우측 하단 
            v3.Set(x1, y1, z1);
            gameobject.transform.position = (v3); //얼굴 가까우면 400까지 증가한다고 생각하면 된다. 
        }
        
        texture=OpenCvSharp.Unity.MatToTexture(rgbaMat, texture);
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = texture;
        //texture.Apply();
    }

// Update is called once per frame
void Update()
    {
        face_detector();
    }
}
