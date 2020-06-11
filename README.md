# Projeto 3 - Trollcave, a roguelike adventure

## Grupo 11

|Nome|Número|GitHub|
|:-:|:-:|:-:|
|Luiz Santos|a21901441|JundMaster|
|Pedro Marques|a21900253|pmarques93|
|Gonçalo Verde|a21901395|MrVerdinsky|

## Tarefas realizadas no exercício

>Por ordem cronológica

|Gonçalo Verde|Luiz Santos|Pedro Marques|
|:-:|:-:|:-:|
|Add `Input`/ `Renderer`|Improve `Input`|Add `ObjectPosition`/`Enemy`/`PowerUp`|
|Add Initial Game Input|Add Movement Input|Add `Map` / `Render` / `Player`|
|Add Main Menu / Menu Input|Add Procedural Generation|Add GameLoop|
|Add Enemy Render|Procedural Generation - Enemies|Add Enemy Damage Collision|
|Add PowerUp Render|Procedural generation - PowerUps|Add PickUp Collision|
|Add Enemy Collision|Override Equals on `Position`|Add Enemy Movement|
|Add Sprites|Program run try-catch|Add Game Actions Information|
|Add Next Level|Implement saving system|Add `HighScoreManager`|
|Bug Fix|Bug Fix|Bug Fix|
|HTML Documentation|Add `Save`||

## Repositório git

[Repositório GitHub](https://github.com/MrVerdinsky/3-ProjetoLP)

## Descrição da solução

### Resumo de lógica implementada

Neste projeto, após a inicialização do programa, é criado um *game loop* que
contem um *level loop*. Estes loops vão criar os elementos do jogo e vão
terminar assim que o jogador acaba um nível (no caso do *level loop*), ou
assim que o jogador morre ou sai do jogo (no caso do *game loop*).

Para a resolução deste projeto criámos diferentes classes com responsabilidades
distintas. Optámos por dividir classes como por exemplo a `Renderer` da `Input`
para uma melhor organização. Através da `Position`, controlamos todos os
aspetos relativos à posição de qualquer elemento, utilizando a `Level` para
gerar procedimentalmente estes mesmos elementos. Utilizamos a classe base
`Position`, que serve de base para as sub-classes `Enemy`, `Player`, `PowerUp`,
e `Map`,  sendo que todas partilham da variável de posição.

### Algoritmos não triviais

A abordagem para controlar todas as posições, foi através de "tags", em que
para cada elemento que criamos no jogo, ativamos a variável que define
que tipo de elemento vai ser, por exemplo, para o jogador, `IsPlayer = true`.
Através de métodos criados na `Map`, nomeadamente `Occupy()` e `Free()`,
controlamos todas as posições do jogo, definindo assim, quais as posições
ocupadas, ou quais posições em que é possível andar, ou quais as posições que
contém algum elemento.

Para a movimentação do inimigo, começamos por verificar a distância entre um
inimigo e o jogador. Após isto, verificamos se o inimigo se pode mover para a
posição que está a confirmar, de modo a cortar a distância entre si e o jogador.
O inimigo move-se através de um número randomizado, alternando sempre a direção
em que se movimenta para o jogador, tal como a direção que escolhe quando
encontra um obstáculo. Utilizamos vários *try-catch* durante o código para
eliminar erros ao verificar posições fora do mapa.

### Diagrama UML
![Diagrama](diagrama_uml.png)

### Trocas de ideias/referências

> Dúvidas Gerais

- ["C# documentation",_Microsoft_, Microsoft 2020](
  https://docs.microsoft.com/en-us/dotnet/csharp)

> Leitura/Escrita de ficheiros

- ["C# Read and Write to a Text File" _Youtube_, uploaded by shad sluiter](
  https://www.youtube.com/watch?v=j6ShXTjG5fg&t)

> Criação de funções para a geração procedimental

- [Criação de funções](
  https://www.desmos.com/calculator/za0q7ec8yy)

> Criação do método para realizar *random weightned choices*

- [Bendersky, Eli."Weighted random generation in Python". _Eli Bendersky_](
  https://eli.thegreenplace.net/2010/01/22/weighted-random-generation-in-python)
  
