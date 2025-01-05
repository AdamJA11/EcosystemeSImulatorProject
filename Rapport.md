# AvaloniaGameBase

Dans ce projet Avalonia UI, je compte realiser une application simulant un ecosysteme 

Le code fournit tente de simuler le fonctionnement d un ecosysteme de base 

Diagramme d'activite :


![image](https://github.com/user-attachments/assets/077df3b3-3993-465e-ba00-f2192009e7aa)


Diagramme de sequence:


![image](https://github.com/user-attachments/assets/d2d0c1a7-2a12-4178-816b-6d2f22cf2df6)



Diagramme de classe:


![image](https://github.com/user-attachments/assets/7bbaba1a-ca02-47a7-a900-5c31ea581cf5)





principe Solide :

1. **Single Responsibility Principle (SRP)**

-Les classes et les méthodes ne sont responsables que d'une chose ce qui respecte le principe solide S : Single Responsibility Principle. En effet comme vous pouvez le voir dans mon diagramme de classe chaque classe porte un nom bien precis (son role) elle est ensuite utilise dans d'autres classe pour accomplir son Objectif comme la classe BaseLifeCycleControl qui gere le cycle de vie de chaque objet present (mort/vie) (frais/pourri) etc.. . Cette classe est basiquement un timer a 2 entre LifeDuration et HungerLife l'un s'ecoulant apres l'autre.



2. **Open/Closed Principle (OCP)** :
   - Les classes sont ouvertes à l'extension mais fermées à la modification. Par exemple, la classe Ball peut être étendue avec de nouvelles fonctionnalités sans modifier le code existant, grâce à l'utilisation de composants comme VisionChase ou Contactzone

