# CSharpAdventures opis 

**[01-02-2019]** 

W ramach rozwoju mini projektu **ChecksumForEanCodeAft** odkrylem blad w koncowym wyliczeniu sumy kontrolnej. W sytuacjach kiedy
reszta z dzielenia (x % 10) byla rowna 0, to suma kontrolna wyliczana byla jako 10. BLAD! Naprawiono.


## Project ChecksumForEanCodeAft
Aft (after few thoughts) czyli po paru przemyśleniach. Jest to zmodyfikowana wersja projektu ChecksumForEanCode. Inne podejście do zagadnienia, prostszy kod. Wyeliminowano kilka powtórzen w kodzie. 

_Jest to wersja, która będzie modyfikowana w ramach zabawy z kodem._


## Project ChecksumForEanCode
Jest to jedna z propozycji realizacji zadania z rozmowy kwalifikacyjnej. Rozwiązanie dot. wyliczenia cyfry kontrolnej w kodzie EAN-13. 
Jest to implementacja algorytmu zamieszczonego na http://ean-13.dlawas.com/index.php/budowa-ean13.html. 
Rozwiązanie nie jest idealne. Z pewnością wymaga kilku tweak’ów, ale na potrzeby wyłącznie poglądowe mogę uznać je za skończone.