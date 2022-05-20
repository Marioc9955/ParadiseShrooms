using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentEnemy : MonoBehaviour
{
    float tiempoEnDesap = 5;
    MeshRenderer mr;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        color = new Color(1, 1, 1);
        StartCoroutine(Desaparecer());
    }

    public IEnumerator Desaparecer()
    {
        float tiempoLimit = tiempoEnDesap + Time.time;
        while (tiempoLimit > Time.time)
        {
            color -= new Color(0, 0, 0, Time.deltaTime * 1 / tiempoEnDesap);
            mr.material.SetColor("_Color", color);
            //sp.color -= new Color(0, 0, 0, Time.deltaTime * 1 / tiempoEnDesap);
            yield return null;
        }
        Destroy(gameObject);
    }
}
