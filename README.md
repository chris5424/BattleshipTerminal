# BattleshipTerminal
Program symuluje grę między dwoma graczami.

Założenia przyjęte w projekcie:
	Plansza gry ma rozmiar 10x10
	Każdy z graczy posiada 5 statków:
		- Carrier - 5 pól
		- Battleship - 4 pola
		- Cruiser - 3 pola
		- Submarine - 3 pola
		- Destroyer - 2 pola
	Statki zostają rozmieszczone na planszach automatycznie przy zachowaniu pewnych ograniczeń:
		- Statki nie mogą się stykać bokami
		- Statki nie mogą się stykać rogami
		- Rotacja statku jest losowa
	Gameplay:
		- Grę rozpoczyna losowy gracz
		- Po trafieniu statku gracz ma kolejny strzał
		- Przy niecelnym trafieniu tura przechodzi na drugiego gracza
		- W przypadku zatopienia statku tura przechodzi na kolejnego gracza
		- Ruchy są wykonywane automatycznie co sekundę