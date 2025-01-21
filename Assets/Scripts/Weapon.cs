using UnityEngine;

public class Weapon : MonoBehaviour
{

    public virtual void SetAmmoEmmision(ParticleSystem ammo, bool isFiring)
    {
        var ammoEmission = ammo.emission;
        ammoEmission.enabled = isFiring;
    }

    public virtual void AimTarget(Transform targetObject , Transform thisObject , float rotationSpeed)
    {
        Vector3 directionToTarget = targetObject.position - thisObject.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        thisObject.rotation = Quaternion.Slerp(thisObject.rotation, rotationToTarget, Time.deltaTime * rotationSpeed);
    }
}
