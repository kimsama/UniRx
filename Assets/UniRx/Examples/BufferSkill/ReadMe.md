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


Reference
---

Code of RPG game is borrowed from [Fluent-Simple-RPG-Game](https://github.com/primaryobjects/Fluent-Simple-RPG-Game)
