# Desarrollo del videojuego “Bebé pintor vs el mundo”
<img src="https://64.media.tumblr.com/tumblr_m9wk2ozqr81rfjowdo1_500.gifv" width="30px"> Por Esteban Mendez y Sofía Salazar
 <img src="https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/974544b9-b3dc-4b0e-ac69-25079f592946/dbth2oe-7702bea4-a96d-4628-9354-20296085f19e.gif?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzk3NDU0NGI5LWIzZGMtNGIwZS1hYzY5LTI1MDc5ZjU5Mjk0NlwvZGJ0aDJvZS03NzAyYmVhNC1hOTZkLTQ2MjgtOTM1NC0yMDI5NjA4NWYxOWUuZ2lmIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.TBhMwfzv4zcecpyHQj5oxj7onDHDuBLVWSarc20AhQM" width="30px">
 #
 Para comenzar, el juego se está desarrollando en el programa Unity. El juego que se está desarrollando es un juego sencillo, básicamente es un bebé pintor que presentará su obra maestra a su madre, sin embargo, hay una pintura que se hizo malvada, la roja, y esconde todas las demás pinturas para que no pueda terminar su obra. La misión del bebé, es simplemente encontrar las pinturas para poder terminar su obra maestra.

Para comenzar a desarrollar el juego en sí, utilizamos las herramientas que se describen a continuación:


  <h3>-Definiciones y utilidades- </h3>

 Primero abordaremos las herramientas del área de programación, que son las que se mencionan a continuación.

El void, es una función que no retornará ningún valor, y es usada constantemente para que las funciones que usaremos realicen alguna acción. Con esto dicho, primeramente, se usaron estos iniciadores para las funciones que más usamos en el juego.

Primeramente, el Awake. Al igual que el OnEnable y el Start, nos sirve para poder inicializar un gameObject. La descripción que nos da Unity en su manual en la versión 2019.4 dice: “Awake se llama cuando la instancia del script está siendo cargada.” Esto quiere decir que el Awake se llama siempre al principio de todo.
También, utilizamos el método Start, que en Unity, nos ayudará a que alguna acción se realice al inicio del juego. La diferencia entre el start y el awake según French, J. ( 2020) es la siguiente: “La función awake es la primera que se llama y, a diferencia de Start, llamará aunque el componente del script esté deshabilitado”.

Se usó igualmente la función Update, que como lo dice su nombre, nos ayudará a actualizar contenidos y funciones que vayamos usando.

El fixed update, igual que el update, realizó las mismas funciones, sin embargo, este lo usamos para cosas que se necesitan en tiempo real.

El late update sirve para lo mismo que el fixed update o el update normal, solamente que actualiza las funciones después de llamar a los otros métodos de actualización.

Una de las más grandes diferencias entre los métodos anteriores es el tiempo de ejecución, que según Çamönü, I. (2019): “Después del FixedUpdate(), el método Update() es llamado. Finalmente,el método LateUpdate() es ejecutado.”

OnDrawGizmos Selected se utiliza para dibujar un gizmo en un objeto, solo si el objeto está seleccionado se dibuja. 

El game manager es el responsable de varias acciones que se realizan en el juego, un ejemplo claro de su uso es el que da Rincón, J.G. (2022): “Es el responsable de iniciar y terminar el juego, llevar los contadores y crear los objetos tales como asteroides y otros enemigos.”

El game inputs básicamente ayuda para definir los ejes que se utilizarán en el juego, por ejemplo, los que se utilizaron para programar el movimiento del personaje.

El AudioSource, como su nombre lo indica, sirve para poder agregar audios, en nuestro caso lo utilizamos para los pasos del personaje, los sonidos de salto, etc.

El animation y el animator se utilizaron para hacer las animaciones individuales y hacer el árbol de animaciones, respectivamente.

El IEnumerator fue usado para alojar las corrutinas que se hicieron para el juego. Según Moon, A. (2021): “ Un IEnumerator no simplemente ‘devuelve’ el tipo de datos como lo haría en un método normal. En cambio, ‘cede’ un valor que hace que la ejecución de la lógica se detenga en su lugar; puede reanudarse más tarde.”

El packageManager, se utiliza para descargar contenidos que sean de alta necesidad para el juego, como lo es el Cinemachine, 2D Tilemap Editor, que sirvió para poder diseñar el nivel mediante las tiles que se usaron, y el 2D sprites, para poder manejar los sprites 2D de la manera óptima.

Las librerías, también fueron de vital importancia para la realización del proyecto, con ellas, se agregaron utilidades al proyecto tal como el sceneManagement, el TMPro, y la UI.

  
 
<h3>-PlayerScript-</h3>

En Awake inicializamos aquellos objetos esenciales para el correcto funcionamiento del personaje. En este se incluye la inicialización del GameInput, Rigidbody2D, SpriteRenderer, Animator y AudioSource.
Utilizamos las funciones OnEnable y OnDisable para inicializar o deshabilitar la función del GameInput respectivamente.
En el void Start inicializamos aquellas funciones u objetos que tenían un peso jerárquico menor o evoluciones al de los posicionados en el Awake. Dentro de estas se encuentra la declaración del SetPlayer en el GameManager, las funciones de inicio o cancelación del salto y caminar y la adquisición de valor de la variable walkTimerLimit a la de walkTimer.

El Update fue utilizado para estar comprobando en cada cuadro algunas condiciones, como por ejemplo, se consultaba si el personaje estaba tocando el piso o no para cancelar la animación de caminar. También para actualizar el valor de la variable que permite cambiar de orientación el sprite del personaje y, por último, comprobar la vida del mismo.
FixedUpdate al entrar una vez cada cuadro, hace más fluída la interpretación del movimiento del personaje añadiendo al Rigidbody el valor resultante de:

      (Vector2.right * Axis.x * speed * Time.fixedDeltaTime)
      
Antes de hablar de las funciones de movimiento, hay que establecer que se utilizaron los GameInputs al que se le añadieron acciones para interpretar los movimientos del jugador. En el plano X se registraron las teclas A, D, Flecha Izquierda y Flecha Derecha. En el plano Y se registraron las teclas S, W, Flecha Abajo y Flecha Arriba. Por último, para el salto se registró la Barra Espaciadora.

Para el movimiento lineal del personaje se inicializó un Vector2 llamado axis al que se le implementan las funciones del GameInput en x y y correspondientes.
Para la orientación del sprite del personaje se inicializó un bool llamado FlipSprite en el que se detecta si el valor de x en el Axis es mayor a 0 entonces el bool es false (queda en  orientación a la derecha). Pero si detecta que es menor a 0 entonces el bool es true (queda en orientación hacia izquierda). Esto como se mencionó anteriormente, se actualiza en el Update.

Para implementar la función de salto en el personaje, se utilizaron 2 funciones llamadas Jump y JumpCanceled. En la función Jump se comprobaba si el personaje estaba tocando el suelo mediante un Bool llamado IsGrounding. Este bool respondía al contacto de un Raycast implementado en dirección hacia abajo detectando objetos pertenecientes a la Layer de Ground o al bool touchingHead que comprueba si se está encima de un enemigo. Si IsGrounding es true, se aplica una fuerza positiva vertical al RigidBody. Esta fuerza es aplicada por:

    Vector2.up * jumpForce, ForceMode2D.Impulse
    
Dentro también se inicializan el audio de salto, la animación de salto y la declaración de falso al bool touchingHead.
JumpCanceled simplemente setea la velocidad del Rigidbody en el salto en 0.
El LateUpdate nos sirvió para setear variables en el Animator para activar las animaciones elaboradas.
Para la implementación del audio al caminar fue implementado un IENumerator y 2 funciones: WalkRoutine, StartWalk y StopWalk respectivamente.
Primero en la función StartWalk si IsGrounding es true, se inicializa la variable walk adjudicando el IEnumerator WalkRoutine para posteriormente mandarlo a llamar.
En WalkRoutine se inicializa un ciclo que comprueba si IsGrounding se estaba cumpliendo, si esto correspondía entonces se reproduce el audio con la ayuda del AudioSource por un tiempo limitado puesto por un:

    yield return new WaitForSeconds(walkTimeLimit)
    
Por último el StopWalk para detener la corrutina.
Utilizamos también la función OnTriggerEnter2D para detectar colisiones con otros objetos dentro de la escena comparando el tag que estos tienen y así aplicar una función dependiendo el objeto.
Por último, se implementaron 2 IEnumerator para definir un tiempo de espera en la inicialización de los audios de muerte y de completar el nivel.


<h3>-EnemyScript-</h3>

Al igual que en el PlayerScript, en Awake inicializamos aquellos objetos esenciales para el correcto funcionamiento del personaje. En este se incluye la inicialización del Rigidbody2D, SpriteRenderer, Animator y una variable local para llamar variables o funciones públicas de PlayerScript.
En el Start se inicializa la Inteligencia del personaje. Esta inteligencia está a cargo de 3 funciones y 2 IEnumerator: StartIA, StartIdling, y StartPatroling, PatrolingRoutine y IdlingRoutine respectivamente.
Primero en StartIA se añadió el PatrolingRoutine a patroling y se inició. Aquí es donde se seteo el valor al float de la animación para comenzar la de patrulla. Posteriormente se creó un ciclo en el que se instauraba el movimiento del personaje, y se comprobaba si se encontraba con el player para atacar o encontraba el límite que lo detenía.

Para que el enemigo ataque, primero se comprueba mediante un Raycast posicionado a la izquierda y otro a la derecha del enemigo, la detección de objetos en la layer del Player. La separación de estos es para identificar si el sprite del enemigo tendrá que cambiar su orientación para atacar al player o no. Esto fue interpretado en el IEnumerator Attacking Routine en el que se comprueba la dirección del ataque y la dirección del enemigo.

Para la detección de los límites de patrullaje, se creó un bool que mediante un Raycast abarcando la izquierda y derecha del enemigo, comprueba si se está colisionando con los objetos ubicados en la layer de Limit. Estos objetos dentro de la escena son cuadrados puestos premeditadamente con los sprites eliminados y ubicados en la layer de limit.

Cuando detecta el limite, entra el IEnumerator IdlingRoutine en el que se setea la animación de idle, se espera un momento y vuelve a iniciar el patrullaje mediante el StartPatroling que ajusta la dirección del sprite con

    direction = direction == Vector2.right ? Vector2.left : Vector2.right
    
 
Posteriormente vuelve a iniciar el IEnumerator PatrolingRoutine.

Ya por último, en un OnTriggerEnter2D correspondiente a un collider que se posicionó sobre la cabeza del enemigo, se detecta la colisión con el tag Player para setear el valor true en la variable TouchingHead del PlayerScrip para que en este permita que el player pueda volver a saltar.


<h3>-GameManager-</h3>

En el Awake determinamos la instancia y el DontDestroyOnLoad.

Por el SceneManager se utilizó la función OnSceneLoaded para inicializar el player y enemy en la escena correspondiente. Después se adjudicaron a variables públicas estas mismas.

<h3>-ArtScript-</h3>

Con este script se le añadió un valor a los pickup del juego para poder manejar una puntuación. Se declaró una variable local int con 1 de valor y posteriormente se hizo pública con la variable GetPoints.

<h3>-ArtScore-</h3>

Este script fue añadido al UI con la utilización del TextMeshPro. Se declara una variable pública de int llamada score con valor 0.

Se creó una función pública llamada AddPoints que recibirá un int llamado points. Dentro se aplica el valor de points en score y se imprime en la pantalla.

<h3>-HealthScript-</h3>

Este script fue añadido al UI con la utilización del TextMeshPro. Se declaró una variable int pública llamada healthUI con valor de 5. Esta variable se utilizó en una función llamada RemoveHealth que recibirá un int llamado damage, el cual se le resta al healthUI para posteriormente imprimirlo en pantalla.


<h3>-MenuScript-</h3>

Con este script se manejaron los botones del UI. Corresponde a un total de 5 los cuales fueron: play, credits, salir, a jugar y regresar.
Estos se relacionaron al script gracias al SerializeField puesto en cada uno que permitió arrastrar manualmente al correspondiente para conectarlos.
Cada botón llama a un IEnumerator que instaura un pequeño tiempo de espera y procede a ejecutar su función dependiendo el seleccionado.


<h3>-Animaciones-</h3>

Para la generación de animaciones se empleó el Animation. En él se crearon los frames con los sprites para cada animación. Estos sprites fueron elaborados en Aseprite.
El resultado de las animaciones se ordenaron en el Animator.


<h3>-Animator Player-</h3>

El animator entra conectado a un blendtree llamado Movement en el que se manejan las animaciones de idle y walk. Desde el Any State se conecta la animación de jump y damage. La animación de jump responde al trigger jump mientras que la de damage responde al trigger damage. Estos trigger son manejados desde el script correspondiente anteriormente detallado. Estas dos animaciones hacen la conexión de regreso al Movement.

<h3>-Animator Enemy-</h3>

Algo parecido al del Player, cuenta con un blendtree llamado patrol en el que se manejan las animaciones de idle y walk. Desde el Any State se conecta la animación attackEnemy que responde al trigger attack. Por último, regresa la conexión al patrol. Estas variables son manejadas desde los scripts correspondientes anteriormente detallados. 



#

-*Referencias*-

    Unity Technologies. (2020). MonoBehaviour.Awake(). Unity Documentation. https://docs.unity3d.com/es/current/ScriptReference/MonoBehaviour.Awake.html

    French, J. (2020, marzo 5). Start vs Awake in Unity. gamedevbegginer. https://gamedevbeginner.com/start-vs-awake-in-unity/

    Rincón, J. G. (2022, febrero). Game Manager. jairogarcíarincón. https://jairogarciarincon.com/clase/unity-videojuego-estilo-space-shooter/game-manager

    Çamönü, İ. (2019, noviembre 16). The difference between Update, FixedUpdate and LateUpdate. codinblack. https://www.codinblack.com/the-difference-between-update-fixedupdate-and-lateupdate/

    Moon, A. (2021). Aprende C# con Unity - Corrutinas. Antonio Moon´s. https://moonantonio.github.io/post/2018/csharpunity/007/





