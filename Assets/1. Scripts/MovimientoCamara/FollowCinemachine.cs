using Cinemachine;
using System.Collections;
using UnityEngine;

public class FollowCinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float smoothTime = 0.3f;
    public float orthoSizeNormal = 6f;  // Tama�o ortogr�fico normal
    public float orthoSizeNi�o = 4f;    // Tama�o ortogr�fico cuando se enfoca al ni�o

    private Transform target;

    void Start()
    {
        virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("Componente CinemachineVirtualCamera no encontrado en el objeto.");
        }        
    }


    public void SeguirObjeto(GameObject follow, bool esNi�o = false)
    {
        target = follow.transform;
        StopAllCoroutines();
        StartCoroutine(MoveTowardsTarget());
        StartCoroutine(ChangeOrthoSize(esNi�o ? orthoSizeNi�o : orthoSizeNormal));
    }

    IEnumerator MoveTowardsTarget()
    {
        while (Vector3.Distance(transform.position, target.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, smoothTime * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ChangeOrthoSize(float targetSize)
    {
        while (Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - targetSize) > 0.01f)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetSize, smoothTime * Time.deltaTime);
            yield return null;
        }
    }
}
