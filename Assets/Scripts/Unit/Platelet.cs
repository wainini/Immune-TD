using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platelet : ImmuneCell
{
    [SerializeField] private Animator anim;

    private bool animationTriggered = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(plot != null && !animationTriggered)
        {
            anim.SetTrigger("FixWound");
        }
    }

    public void FinishFixWound()
    {
        Wound wound = (Wound)plot;
        wound.CloseWound();

        Destroy(this.gameObject);
    }
}
