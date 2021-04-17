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
         void Start()
         {
             mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
             GameMgr.instance.AddFristUpdateEventListener(InitUpdate);
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
            PlayerPropertyInstance.EventCenterInstance.AddEventListener<Vector2>("ScreenMove", ScreenMove);
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
        
        void KeySpaceDown()
        {
        }

        void KeyTUp()
        {
        }
        
        void KeyRUp()
        {
        }

        void ScreenMove(Vector2 vector2)
        {
            
        }
        
    }
}