# Console.DomainObject vs ValueObject

![NET](https://img.shields.io/badge/NET-10.0-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2026](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)

## Projekt 
Im dem Projekt geht es um das Thema des Unterschied zwischen `Doamin Object` und `Value Object`.

##  Domain-Klasse vs Value Object 
Der Unterschied zwischen einer Domain-Klasse (Entity) und einem Value Object gehört zu den Grundprinzipien des Domain-Driven Design (DDD). Beide repräsentieren fachliche Konzepte, unterscheiden sich aber in ihrer Bedeutung und ihrem Verhalten.

| Merkmal         | Domain-Klasse (Entity)                 | Value Object                                 |
| --------------- | -------------------------------------- | -------------------------------------------- |
| Identität       | Besitzt eine eindeutige Identität (Id) | Keine Identität                              |
| Gleichheit      | Gleich über die Id                     | Gleich über alle Werte                       |
| Veränderbarkeit | Meist veränderbar                      | Sollte unveränderlich (immutable) sein       |
| Lebenszyklus    | Hat einen Lebenszyklus                 | Existiert nur durch seine Werte              |
| Datenbank       | Eigene Tabelle mit Primärschlüssel     | Häufig in der Tabelle der Entity gespeichert |
| Zweck           | Repräsentiert ein fachliches Objekt    | Repräsentiert einen fachlichen Wert          |

### Beispiel einer Domain-Klasse

Ein Kunde besitzt eine Identität.
```csharp
public class Customer
{
    public Guid Id { get; }

    public string Name { get; private set; }

    public Address Address { get; private set; }

    public Customer(Guid id, string name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}
```

**Beispiel:**

```csharp
Customer(Id=1, Name="Max") == Customer(Id=1, Name="Max Mustermann")
```

Die beiden Objekte beschreiben denselben Kunden, obwohl sich Eigenschaften geändert haben.
**Die Identität entscheidet.**

### Beispiel eines Value Objects

Eine Adresse besitzt normalerweise keine eigene Identität.

```csharp
public record Address(string Street, string ZipCode, string City);
```


```csharp
public sealed class Address
{
    public string Street { get; }
    public string ZipCode { get; }
    public string City { get; }

    public Address(string street, string zipCode, string city)
    {
        Street = street;
        ZipCode = zipCode;
        City = city;
    }
}
```

**Beispiel:**

```csharp
var a1 = new Address("Hauptstr. 1", "68161", "Mannheim");
var a2 = new Address("Hauptstr. 1", "68161", "Mannheim");
```
Diese beiden Objekte sind fachlich identisch.

Es interessiert niemanden, welche Instanz es ist, sondern nur welche Werte sie enthält.

## Technischer Unterschied
### Entity

Eine Entity besitzt fast immer eine Id.
```csharp
public class Person
{
    public Guid Id { get; }

    public string Name { get; set; }
}
```
Der Vergleich erfolgt auf Basis der `Id`

```csharp
person1.Id == person2.Id
```
und nicht auf Basis des Inhalts.
```csharp
person1.Name == person2.Name
```

### Value Object

Ein Value Object besitzt keine Id. Der Vergleich erfolgt über sämtliche Eigenschaften. Mit einem record erledigt C# das automatisch:
```csharp
public record Money(decimal Amount, string Currency);
```

```csharp
var m1 = new Money(10, "EUR");
var m2 = new Money(10, "EUR");

Console.WriteLine(m1 == m2);   // True
```

Schreibt man dazu eine eigene Klasse, so müsste Equals() und GetHashCode() selbst überschrieben werden.

### Veränderbarkeit

Entities ändern ihren Zustand.
```csharp
customer.ChangeName("Müller");
customer.Move(newAddress);
```
Value Objects werden nicht verändert. Stattdessen erzeugt man eine neue Instanz (Beispiel mit `record`).
```csharp
var oldAddress = new Address("A", "1", "X");

var newAddress = oldAddress with
{
    City = "Y"
};
```

### Fachliche Sicht
Frage:
> "Ist dieses Objekt dieselbe fachliche Sache?"

**Kunde**
Ja, der Ansprechpartner ändert sich

**Vorher:*+
```Text
Kunde #15
Ansprechpartner: Donald Duck
```
**Nachher**
```Text
Kunde #15
Ansprechpartner: Dagobert Duck
```
Es ist immer noch derselbe Kunde.

=> Entity Adresse
**Vorher:*+
```Text
Musterstraße 5
01010 Endetenhausen
```
**Nachher**
```Text
Musterstraße 5
01010 Endetenhausen
```
Sind das dieselben? Ja, weil alle Werte gleich sind.

### Weitere typische Value Objects
- Geldbetrag (Money)
- Adresse (Address)
- E-Mail (Email)
- Telefonnummer (PhoneNumber)
- Zeitraum (DateRange)
- Name (PersonName)
- Gewicht (Weight)
- Entfernung (Distance)
- Temperatur (Temperature)
- Prozentsatz (Percentage)

### Typische Domain-Entities
- Kunde (Customer)
- Benutzer (User)
- Bestellung (Order)
- Rechnung (Invoice)
- Produkt (Product)
- Mitarbeiter (Employee)
- Fahrzeug (Car)
- Lagerartikel (InventoryItem)

Alle besitzen eine Identität.

## Eine gute aus Kombination Domain Objekte und Value Objekte :

Hier ist Customer die Entity, während PersonName, Address und Money Value Objects sind. Das führt zu einem klaren Domänenmodell mit unveränderlichen, leicht vergleichbaren Werttypen und einer Entity, die ihren fachlichen Lebenszyklus und ihre Identität kapselt.
```csharp
public sealed class Customer
{
    public Guid Id { get; }

    public PersonName Name { get; private set; }

    public Address Address { get; private set; }

    public Money CreditLimit { get; private set; }
}
```

```csharp
public record PersonName(string FirstName, string LastName);

public record Address(string Street, string ZipCode, string City);

public record Money(decimal Amount, string Currency);
```

**Merksatz:**

- Entity: Wer oder was ist es? → Identität ist entscheidend.
- Value Object: Wie sieht es aus? oder Welchen Wert hat es? → Die Werte sind entscheidend, nicht die Identität.

## Hinweis zum `record` 

Ein `record` in C# implementiert wertbasierte Gleichheit (Value Equality). Das bedeutet, dass alle öffentlichen Eigenschaften, die im primären Konstruktor definiert sind, zum Vergleich herangezogen werden.

Beispiel:
```csharp
public record Money(decimal Amount, string Currency);
```
generiert der Compiler sinngemäß folgenden Code:
```csharp
public override bool Equals(object? obj)

public virtual bool Equals(Money? other)

public override int GetHashCode()

public static bool operator ==(Money? left, Money? right)

public static bool operator !=(Money? left, Money? right)
```
### Der Vergleich

```csharp
var m1 = new Money(10, "EUR");
var m2 = new Money(10, "EUR");
var m3 = new Money(15, "EUR");
var m4 = new Money(10, "USD");
```

```csharp
Console.WriteLine(m1 == m2);      // True
Console.WriteLine(m1.Equals(m2)); // True

Console.WriteLine(m1 == m3);      // False
Console.WriteLine(m1 == m4);      // False
```
Intern entspricht das ungefähr folgendem Code:
```csharp
public bool Equals(Money? other)
{
    return other is not null
        && Amount == other.Amount
        && Currency == other.Currency;
}
```
### Was passiert bei komplexen Eigenschaften?

Überlegung:
```csharp
public record Product(string Name, Money Price);
```
Dann wird rekursiv verglichen.
```csharp
var p1 = new Product("PC", new Money(1000, "EUR"));
var p2 = new Product("PC", new Money(1000, "EUR"));

Console.WriteLine(p1 == p2); // True
```
Der Compiler macht sinngemäß:
```csharp
Name.Equals(other.Name)&& Price.Equals(other.Price)
```
Da Money selbst wieder ein Record ist, funktioniert der Vergleich ebenfalls wertbasiert.

### Vergleich mit einer normalen Klasse
Bei einer normalen Klasse:
```csharp
public class Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
}
```
ergibt sich:
```csharp
var m1 = new Money { Amount = 10, Currency = "EUR" };
var m2 = new Money { Amount = 10, Currency = "EUR" };

Console.WriteLine(m1 == m2);      // False
Console.WriteLine(m1.Equals(m2)); // False
```

### Warum Records ideal für Value Objects sind
Genau deshalb empfiehlt sich für Value Objects fast immer ein `record`:
```csharp
public record Money(decimal Amount, string Currency);
public record Address(string Street, string ZipCode, string City);
public record PersonName(string FirstName, string LastName);
```
Der Compiler erzeugt automatisch:

✔ Equals()\
✔ GetHashCode()\
✔ == und !=\
✔ Unterstützung für Dictionaries und HashSet<T>\
✔ Wertbasierte Gleichheit

Das erspart viel fehleranfälligen Boilerplate-Code und entspricht genau dem fachlichen Konzept eines Value Objects, bei dem nicht die Identität, sondern ausschließlich die enthaltenen Werte zählen.

## Hinweis
Der Source ist soll auch einfache Art und Weise die Funktionen eines Features zeigen. Der Source ist so geschrieben, das so wenig wie möglich zusätzliche NuGet-Pakete benötigt werden.

## Beispielsource

> Beschreibung

```csharp
```

```xml
```

```json
```

# Versionshistorie
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)
- Migration auf NET 10
