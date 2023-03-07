---
marp: true
---

<style>
    {
        font-family: Consolas;
    }
</style>

# Generování kódu v .NET/C#

NALEJVÁRNA 2023

---

# Možnosti využití 
- Alternativa k reflexi, pokud vše znám v době kompilace
- Sdílení codebase napříč technologiemi a formáty
- Vyhnutí se psaní boilerplate codu (mapování apod.)

---

# Způsoby generování kódu
- Vlastní řešení
- T4 - Text Template Transformation Toolkit (.NET 2)
- Roslyn source generators (.NET 5)
- Roslyn incremental generators (.NET 6)

---

# Text Template Transformation Toolkit - T4
- Poměrně známá technologie :heavy_plus_sign:
- Horší integrace s .NET core :heavy_minus_sign:
- Špatná podpora v IDE :heavy_minus_sign:
- Nepřehledný kód :heavy_minus_sign:
- Za sebe doporučuji se spíše vyhnout :heavy_minus_sign:

---

# Roslyn source generators

---

# Roslyn incremental generators

--- 


![width:320px height:320px](qr.png)
<!-- _class: lead -->

### https://github.com/paukertj/Nalejvarna2023