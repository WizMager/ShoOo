using UnityEngine;

namespace Views.Impl.Ai.Impl
{
    public class MeleeAiView : AAiView, IDamagable
    {
        public void ReceiveDamage(float damage)
        {
            Debug.Log($"Ai {gameObject.name} receive damage: {damage}!");
        }
    }
}