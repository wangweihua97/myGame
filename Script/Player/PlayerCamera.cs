using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerCamera:NetworkBehaviour
    {
        private float horizontal;
        private float vertical;
        private float size = 12;
        private float minHorizontal = -20;
        private float maxHorizontal = 120;
        private float minVertical = -20;
        private float maxVertical = 20;
        private float minSize = 10;
        private float maxSize = 20;
        private Camera mainCamera;
        public PlayerProperty PlayerPropertyInstance;
        
        //摄像机距离
        public float distance = 0f;
        public float scaleSpeed = 3f;
        public float movepeed = 0.1f;
 
 
        public float maxDistance = 30f;
        public float minDistance = 5f;
 
 
        //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
        private Vector2 oldPosition1;
        private Vector2 oldPosition2;
 
 
        private Vector2 lastSingleTouchPosition;
 
        private Vector2 m_CameraOffset;
        private Camera m_Camera;
 
        public bool useMouse = true;

        //这个变量用来记录单指双指的变换
        private bool m_IsSingleFinger;

        void Awake()
        {
            m_CameraOffset = new Vector2(0, 0);
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        void Start()
         {
             GameMgr.instance.AddFristUpdateEventListener(InitUpdate);
             GameMgr.instance.AddUpdateEventListener(ScreenMove);
             GameMgr.instance.AddUpdateEventListener(CheckInput);
         }

        void InitUpdate()
        {
            if (!isLocalPlayer)
            {
                GameMgr.instance.RemoveFristUpdateEventListener(InitUpdate);
                return;
            }
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyTDown", KeyTDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyRDown", KeyRDown);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyTUp", KeyTUp);
            PlayerPropertyInstance.EventCenterInstance.AddEventListener("keyRUp", KeyRUp);
            GameMgr.instance.RemoveFristUpdateEventListener(InitUpdate);
        }
        void KeyTDown()
        {
            size = mainCamera.orthographicSize + 1;
            if (size > maxSize)
                size = maxSize;
            mainCamera.orthographicSize = size;
        }
        
        void KeyRDown()
        {
            size = mainCamera.orthographicSize - 1;
            if (size < minSize)
                size = minSize;
            mainCamera.orthographicSize = size;
        }

        void KeyTUp()
        {
        }
        
        void KeyRUp()
        {
        }
        
        void ScreenMove()
        {
            /*if (Input.GetMouseButton(0))//判断鼠标是否按下
            {
                mainCamera.transform.Translate(Input.GetAxis("Mouse X") * -1f, Input.GetAxis("Mouse Y") * -1f, 0);

            }*/
        }

        public void LocationPlayer()
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y,
                mainCamera.transform.position.z);
        }

        void CheckInput()
        {
            
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
                {
                    lastSingleTouchPosition = Input.GetTouch(0).position;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    MoveCamera(Input.GetTouch(0).position);
                }
                m_IsSingleFinger = true;
     
            }
            else if (Input.touchCount > 1)
            {
                if (m_IsSingleFinger)
                {
                    oldPosition1 = Input.GetTouch(0).position;
                    oldPosition2 = Input.GetTouch(1).position;
                }
     
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    ScaleCamera();
                }
     
                m_IsSingleFinger = false;
            }
            else
            {
                m_CameraOffset = new Vector2(0, 0);
            }
            //用鼠标的
            if (useMouse)
            {
                distance = -1 * Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
                if (Input.GetMouseButtonDown(0))
                {
                    lastSingleTouchPosition = Input.mousePosition;
                }
                if (Input.GetMouseButton(0))
                {
                    MoveCamera(Input.mousePosition);
                }
            }
        }
     
        private void ScaleCamera()
        {
            var tempPosition1 = Input.GetTouch(0).position;
            var tempPosition2 = Input.GetTouch(1).position;
            float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
            float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);
            distance = (currentTouchDistance - lastTouchDistance) * scaleSpeed * Time.deltaTime;
            oldPosition1 = tempPosition1;
            oldPosition2 = tempPosition2;
        }
     
     
        private void LateUpdate()
        {
            if (m_CameraOffset.x != 0 || m_CameraOffset.y != 0)
            {
                float x = mainCamera.transform.position.x - m_CameraOffset.x*movepeed;
                if (x > maxHorizontal)
                    x = maxHorizontal;
                else if (x < minHorizontal)
                    x = minHorizontal;
                float y = mainCamera.transform.position.y - m_CameraOffset.y*movepeed;
                if (y > maxVertical)
                    y = maxVertical;
                else if (y < minVertical)
                    y = minVertical;
                mainCamera.transform.position = new Vector3(x, y, -10);
            }
            if (distance != 0)
            {
                size = mainCamera.orthographicSize + distance;
                if (size > maxSize)
                    size = maxSize;
                else if (size < minSize)
                    size = minSize;
                mainCamera.orthographicSize = size;
                distance = 0;
            }
            
        }
     
     
        private void MoveCamera(Vector2 scenePos)
        {
            m_CameraOffset = scenePos - lastSingleTouchPosition;
            lastSingleTouchPosition = scenePos;
        }
            
    }
}