using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public GameSettings settings;

    [SerializeField] private ActorData actorData;

    public TMP_Text actorUI;

    private string actorName;

    public string Name
    {
        get { return actorName; }
        set { actorName = value; }
    }

    public HealthSystem healthSystem = new HealthSystem();

    private int damage;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public string dataFilePath;
    public TextAsset actorDataJson;

    private void Start()
    {
        actorData = settings.GetDataFromTextJsonFile<ActorData>(actorDataJson);
        settings.LoadActorData(actorData, this);
    }

    public void UI()
    {
        actorUI.text = $"{Name} | Health: {healthSystem.Health} | Damage: {Damage}";
    }

}
