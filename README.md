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

Diagramme d'activite : ![image](https://github.com/user-attachments/assets/fc002a39-16a6-4fc0-a46b-7941fa4860f4)



principe Solid 







