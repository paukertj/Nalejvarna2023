# Nalejvárna 2023
Prezentace k přednášce Nalejvárna 2023 ([Bratislava](https://www.meetup.com/nalejvarna/events/292127491/), [Hradec Králové](https://www.meetup.com/nalejvarna/events/292127406/)). Prezentace byla vytvořena v [Marp](https://marp.app/). :warning: Ukázky v kódu jsou skutečně jen ukázky, jsou napsané nainvně a nebezpečně. :warning:

## Obsah
- Přednáška o možnostech generování kódu 
- Obsahuje ukázku vylepšení [Autmapperu](https://automapper.org/) za použití Roslyn Code Generatoru

## Zajímavé odkazy
- [Dokumentace k Roslyn Code Generatorům](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview)
- [Dokumentace k Incremental Roslyn Code Generatorům](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md)
- [Andrew Lock, série článků k Incremental Roslyn Code Generatorům](https://andrewlock.net/creating-a-source-generator-part-1-creating-an-incremental-source-generator/)
- [Stefan Pölz, JetBrains .NET Days 2022 vytvoření Incremental Roslyn Code Generatorům](https://www.jetbrains.com/dotnet/guide/tutorials/dotnet-days-online-2022/lets-build-an-incremental-source-generator-with-roslyn/)

## Přehled pojmů
- Roslyn, Open-source .NET compiler platform
- Trivia, Whitespace, komentáře a další informace, které nejsou součástí samotného kódu
- SyntaxTree, Reprezentuje strom syntaktické analýzy kódu, obsahuje SyntaxNode objekty a MetadataReference objekty
- SyntaxNode, Základní jednotka syntaktické analýzy, představuje syntaktickou jednotku jazyka jako například deklarace proměnné nebo if podmínka
- SyntaxToken, Základní jednotka syntaktické analýzy. Jedná se o objekt, který reprezentuje jednotlivé tokeny v textu zdrojového kódu, například klíčová slova, identifikátory, číselné literály, operátory a další. SyntaxToken obsahuje informace o typu tokenu, jeho pozici v rámci textu zdrojového kódu, případně další metadata, jako například hodnotu literálu. SyntaxToken je součástí syntaktického stromu (SyntaxTree) a lze s ním provádět další operace, jako například získávání rodiče, potomků, či nahrazování tokenu novým tokenem.
- Semantic model, Reprezentuje sémantickou strukturu kódu a umožňuje přístup k typům, memberům, anotacím a dalším informacím, které nejsou přímo viditelné v kódu.