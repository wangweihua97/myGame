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
        public float distance = 10.0f;
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
         void Start()
         {
             m_CameraOffset = new Vector2(0, 0);
             mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
 
        void CheckInput()
        {
            
            //判断触摸数量为单点触摸
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
                {
                    //在开始触摸或者从两字手指放开回来的时候记录一下触摸的位置
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
                //当从单指触摸进入多指触摸的时候,记录一下触摸的位置
                //保证计算缩放都是从两指手指触碰开始的
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
                    Debug.Log("GetMouseButtonDown:" + lastSingleTouchPosition);
                }
                if (Input.GetMouseButton(0))
                {
                    MoveCamera(Input.mousePosition);
                }
            }
        }
     
        /// <summary>
        /// 触摸缩放摄像头
        /// </summary>
        private void ScaleCamera()
        {
            //计算出当前两点触摸点的位置
            var tempPosition1 = Input.GetTouch(0).position;
            var tempPosition2 = Input.GetTouch(1).position;
     
     
            float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
            float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);
     
            //计算上次和这次双指触摸之间的距离差距
            //然后去更改摄像机的距离
            distance = (currentTouchDistance - lastTouchDistance) * scaleSpeed * Time.deltaTime;
     
     
            //把距离限制住在min和max之间
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
     
     
            //备份上一次触摸点的位置，用于对比
            oldPosition1 = tempPosition1;
            oldPosition2 = tempPosition2;
        }
     
     
        //Update方法一旦调用结束以后进入这里算出重置摄像机的位置
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
            }
            
        }
     
     
        private void MoveCamera(Vector2 scenePos)
        {
            m_CameraOffset = scenePos - lastSingleTouchPosition;
            lastSingleTouchPosition = scenePos;
        }
            
    }
}