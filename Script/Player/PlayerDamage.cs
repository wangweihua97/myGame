using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerDamage : NetworkBehaviour
    {
        public PlayerProperty PlayerPropertyInstance;

        public void Damage(Vector2 pos ,float damagePower)
        {
            if(damagePower <= 0)
                return;
            Vector2 playerPos = transform.position;
            float distance = Mathf.Sqrt(Vector2.SqrMagnitude(playerPos - pos));
            Vector2 vector2 = Vector2.ClampMagnitude(playerPos - pos, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2((vector2.x)*damagePower*7/distance, (vector2.y)*damagePower*7/distance);
        }

        public void Hurt(int hurt)
        {
            PlayerPropertyInstance.EventCenterInstance.EventTrigger("Hurt",hurt);
            PlayerPropertyInstance.healthy -= hurt;
            PlayerPropertyInstance.healthyUI.RefreshText();
        }
    }
}