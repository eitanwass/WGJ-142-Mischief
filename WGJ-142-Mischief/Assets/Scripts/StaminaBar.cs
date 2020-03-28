using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaminaBar : MonoBehaviour
{

    public Slider staminaBar;

    private int maxStamina = 100;
    private int currentStamina;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    public static StaminaBar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public int getStamina()
    {
        return currentStamina;
    }

    public void UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("not enough stamina");
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);//2 seconds cooldown until regen

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100; //stamina will regen by 1%
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}
