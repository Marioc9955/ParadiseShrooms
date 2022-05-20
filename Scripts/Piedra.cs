using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piedra : Municion
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(base.Desaparecer());
        StartCoroutine(Recoger());
        //print("Start " + name + " velocity " + GetComponent<Rigidbody2D>().velocity + "rotation " + transform.rotation.eulerAngles);
    }

    public override void Destruir()
    {
        base.Destruir();
        //print("Se destruye una piedra");
    }

}
