using UnityEngine;

public static class BulletRaycast {

    public static void Shoot(Vector3 shootPosition, Vector3 shootDirection, float range) {

        LayerMask mask = LayerMask.GetMask("Ghost");
        RaycastHit raycastHit;

        if (Physics.Raycast(shootPosition, shootDirection, out raycastHit, range, mask)) {
            // Hit!            
            Ghost target = raycastHit.collider.GetComponent<Ghost>();
            if (target != null) {
                target.Damage();
            }
        }

    }

}
