using UnityEngine;
using System.Collections;

public class Player_Mouse_Control : MonoBehaviour
{
    private static Player_Mouse_Control instance;
    public static Player_Mouse_Control Instance { get { return instance; } }


    public GameObject planeObj;
    Plane playerPlane;

    [System.NonSerialized]
    public Vector3 targetPoint;


    private void Awake()
    {
        instance = this;
        playerPlane = new Plane(Vector3.up,transform.parent.position);
    }


    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitdist;
        if (planeObj.GetComponent<MeshCollider>().Raycast(ray, out hitdist, 300f))
        {
            targetPoint = new Vector3(hitdist.point.x, hitdist.point.y, hitdist.point.z + 2);

            iTween.LookUpdate(gameObject, iTween.Hash(
                "looktarget", targetPoint,
                "axis","y",
                "time ",0.5f
                ));
        }
    }
}