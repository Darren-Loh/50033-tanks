using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;
    
    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    private bool m_Dead; 
    public int isTutorial;           
    private bool isShielded = false;


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        if (isShielded == false){
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        m_CurrentHealth -= amount;

        SetHealthUI();
        if (m_CurrentHealth <= 0f && !m_Dead) OnDeath();
        }

    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        gameObject.SetActive(false);
        if (isTutorial == 1){
            SceneManager.LoadScene("Tutorial");
        }
    }
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("HealthPowerup")){
                if (m_CurrentHealth + 50f > m_StartingHealth){
                    m_CurrentHealth = m_StartingHealth;
                }else{
                    m_CurrentHealth += 50f;
                }
                
                SetHealthUI();
            } else if (col.gameObject.CompareTag("ShieldPowerup")){
                isShielded = true;
                StartCoroutine(removeEffect());
            }

        }
        IEnumerator  removeEffect(){
		yield  return  new  WaitForSeconds(5.0f);
		isShielded = false;
	}
}