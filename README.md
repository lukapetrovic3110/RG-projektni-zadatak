# Projektni zadatak 8.1–Trka bolida Formule 1
## Modelovanje statičke 3D scene (prva faza): 
* Uključiti testiranje dubine i sakrivanje nevidljivih površina. Definisati projekciju u perspektivi sa fov=50, near=1, far=20.000 i viewport-om preko celog prozora unutar Resize metode.
* Koristeći AssimpNet bibloteku i klasu AssimpScene, importovati dva različita modela bolida Formule 1.
* Modelovati sledeće objekte: 
	* podlogu koristeći GL_QUADS primitivu 
    * stazu korišćenjem GL_QUADS primitive
    * zaštitne zidove sa leve i desne strane staze, korišćenjem instanci Cube klase
* Ispisati 2D tekst svetlo plavom (cyan) bojom u donjem desnom uglu prozora (redefinisati viewport korišćenjem glViewport metode). Font je Arial, 14pt, underline. Tekst treba biti oblika:
  * Predmet: Racunarska grafika 
  * Sk.god: 2020/21.
  * Ime: <ime_studenta>
  * Prezime: <prezime_studenta>
  * Sifra zad: <sifra_zadatka>
  ### Fotografija nakon završene prve faze
  <img src="foto/Trka bolida Formule 1 OpenGL Projekat.PNG" width="900">
## Definisanje materijala, osvetljenje, tekstura, interkacije i kamere u 3D sceni (druga faza):
* Uključiti color tracking mehanizam i podesiti da se pozivom metode glColor definiše ambijentalna i difuzna komponenta materijala.
* Definisati tačkasiti svetlosni izvor svetlo-žute boje i pozicionirati ga gore-levo u odnosu na centar scene (na negativnom delu horizontalne i pozitivnom delu vertikalne ose). Svetlosni izvor treba da bude stacionaran (tj. transformacije nad modelom ne utiču na njega). Definisati normale za podlogu i stazu. Uključiti njihovu normalizaciju.
* Za teksture podesiti wrapping da bude GL_REPEAT po obema osama. Podesiti filtere za teksture da budu linearnofiltriranje. Način stapanja teksture sa materijalom postaviti da bude GL_DECAL.
* Stazi pridružiti teksturu asfalta. Zaštitnim zidovima pridružiti teksturu metalne zaštitne ograde. Definisati koordinate tekstura.
* Podlozi pridružiti teksturu šljunka (slika koja se koristi je jedan segment šljunka), pritom obavezno skalirati teksturu (shodno potrebi). Skalirati teksturu korišćenjem Texture matrice. Definisati koordinate tekstura.
* Pozicionirati kameru, tako da „gleda“ na scenu spreda i odgore (ne previše izdignuta od podloge). Koristiti gluLookAt() metodu.
* Pomoću ugrađenih WPF kontrola, omogućiti sledeće:
  * transliranje desnog bolida, po horizontalnoj osi za zadatu vrednost
  * rotiranje levog bolida oko vertikalne ose za zadati ugao za zadatu vrednost
  * izbor boje ambijentalne komponete refletorskog svetlosnog izvora
* Omogućiti interakciju korisnika preko tastature: sa F4 se izlazi iz aplikacije, sa tasterima I/K vrši se rotiranje za 5 stepeni oko horizontalne ose, sa tasterima J/L vrši se rotacija za 5 stepeni oko vertikalne ose, a sa tasterima +/- približavanje i udaljavanje od centra scene. Ograničiti rotaciju tako da se nikada ne vidi donja strana horizontalne podloge i da scena nikada ne bude prikazana naopako.
* Definisati reflektorski svetlosni izvor (cut-off = 45 stepeni) bele boje iznad automobila.
* Način stapanja teksture sa materijalom za modele oba bolida postaviti na GL_MODULATE
* Kreirati animaciju trke bolida. Animacija treba da sadrži sledeće:
  * Leva formula je od starta dominantna i pobeđuje (stiže prva do cilja)
  * Kamera počinje tako što se zumira na stazu, a onda je kadrira odozgo (iz ptičije perspektive)
  * Animaciju realizovati transformacijom sveta ili korišćenjem gluLookAt() metode. U toku animacije, onemogućiti interakciju sa korisnikom (pomoću kontrola korisničkog interfejsa i tastera). 
  * Animacija se može izvršiti proizvoljan broj puta i pokreće se pritiskom na taster V. 
  ### Fotografija nakon završene druge faze
  <img src="foto/Trka bolida F1 Projekat 2020_2021.PNG" width="900">
