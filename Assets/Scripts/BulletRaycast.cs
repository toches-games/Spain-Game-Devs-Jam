using UnityEngine;

public static class BulletRaycast {

    public static RaycastHit Shoot(Vector3 shootPosition, Vector3 shootDirection, float range) {

        LayerMask mask = LayerMask.GetMask("Ghost");
        RaycastHit raycastHit;

        if (Physics.Raycast(shootPosition, shootDirection, out raycastHit, range, mask)) {
            // Hit!            
            Ghost target = raycastHit.collider.GetComponent<Ghost>();

            //Debug.Log(raycastHit.collider.gameObject.name);

            if (target != null) {
                target.Damage();
                //Debug.Log("Hit");
            }
        }

        return raycastHit;

    }

}
