# AvaloniaGameBase

Dans ce projet Avalonia UI, je compte realiser une application simulant un ecosysteme 

Pour cela differente classe son utilise: 

La classe BaseLifeCycleControl qui est basiquement un chrnometre a 2 entre. Elle va s'occuper de gerer le cycle de vie de la classe Prey, Meat, Ball (chasseur), Plant

La classe MainWindowViewModel s'occupe de l'affichage sur la fenetre des differentes classe selon certaine condition specifie

La classe Meat reprente representant la viande elle utilise la classe BaseLifeCycleControl pour envoyer une valeur true lorsque le timer c'est ecoule indiquand a la classe MainWindowViewModel de remplacer la viande par de la matiere organique

La classe Ball quand a elle gere le mouvement de mon predateur tout les 2 seconde il change de direction et de vitesse de maniere aleatoire et ceux grace a la classe BaseLifeCycleControl

La classe Prey utilise la classe Ball pour gerer ces mouvement evitant ainsi de copier du code. Note qu'elle est semblable a la classe ball 

La classe Rubish est plutot basique elle ne fait que prendre un position et afficher la matiere organique selon cette position grace a la classe MainWindowViewModel

La classe Chase quand elle va representer le reperage et la probabilite de capturer une proie par le predateur. 

Diagramme d'activite :


![image](https://github.com/user-attachments/assets/077df3b3-3993-465e-ba00-f2192009e7aa)


Diagramme de sequence:


![image](https://github.com/user-attachments/assets/d2d0c1a7-2a12-4178-816b-6d2f22cf2df6)



Diagramme de classe:


![image](https://github.com/user-attachments/assets/7bbaba1a-ca02-47a7-a900-5c31ea581cf5)





principe Solide :

-Les classes et les méthodes ne sont responsables que d'une chose ce qui respecte le principe solide S : Single Responsibility Principle. En effet comme vous pouvez le voir dans mon diagramme de classe chaque classe porte un nom bien precis (son role) 







