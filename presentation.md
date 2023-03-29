---
marp: true
---

<style>
    {
        font-family: Consolas;
    }
</style>

# Generování kódu v .NET/C#

### NALEJVÁRNA 2023
Jiří Paukert (jip@ciklum.com)

---

# Cíle přednášky 
- Stručné seznámení s problematikou
- Ukázka praktického využití Roslyn Source Code generatoru

---

# Možnosti využití 
- Alternativa k reflexi, pokud vše znám v době kompilace
- Sdílení codebase napříč technologiemi a formáty
- Vyhnutí se psaní boilerplate codu (mapování, testy, apod.)

---

# Způsoby generování kódu
- Vlastní řešení
- T4 - Text Template Transformation Toolkit (.NET 2)
- Roslyn source generators (.NET 5) (demo)
- Roslyn incremental generators (.NET 6)

---

# Text Template Transformation Toolkit - T4
- Poměrně známá technologie
- Je možné generovat jakýkoli řetězec
- Horší integrace s .NET core
- Špatná podpora v IDE
- Nepřehledný kód
- Za sebe se dnes doporučuji se spíše vyhnout

---

# Roslyn source generators
- Umožňují generování kódu v C# a VB
- V době kompilace umožňují procházet analyzovat syntaktické stromy
- Jsou vlastně jen další analyzer, dobrá integrace s IDE
- Mohou zpomalit kompilaci, spouští se s každou kompilací

---

# Roslyn source generators

![width:1620px height:auto](images/diagrams-Roslyn-code-geneartor-flow.drawio.svg)

---

# Incremental source code generators
- Umožňují generování kódu v C# a VB
- Vyžadují přinejmenším .NET 6
- Spouští se v průběhu editace kódu
- Při analýze pracují s předchozím vygenerovaným kódem

---

# Demo - Představení ukázkové aplikace

![width:1620px height:auto](images/diagrams-Roslyn-code-geneartor-demo-app.drawio.svg)

--- 

# Demo - Představení generátoru
- Obyčejný projekt v `.netstandard2.0`
- Referencovaný jako `Analyzer`
- Přirozeně není součástí build outputu
- Referencuje `Microsoft.CodeAnalysis.CSharp` a `Microsoft.CodeAnalysis.Analyzers`

--- 

# Demo - Představení generátoru
![width:1620px height:auto](images/diagrams-Roslyn-code-generator-flow.drawio.svg)

--- 

![width:320px height:320px](images/qr.png)
<!-- _class: lead -->

### https://github.com/paukertj/Nalejvarna2023