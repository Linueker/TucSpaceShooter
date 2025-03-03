# TucSpaceShooter

Ett rymdskjutarspel utvecklat med MonoGame/XNA-ramverket.

## Om projektet

TucSpaceShooter är ett klassiskt shoot 'em up-spel inspirerat av arkadspel där spelaren kontrollerar ett rymdskepp och bekämpar vågor av fiender. Spelet är utvecklat med MonoGame-ramverket, en öppen källkod-implementation av Microsoft XNA.

## Funktioner

- **Intensivt spelupplägg**: Styr ditt rymdskepp och skjut ned fiender
- **Poängsystem**: Samla poäng för varje besegrad fiende
- **Olika fiender**: Möt en variation av fiendeskepp med olika beteenden
- **Nivåer och svårighetsgrader**: Ökande utmaning allteftersom du avancerar
- **Powerups**: Samla förbättringar för ditt skepp
- **Kollisionshantering**: Realistiska kollisioner mellan spelobjekt

## Teknisk stack

- **Utvecklingsmiljö**: MonoGame/XNA
- **Programmeringsspråk**: C#
- **Grafikhantering**: MonoGame Content Pipeline
- **Ljudmotor**: MonoGame Audio Engine

## Installation och körning

### Förutsättningar

- Visual Studio 2019 eller senare
- .NET Framework eller .NET Core
- MonoGame-ramverket (senaste versionen)

### Steg för att komma igång

1. Klona repot
   ```bash
   git clone https://github.com/Linueker/TucSpaceShooter.git
   cd TucSpaceShooter
   ```

2. Öppna lösningen i Visual Studio
   ```
   TucSpaceShooter.sln
   ```

3. Återställ NuGet-paket om det behövs

4. Bygg och kör projektet genom att trycka på F5 eller använda "Start Debugging" från Debug-menyn

## Kontroller

- **Piltangenter** eller **WASD**: Förflytta rymdskeppet
- **Mellanslag**: Skjut
- **Escape**: Pausa spelet
- **R**: Starta om spelet vid Game Over

## Spelmekanik

### Spelmål
Målet med TucSpaceShooter är att överleva så länge som möjligt medan du besegrar fiender och samlar poäng. Spelaren styr ett rymdskepp och måste undvika kollisioner med fiender samtidigt som de skjuter ned dem.

### Poängsystem
- Olika fiender ger olika poäng baserat på deras svårighetsgrad
- Bonuspoäng kan erhållas genom att samla speciella objekt
- Höga poäng registreras i High Score-listan

### Liv och Skada
- Spelaren börjar med ett visst antal liv
- Kollision med fiender eller deras vapen minskar spelarens liv
- Spelet är slut när alla liv är förlorade

## Utvecklingsfokus

Detta projekt fokuserar på att demonstrera:
- Speluveckling med MonoGame/XNA-ramverket
- Objektorienterad design för spelmekanik
- Kollisionsdetektering och fysikeffekter
- Spelstatushantering och användarinput

## Bidragande

Om du vill bidra till projektet, vänligen följ dessa steg:

1. Forka repositoriet
2. Skapa en feature branch (`git checkout -b feature/ny-funktion`)
3. Commit dina ändringar (`git commit -m 'Lägg till ny funktion'`)
4. Push till branchen (`git push origin feature/ny-funktion`)
5. Öppna en Pull Request

## Licens

Detta projekt är licensierat under MIT-licensen.

## Erkännanden

- MonoGame-teamet för deras utmärkta spelramverk
- Bidragsgivare till open source-tillgångar som använts i projektet

---

Utvecklat under feb-mars 2024 av Grupp 6, Systemutvecklare .NET klass 2023-2025, TUC Yrkeshögskola, Linköping
