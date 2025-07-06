# Przegląd gry

Turowa gra symulacyjna, w której gracz zarządza akcjami firm. Celem gry jest maksymalizacja majątku poprzez kupowanie i sprzedawanie aktywów, reagowanie na losowe zdarzenia rynkowe oraz plotki wpływające na rynek.

## Instrukcja

- W każdej turze możesz kupować lub sprzedawać akcje różnych firm.
- Zwracaj uwagę na pojawiające się plotki rynkowe — mogą one zmieniać wartość akcji różnych firm
- Po zakończeniu tury zobacz jak zmieniła się sytuacja na rynku
- Gra kończy się po 20 turach — wyświetlone zostanie podsumowanie: Twój majątek, wielkość przychodu/straty, firmy, których akcje przyniosły największy i najmniejszy zysk, oraz chronologiczny log wydarzeń i transakcji.

### System eventów w grze

W grze każdy event (zdarzenie losowe) implementuje interfejs `IRandomEvent`, który wymusza określenie prawdopodobieństwa (`probability`) oraz metodę wykonującą skutki eventu (`Apply()`). 

Wywoływaniem eventów zarządza klasa `RandomEventManager`, która na podstawie losowania i warunków gry decyduje, czy dany event powinien się wydarzyć. Informacja o każdym wywołanym evencie trafia do systemu logowania, co umożliwia prezentację pełnej historii rozgrywki.

Każdy rodzaj eventu jest osobną klasą implementującą konkretny efekt eventu. Zaimplementowane są trzy: pierwszy mający wpływ na jedną firmę, drugi mający wpływ na cały sektor, trzeci zależny od czynności gracza.

Cały system jest modularny, łatwy do rozbudowy i umożliwia dodawanie nowych typów eventów bez konieczności modyfikowania już istniejącej logiki gry.

### System plotek

W grze zaimplementowano system plotek, który wprowadza dodatkowe, losowe zdarzenia wpływające na wybrane firmy. Plotki wczytywane są z pliku JSON, a każda z nich opisuje tekst plotki i konkretne efekty dla każdej z firm (np. wzrost lub spadek kursu). W każdej rundzie losowana jest jedna plotka, która może zmienić sytuację na rynku.

Docelowo system plotek może zostać zintegrowany z systemem eventów pod kątem systemów logowania, co pozwoli prezentować graczowi pełną historię wszystkich wydarzeń w grze w jednym miejscu.
