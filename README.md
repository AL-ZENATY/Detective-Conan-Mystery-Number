# Detective-Conan-Mystery-Number 🕵️‍♂️

A Detective Conan-inspired mystery number game developed in C# WinForms.

The player takes the role of a detective attempting to solve a hidden number case using temperature-based hints ranging from ICE ❄️ to HOT 🔥.

This project combines:
- custom anime-inspired UI design
- multimedia integration
- audio/video systems
- dynamic visual effects
- interactive gameplay mechanics

---

## Features

- Detective Conan themed interface
- Dynamic HOT/COLD investigation system
- Custom transparent UI controls
- Animated intro sequence
- Background music + sound effects
- Video integration using Windows Media Player
- Multiple font switching system
- Interactive temperature meter
- Guess history log
- Cheat/debug system
- Fully custom UI helper framework

---

## Gameplay

Players must:
- choose a minimum and maximum range
- select attempt count
- guess the hidden number before attempts run out

Hints become:
- ICE
- COLD
- WARM
- HOT

depending on how close the guess is to the secret number.

---

## Technologies Used

- C#
- WinForms
- Windows Media Player API
- Custom UI rendering
- Object-oriented game architecture

---

## Audio & Multimedia System 🎵

The game includes:
- intro music
- animated intro video
- in-game background music
- UI interaction sound effects
- win/lose jingles
- dynamic fade transitions

All multimedia systems were custom-managed through dedicated classes and event-driven logic.

---

## Custom UI Framework 🎨

The project contains a fully custom helper system for:
- transparent controls
- hover effects
- custom trackbars
- skinned buttons
- transparent RichTextBoxes
- animated UI feedback

---

## Game Logic

The game engine handles:
- number generation
- guess validation
- temperature calculation
- attempt tracking
- heat percentage system
- win/loss conditions

---

## Project Structure

Main systems include:

- `Form1.cs`
  - main gameplay + UI logic

- `GameLogicZy.cs`
  - core gameplay engine

- `MusicManagerZy.cs`
  - audio management system

- `TransparentHelperZy.cs`
  - custom transparent UI framework

- `HotColdMeterZy.cs`
  - custom temperature meter system

---

## Inspiration

Inspired by:
- Detective Conan / Case Closed
- detective investigation aesthetics
- retro anime game interfaces

---

## Status

Project completed as a multimedia-focused C# desktop game and portfolio project.

---

## Developer

Developed by Zy (Abdullah Zenaty)
