UniRX RPG Samle
===

First, create Subject and Observable. Note the Observable is subscribed when a Character is no longer alive.

```
    Subject<Character> heroDeadSubject;
    ...
    heroDeadSubject = new Subject<Character>();
    heroDeadSubject.AsObservable().Where(c => c.IsAlive == false).Subscribe(c => Debug.Log("Dead HP: " + c.HP.ToString()));    
```

Whenever the battle is done, fire the given event. When an event is fired, if the hero's HP is under zero, then it will be subscribed.

```
public IEnumerator Battle(Character hero, Character enemy)
{
    while (hero.IsAlive && enemy.IsAlive)
    {
        //This will subscribe due to the given expression of observable's Where does match the condition.
        heroDeadSubject.OnNext(hero);

        yield return 0;            
    }
}
```

Whenever the end of attack (aka each of turn) we can do execute event and observe it.

```
        Observable.FromEvent<EventHandler<EndAttackEventMessage>, EndAttackEventMessage>(
            h => (sender, e) => h(e),
            h => EndAttackEvent += h, h => EndAttackEvent -= h)
            .Subscribe(x => 
            {
                // Create status message.
                Debug.Log(x.sender.Name + " " + x.skillName + " " + x.target.Name + " for " + x.damage + " damage!");
            })
            .AddTo(disposables);

```

And send an event with event message at the end of attack.

```
    private string Attack(Character attacker, Character defender)
    {
        ...

        // send event
        EndAttackEvent(attacker, new EndAttackEventMessage { sender = attacker, target = defender, skillName = verb, damage = damage });

        return result;
    }
```

Reference
---

Code of RPG game is borrowed from [Fluent-Simple-RPG-Game](https://github.com/primaryobjects/Fluent-Simple-RPG-Game)
