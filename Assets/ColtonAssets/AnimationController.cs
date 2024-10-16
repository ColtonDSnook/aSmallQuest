using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CombatManager;

public class AnimationController : MonoBehaviour
{
    private Animator mAnimator;
    public CombatManager combatManager;
    public CombatState combatState;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        combatManager = FindObjectOfType<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            if (combatManager.combatState == CombatManager.CombatState.InCombat)
            {
                mAnimator.ResetTrigger("MCRun");
                mAnimator.SetTrigger("MCIdle");
                return;
            }
            if (combatManager.combatState == CombatManager.CombatState.None)
            {
                mAnimator.ResetTrigger("MCIdle");
                mAnimator.SetTrigger("MCRun");
                return;
            }
        }
    }
}
