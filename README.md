# Projektni zadatak 8.1–Trka bolida Formule 1
## Modelovanje statičke 3D scene (prva faza): 
* Uključiti testiranje dubine i sakrivanje nevidljivih površina. Definisati projekciju u perspektivi sa fov=50, near=1, far=20.000 i viewport-om preko celog prozora unutar Resize metode.
* Koristeći AssimpNet bibloteku i klasu AssimpScene, importovati dva različita modela bolida Formule 1.
* Modelovati sledeće objekte: 
	* podlogu koristeći GL_QUADS primitivu, 
  * stazu korišćenjem GL_QUADS primitive, i
  * zaštitne zidove sa leve i desne strane staze, korišćenjem instanci Cube klase
* Ispisati 2D tekst svetlo plavom (cyan) bojom u donjem desnom uglu prozora (redefinisati viewport korišćenjem glViewport metode). Font je Arial, 14pt, underline. Tekst treba biti oblika:
  * Predmet: Racunarska grafika 
  * Sk.god: 2020/21.
  * Ime: <ime_studenta>
  * Prezime: <prezime_studenta>
  * Sifra zad: <sifra_zadatka>
