﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        FreshDonut,
        Latte,
        WhiteClaw,
        BoneFragment,
        Honey,
        DroppedDonut
    }

    public CollectibleType type;

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            PowerupCollector collector = c.attachedRigidbody.gameObject.GetComponent<PowerupCollector>();
            if (collector != null && !type.Equals(CollectibleType.DroppedDonut))
            {
                switch (type)
                {
                    case CollectibleType.FreshDonut:
                        collector.ReceiveDonut();
                        break;
                    case CollectibleType.Latte:
                        collector.ReceiveLatte();
                        break;
                    case CollectibleType.WhiteClaw:
                        collector.ReceiveWhiteClaw();
                        break;
                    case CollectibleType.Honey:
                        collector.ReceiveHoney();
                        break;
                    case CollectibleType.BoneFragment:
                        collector.ReceiveBoneFragment();
                        break;
                    default:
                        break;
                }
                Destroy(this.gameObject);
            }
        }

    }

    public void Drop(Vector3 spawnPoint)
    {
        this.gameObject.transform.position = spawnPoint;
    }
}
