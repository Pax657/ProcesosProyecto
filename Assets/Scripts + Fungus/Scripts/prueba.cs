using UnityEngine;
using Fungus;

public class AutoDialogueFollower : MonoBehaviour
{
    public SayDialog sayDialog;
    public Vector3 offset;

    void LateUpdate()
    {
        if (sayDialog.SpeakingCharacter == null)
            return;

        Character activeChar = sayDialog.SpeakingCharacter;

        characterAnchor anchor = activeChar.GetComponent<characterAnchor>();

        if (anchor != null && anchor.dialogueAnchor != null)
        {
            transform.position = anchor.dialogueAnchor.position + offset;
        }
    }
}