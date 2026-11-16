# Quiz Battle – Ninja Unicorns

Quiz Battle är ett frågespel för konsolen. Vi har skrivit det i C#. Det är 2–4 spelare som spelar på samma dator. De 10 bästa resultaten sparas på en topplista.

## Det här behövs

- .NET SDK 10
- Git

## Starta spelet

```bash
git clone https://github.com/fadahm1/QuizBattle.git
cd QuizBattle
dotnet build
dotnet run
```

Skriv `dotnet run` i mappen där `QuizBattle.csproj` ligger. Då hittar programmet filen `data/questions.json`. Man kan också öppna mappen i Visual Studio och trycka F5.

## Så spelar man

1. Välj 1 i menyn för att spela. 2 visar topplistan och 3 avslutar.
2. Skriv hur många ni är (2–4), era namn och hur många rundor ni vill spela (1–10). Namnen får inte vara tomma eller lika.
3. I varje runda får alla spelare en fråga. Man väljer en kategori eller slumpar.
4. Efter svaret får man veta om det var rätt eller fel.
5. När rundorna är slut visas resultatet och vem som vann. Resultatet sparas på topplistan.

Om man skriver fel, till exempel en bokstav istället för en siffra, får man ett meddelande och försöker igen. Spelet kraschar inte.

## Poäng

| Svårighet | Poäng |
|-----------|-------|
| Lätt      | 10    |
| Medel     | 20    |
| Svår      | 30    |

Fel svar ger 0 poäng.

Svarar man rätt flera gånger i rad får man extra poäng. Andra rätta svaret ger +5, tredje +10, fjärde +15 och femte och alla efter det +20. Ett fel svar startar om serien.

Flest poäng vinner. Är poängen lika vinner den som har flest rätta svar. Är det ändå lika delar man segern.

## Topplistan

Efter varje spel sparas namn, poäng, antal rätta svar och datum. Topplistan visar de 10 bästa. Högst poäng kommer först. Är poängen lika kommer den med flest rätta svar först.

## Filer

| Fil | Vad den gör |
|-----|-------------|
| `Program.cs` | Startar programmet |
| `Menu.cs` | Huvudmenyn och inmatning |
| `GameEngine.cs` | Själva spelet |
| `Player.cs` | En spelare med poäng |
| `IQuestion.cs` | Interface för frågor |
| `Question.cs` | Basklass för frågor |
| `MultipleChoiceQuestion.cs` | Flervalsfråga |
| `TrueFalseQuestion.cs` | Sant eller falskt-fråga |
| `QuestionManager.cs` | Läser frågorna från filen |
| `HighscoreEntry.cs` | Ett sparat resultat |
| `HighscoreManager.cs` | Sparar och visar topplistan |
| `data/questions.json` | Frågorna |
| `data/highscores.json` | Topplistan (skapas när man har spelat) |

## Gruppen

Vi är gruppen Ninja Unicorns.

- Fadumo : frågor och inläsning av `questions.json`
- Adiam: spelet, poäng och spelare
- Ismete: meny, topplista, sparande i JSON och README