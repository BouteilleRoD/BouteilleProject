
using UnityEngine;

public class Agent
{
    private Sensor _sensor;
    private Effector _effector;

    public static GameObject Prefab;
    public static GenerationManager manager;
    public const float MAXPower = 400;
    public const float MINPower = 100;
    public float X;
    public float Y;
    public float Z;
    public float power;
    public float Score = 0;
    public GameObject instance;


    //Constructor 
    public Agent(float x, float y, float z, float power)
    {
        _sensor = new Sensor();
        _effector = new Effector();
        X = x;
        Y = y;
        Z = z;
        this.power = power;
        Setup();
    }

    //Constructor par défaut
    public Agent()
    {
        _sensor = new Sensor();
        _effector = new Effector();
        X = Random.Range(-1f, 1f);
        Y = Random.Range(-1f, 1f);
        Z = Random.Range(0f, 1f);
        power = Random.Range(MINPower, MAXPower);
        Setup();
    }

    //Constructor par copie
    public Agent(Agent agent)
    {
        _sensor = new Sensor();
        _effector = new Effector();
        X = agent.X;
        Y = agent.Y;
        Z = agent.Z;
        power = agent.power;
        Setup();
    }

    private void Setup()
    {
        instance = Object.Instantiate(Prefab, manager.transform, false);
        instance.GetComponentInChildren<Dart>().Agent = this;
    }
    
    public void Mutate()
    {
        var xr = Random.Range(-0.02f, 0.02f);
        var yr = Random.Range(-0.02f, 0.02f);
        var zr = Random.Range(-0.02f, 0.02f);
        X += xr;
        Y += yr;
        Z += zr;
    }

    //Effecteur qui lance la flechette
    
    public void Start()
    {
        _effector.ThrowDart(instance, X, Y, Z, power);
    }

    //Capteur de score 
    public void OnCollisionEnter(Collision collision)
    {
        Score = _sensor.CalculateScore(collision, Score);
    }

    class Effector
    {
        public void ThrowDart(GameObject instance, float X, float Y, float Z, float power)
        {
            var rb = instance.GetComponentInChildren<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(Vector3.Normalize(new Vector3(X, Y, Z)) * power);
        }
    }

    class Sensor
    {
        public float CalculateScore(Collision collision, float Score)
        {
            if (collision == null)
            {
                Score = 0;
                return Score;
            }
            var targetCenter = GameObject.Find("center");
            var position = collision.contacts[0].point;
            var position1 = targetCenter.transform.position;
            Score = 10 - (position - position1).magnitude / 0.19f;
            //if (Score < 0) Score = 0;

            return Score;
        }
    }


}