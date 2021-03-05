# GameOfWar
The card game war, implemented into a console app

## Design

This is obviously overkill for a simple app. It's main focus is to allow me to practice:
- Jason Taylor's Clean Architechture: https://github.com/jasontaylordev/CleanArchitecture
- Following SOLID principles
- Using xUnit for unit testing
- Setting up and using Dependency Injection
- Using appsettings.json for settings

## Review

I tried followed Jason Taylor's clean architechture, however I still need to clarify what goes in 
application layer and what goes in domain layer. As I understand it all buisness logic goes in 
domain layer, and the application layer is primarily for organizing the flow of the domain entities
and services. I think the big class I violate this is the Game.cs file (which also should have a better
name). I feel like it contains too much buisness logic. But putting it into the domain also doesnt 
seem correct since it is organising the flow of the application.

