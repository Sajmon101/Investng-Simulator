# Game Overview

A turn-based simulation game where the player manages company stocks. The objective is to maximize your wealth by buying and selling assets, reacting to random market events, and responding to rumors that influence the market.

## How to Play

- In each turn, you can buy or sell shares of different companies.
- Pay attention to market rumors—they can affect the value of various companies’ stocks.
- After each turn, see how the market situation has changed.
- The game ends after 20 turns—a summary will be displayed: your total wealth, income/loss, the companies with the highest and lowest profit, and a chronological log of all events and transactions.

### Event System

Every event (random occurrence) in the game implements the `IRandomEvent` interface, which requires each event to define its probability (`probability`) and the method that applies its effects (`Apply()`).

Event triggering is managed by the `RandomEventManager` class, which decides—based on randomness and game conditions—whether a given event should occur. Information about every triggered event is sent to a logging system, enabling the presentation of a complete history of the gameplay.

Each type of event is a separate class implementing a specific event effect. There are currently three implemented: one affecting a single company, another impacting an entire sector, and a third one dependent on player actions.

The entire system is modular, easy to extend, and allows for new event types to be added without modifying the existing game logic.

### Rumor System

The game also features a rumor system, introducing additional random occurrences that affect selected companies. Rumors are loaded from a JSON file at the start of the round, and each rumor includes descriptive text as well as specific effects for each company. At the end of the round, the actual events are revealed, which could partially be anticipated based on the rumors.

In the future, the rumor system may be integrated with the event system in terms of logging, allowing the player to view the complete history of all in-game occurrences in one place.
