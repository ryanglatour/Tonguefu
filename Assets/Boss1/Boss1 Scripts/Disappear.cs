using UnityEngine;

public class DisappearAfterDelay : MonoBehaviour
{
    public GameObject Heart;
    public GameObject Power;
    public GameObject Core;
    public GameObject Army_1;
    public GameObject Army_2;
    public GameObject Army_3;
    public GameObject Shield;
    public GameObject Stinger;
    public GameObject Slime;
    public GameObject Slime_2;
    public BeeHealthBar beeHealthBar;
    public coreHealthBar CoreHealthBar;
    public GameObject BeeHive;
    public GameObject Avoid;
    public GameObject Stop;

    private void Start()
    {
        BeeHive.SetActive(false);
        CoreHealthBar.SetVisibility(false); // Dont show the health bar slider
        beeHealthBar.SetVisibility(false); // Dont show the health bar slider
        Slime.SetActive(false);
        Slime_2.SetActive(false);
        Stinger.SetActive(false);
        Shield.SetActive(false);  
        Core.SetActive(false);
        Avoid.SetActive(false);
        Heart.SetActive(false);
        Power.SetActive(false);
          
        Army_1.SetActive(false);
        Army_2.SetActive(false);
        Army_3.SetActive(false);
        Invoke("Disappear", 6.2f);
        Invoke("Appear", 10f);
        Invoke("Appear_2", 12f);
        Invoke("Appear_3", 14f);
        
    }

    private void Disappear()
    {
        gameObject.SetActive(false);
        BeeHive.SetActive(true);
    }

    private void Appear()
    {
        CoreHealthBar.SetVisibility(true); // show the health bar slider

        Shield.SetActive(true);
        Core.SetActive(true);
    
        Slime.SetActive(true);
        Slime_2.SetActive(true);

        beeHealthBar.SetVisibility(true); // show the health bar slider
        Stop.SetActive(false);

    }

        private void Appear_2()
    {
        Army_1.SetActive(true);
        Army_2.SetActive(true);
        Army_3.SetActive(true);

    }

        private void Appear_3()
    {
        Stinger.SetActive(true);

    }


    
}


    

