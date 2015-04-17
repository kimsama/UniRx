using UnityEngine;
using System;
using System.Collections;

using FluentInterfaceExample.Types;
using FluentInterfaceExample.Types.ExpressionBuilder;
using FluentInterfaceExample;

using UniRx;

public class BattleTest : MonoBehaviour 
{

    System.Random ran = new System.Random((int)DateTime.Now.Ticks);
    CharacterBuilder builder = new CharacterBuilder();

    Character hero, enemy;

    Subject<Character> heroDeadSubject, enemyDeadSubject;

    public class EndAttackEventMessage : EventArgs
    {
        public Character sender { get; set; }
        public Character target { get; set; }

        public string skillName { get; set; }

        public int damage { get; set; }
    }

    public static EventHandler<EndAttackEventMessage> EndAttackEvent;

    CompositeDisposable disposables = new CompositeDisposable();
	// Use this for initialization
	void Start () 
    {
        // Build our Character with the expression builder.
        builder.Create("Valient")
            .As(Character.ClassType.Fighter)
            .WithAge(22)
            .HP(20)
            .Str(18)
            .Agi(14)
            .Int(12);

        // Get our Character.
        hero = builder.Value();
        enemy = null;

        heroDeadSubject = new Subject<Character>();
        heroDeadSubject.AsObservable().Where(c => c.IsAlive == false).Subscribe(c => Debug.Log("Dead HP: " + c.HP.ToString()));
        
        //This will not subscribe due to the given expression of Where does not match the condition.
        //heroDeadSubject.OnNext(hero);

        Observable.FromEvent<EventHandler<EndAttackEventMessage>, EndAttackEventMessage>(
            h => (sender, e) => h(e),
            h => EndAttackEvent += h, h => EndAttackEvent -= h)
            .Subscribe(x => 
            {
                // Create status message.
                Debug.Log(x.sender.Name + " " + x.skillName + " " + x.target.Name + " for " + x.damage + " damage!");
            })
            .AddTo(disposables);

        StartCoroutine(DoUpdate());
	}
	
	// Update is called once per frame
	IEnumerator DoUpdate () 
    {
        // Put our hero to battle against endless enemies, and see how long he survives!
        while (hero.IsAlive)
        {
            if (enemy == null || !enemy.IsAlive)
            {
                // Build an enemy with the expression builder.
                builder.Create(CommonHelper.GenerateRandomName())
                    .As((Character.ClassType)ran.Next(4))
                    .WithAge(ran.Next(12, 200))
                    .HP(ran.Next(5, 12))
                    .Str(ran.Next(21))
                    .Agi(ran.Next(21))
                    .Int(ran.Next(21))
                    .Gold(ran.Next(50));

                // Get our enemy.
                enemy = builder.Value();
            }
            else 
            {
                // Display start of battle.
                CommonHelper.DisplayStartOfBattle(hero, enemy);

                // Battle time!
                //CommonHelper.Battle(hero, enemy);
                yield return StartCoroutine(Battle(hero, enemy));
                //yield return Battle(hero, enemy);
            }

            //;
            //yield return StartCoroutine(WaitForKey());
            if (hero == null)
                yield break;
        }

        Debug.LogWarning("+++ End of Game +++");

        yield return 0;
	}

    /// <summary>
    /// Simple battle system.
    /// </summary>
    /// <param name="hero">Character</param>
    /// <param name="enemy">Character</param>
    public IEnumerator Battle(Character hero, Character enemy)
    {
        // Battle the enemy.
        while (hero.IsAlive && enemy.IsAlive)
        {
            // Print quick stats.
            Debug.Log(hero.QuickStats() + " / " + enemy.QuickStats());

            yield return new WaitForSeconds(1);

            // Hero attacks!
            Debug.Log(Attack(hero, enemy));

            yield return new WaitForSeconds(1);

            // Enemy attacks!
            Debug.Log(Attack(enemy, hero));

            // Prompt for next round of combat.
            if (hero.IsAlive)
            {
                Debug.Log(">");
            }

            //This will subscribe due to the given expression of observable's Where does match the condition.
            heroDeadSubject.OnNext(hero);

            yield return 0;
        }

        // Analyze battle results.
        if (hero.IsAlive)
        {
            // Hero won!
            hero.Gold += enemy.Gold;

            Debug.Log("Our hero survives to fight another battle! Won " + enemy.Gold + " gold!");
            Debug.Log(">");
            enemy = null;
        }
        else
        {
            // Enemy won!
            Debug.Log("Our hero has fallen with " + hero.Gold + " gold! The world is covered in darkness once again.");
        }

        yield return 0;
    }

    /// <summary>
    /// Verbs used for attacking.
    /// </summary>
    private static readonly string[] _attackVerbs =
        {
            "slashes",
            "stabs",
            "smashes",
            "impales",
            "poisons",
            "shoots",
            "incinerates",
            "destroys"
        };

    /// <summary>
    /// Attacks a Character and returns status message.
    /// </summary>
    /// <param name="attacker">Character initiating attack</param>
    /// <param name="defender">Character to attack</param>
    /// <returns>Status message</returns>
    private string Attack(Character attacker, Character defender)
    {
        string result = "";

        System.Random ran = new System.Random((int)DateTime.Now.Ticks);

        // Calculate damage.
        int damage = ran.Next(10);

        // Deduct damage from defender's HP.
        defender.HP -= damage;

        // Select an attack verb.
        string verb = _attackVerbs[ran.Next(_attackVerbs.Length)];

        // send event
        EndAttackEvent(attacker, new EndAttackEventMessage { sender = attacker, target = defender, skillName = verb, damage = damage });

        return result;
    }

    IEnumerator WaitForKey()
    {
        while(!Input.anyKeyDown)
            yield return 0;

        yield return 0;
    }

    void OnDestroy()
    {
        disposables.Dispose();
    }
}
