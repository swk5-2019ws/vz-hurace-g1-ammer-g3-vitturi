---
title: Hurace
subtitle: Assignment 2
author: [Ammer Simon, Vitturi Jan]
date: 2019-12-22
documentclass: scrartcl
---

# Race Control

# Simulator

The simulator provides a simple way to send time impulses for testing and demonstration purposes. It is composed by an implementation of `IRaceClock` and an view and related view controller.

The implementation of `SimulatorRaceClock` is trivial since it only provides a public method `SendImpulse` to trigger the `TimingTriggered` from `IRaceClock` with an artificial sensor identifier.

The view model to control the simulator takes an instance of `SimulatorRaceClock`, on which a bound `Command` `SendTimerImpulse` will call the clock's method that will trigger an event. The UI provides an input for the sensor identifier that, for convenience, is automatically incremented every time an impulse is sent, and reset to 0 after defined amount of impulses has been sent, so that the user can continually click to send impulses.

# Services

In order to access the data access objects (DAOs) the view controllers, and eventually the web API backend, communicate with a business logic layer divided into multiple *services*.

The services implement the business logic and provide an interface to control an manipulate the application's data in a way that avoids inconsistent or invalid states.

Each service may need one or more DAO implementations for each entity it updates or needs information from. An helper class `DaoProvider` has been created in order to collect all DAO implementations so to be passed to each service, which will then have all DAOs at its availability. The helper class simply takes all DAOs (`ICountryDao`, `ILocationDao`, `IRaceDao`, `IRunDao`, `ISensorMeasurementDao`, `ISkierDao`) in the constructor an makes them available as public properties.
