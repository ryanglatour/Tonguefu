using UnityEngine;

public class DisappearAfterDelay : MonoBehaviour
{
 
    public GameObject Army_1;
    public GameObject Army_2;
    public GameObject Army_3;
    public GameObject Shield;
    
    public GameObject Stinger;

    public GameObject Slime;
    

    private void Start()
    {
        Slime.SetActive(false);
        Stinger.SetActive(false);
        Shield.SetActive(false);  
          
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
    }

    private void Appear()
    {

        Shield.SetActive(true);
        
        Slime.SetActive(true);

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


    

