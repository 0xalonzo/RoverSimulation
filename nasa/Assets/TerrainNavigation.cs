using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TerrainNavigation : MonoBehaviour
{
    public Terrain Terrain;
    public int ScanRange = 10;
    public float Gap = 1.0f;
    public float Height = 0.0f;
    public Transform ScannerPosition;
    public Transform MarkersContainer;
    public GameObject PositiveMarker;
    public GameObject NegativeMarker;

    void Start()
    {
        StartCoroutine(Scan());
    }

    IEnumerator Scan()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            foreach (Transform child in MarkersContainer)
            {
                GameObject.Destroy(child.gameObject);
            }

            for (int i = -ScanRange; i <= ScanRange; i++)
            {
                for (int j = -ScanRange; j <= ScanRange; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        var pxy = ScannerPosition.position;
                        pxy.x = Mathf.RoundToInt(ScannerPosition.position.x);
                        pxy.y = Mathf.RoundToInt(ScannerPosition.position.y);
                        pxy.z = Mathf.RoundToInt(ScannerPosition.position.z);
                        Debug.Log(pxy);
                        continue;
                    }
                    var oxy = ScannerPosition.position;
                    oxy.x = Mathf.RoundToInt(ScannerPosition.position.x);
                    oxy.y = Mathf.RoundToInt(ScannerPosition.position.y);
                    oxy.z = Mathf.RoundToInt(ScannerPosition.position.z);

                    var xy = Vector3.zero;
                    xy.x = oxy.x + i * Gap;
                    xy.y = oxy.y;
                    xy.z = oxy.z + j * Gap;
                    if (Vector3.Distance(xy, oxy) > ScanRange * Gap) continue;

                    var height = Terrain.SampleHeight(xy);
                    xy.y = height + Height;

                    var isOnMesh = NavMesh.SamplePosition(xy, out var hit, 1.0f, NavMesh.AllAreas);
                    if (isOnMesh)
                    {
                        GameObject.Instantiate(PositiveMarker, xy, Quaternion.identity, MarkersContainer);
                    } 
                    else
                    {
                        GameObject.Instantiate(NegativeMarker, xy, Quaternion.identity, MarkersContainer);
                    }
                }
            }
        }
    }
}
