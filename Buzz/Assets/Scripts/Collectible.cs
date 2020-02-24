using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        Donut,
        Latte,
        WhiteClaw,
        BoneFragment
    }

    public CollectibleType type;

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            PowerupCollector collector = c.attachedRigidbody.gameObject.GetComponent<PowerupCollector>();
            if (collector != null)
            {
                switch (type)
                {
                    case CollectibleType.Donut:
                        collector.ReceiveDonut();
                        break;
                    case CollectibleType.Latte:
                        collector.ReceiveLatte();
                        break;
                    case CollectibleType.WhiteClaw:
                        collector.ReceiveWhiteClaw();
                        break;
                    case CollectibleType.BoneFragment:
                        collector.ReceiveBoneFragment();
                        break;
                }
                // TODO - implement sound here
                // EventManager.TriggerEvent<BombBounceEvent, Vector3>(c.transform.position);
                Destroy(this.gameObject);
            }
        }

    }
}
