window.addEventListener('load', function () {

    var display = document.getElementById("display");
    var btnGo = document.getElementById("btnGo");

    /* Initialiser l'array des rules avec le back : 

        1) Faire appel à l'API pour récupérer l'ensemble des rules en JSON; 

        2) Boucler en back sur la liste des rules;

        3) Compléter 

    */
   
    var initRules = [];  

    /*
    for(let i = 0; i < [TAILLE DE LA LISTE DE RULES DANS LE BACK]; i++){
        rules.add([RULE DU BACK]);
    }
    */

    //Temporaire pour tester
    initRules = ["Faire 3 bandes avec la blanche", "Faire bouger la blanche", "Rentrer la blanche sans toucher une autre boule."];


    function WriteInDisplay(text){
        display.innerHTML = text;
    }

    function DisplaysRandomRule(){
        //On choisit une règle au hasard dans la liste des règles
        let rule = initRules[Math.floor(Math.random() * initRules.length)];

        WriteInDisplay(rule);
    }

    btnGo.addEventListener("click", function () { DisplaysRandomRule(); });
    DisplaysRandomRule();
  });