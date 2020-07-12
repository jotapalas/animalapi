# AnimalAPI

This is a simple HTTP API which could be used to power a simple virtual pet game where you can feed and stroke your pets.

## Getting Started

This API is developed with .NET Core using C#. To test it you can just download the code, put it in a folder and then use `dotnet run`.

This will get the server running and you can test all the endpoints calling to your `localhost:5001`

## Creating test data

There are various endpoints in this API, but for a simple test you can use:

* `POST Users`: Creates a new User. The body must contain a JSON like this:
```
{
    "name": "Ash"
}
```

* `POST Animals`: Creates a new Animal. Animals represent the base animal that could later become a Pet. Example body:
```
{
    "name": "turtle"
}
```

* `POST Pets`: Creates a relation between a User and an Animal, making it a Pet:
```
{
    "name": "Blastoise",
    "ownerId": 1,
    "animalId": 1
}
```

## Testing the API manually

Once you have created all your neccesary Pets, you can start testing the Pets APO:

* `PATCH Pets/step`: Performs a step in all pets in database, increasing their hunger and decreasing their happiness both by one. Note that both hunger and happiness vary between -100 and +100 values, 0 being neutral, so if some of the values are already at one of the limit levels, it will not increase/decrease.

* `PUT Pets/{pet_id}/step`: Performs a step only in one pet. You can set how much you want to increase hunger or decrease happiness:
```
{
	"hungerIncrease": 150,
	"happinessDecrease": 120,
}
```

* `PUT Pets/{pet_id}/feed`: Feed your pet! You can set the `nutritionalValue` of the food you gave it:
```
{
	"nutritionalValue": 50
}
```

* `PUT Pets/{pet_id}/stroke`: Similar to `feed`. You can also set how much happiness your pet gets from the caress... Not all animals like petting:
```
{
	"happinessIncrease": -10
}
```

## Future work

There are still some features needed. For example, it would be good if different animals had different methods for increasing/decreasing happiness or hunger.

This could be made with some inheritance on the Animal model, but I haven't found the correct way to do this in C# yet (how the controllers should be, how the database should treat this, wether with pointers or without them...)

Should the backend perform the step automatically? I've decided to keep this project as a pure API, so some other service could perform the `step` part on the pets, but maybe we can implement an automatic service that performs a task every so often? I couldn't find the way either to do this properly.
