using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerVibration : MonoBehaviour {

    Coroutine snowmanCoroutine;
    Coroutine humanCoroutine;

    public void vibrate(bool snowman, InputDevice device, float time, float intensity)
    {
        if (snowman)
        {
            if (snowmanCoroutine != null)
            {
                //StopCoroutine(snowmanCoroutine);
            }
            snowmanCoroutine = StartCoroutine(SnowmanAttackVibration(time, intensity, device));
        }
        else
        {
            if (humanCoroutine != null)
            {
               // StopCoroutine(humanCoroutine);
            }
            humanCoroutine = StartCoroutine(HumanAttackVibration(time, intensity, device));
        }
    }

    IEnumerator SnowmanAttackVibration(float time, float intensity, InputDevice device)
    {
		
        device.Vibrate(0, intensity);
        yield return new WaitForSeconds(time / 2);
        device.Vibrate(intensity, 0);
        yield return new WaitForSeconds(time / 2);
        
		//device.Vibrate(intensity);
		//yield return new WaitForSeconds(time);
        device.Vibrate(0, 0);
    }

    IEnumerator HumanAttackVibration(float time, float intensity, InputDevice device)
    {
        device.Vibrate(intensity);
        yield return new WaitForSeconds(time);
        device.Vibrate(0);
    }
}
