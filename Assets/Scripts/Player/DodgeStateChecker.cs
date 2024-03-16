using UnityEngine;

public class DodgeStateChecker : StateMachineBehaviour
{
    private bool _dodgeFinished;
    private DodgeState _dodgeState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _dodgeFinished = false;
        _dodgeState = animator.GetComponentInParent<DodgeState>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!(stateInfo.normalizedTime >= 1) || _dodgeFinished) return;
        _dodgeFinished = true;
        _dodgeState.DodgeEnded();
    }
}
