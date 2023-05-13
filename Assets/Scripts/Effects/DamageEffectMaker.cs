using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamagePoint
{
    public Vector3 position;
    public Vector2 direction;
    public DamagePoint(Vector3 position, Vector2 direction)
    {
        this.position = position;
        this.direction = direction;
    }

    public static implicit operator DamagePoint(Transform t)
    {
        return new DamagePoint(t.position, t.right);
    }
}
public class DamageEffectMaker : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    public static void CreateDamageEffect(GameObject gm, float damage, DamagePoint damagePos)
    {
        DamageEffectMaker effectMaker = gm.GetComponent<DamageEffectMaker>();
        if (effectMaker != null)
        {
            effectMaker.InstantiateEffect(damage, damagePos);
        }
    }
    protected virtual void InstantiateEffect(float damage, DamagePoint damagePos)
    {
        Transform effect = Instantiate(effectPrefab, damagePos.position, Quaternion.identity).transform;
        effect.right = -damagePos.direction;
    }
}
