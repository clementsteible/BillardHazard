//JS page Home/Index/New game

var redBallSection = document.getElementById('redBallSection');
var redShadow = document.querySelector('.redBall');
var redRadioBtn = document.getElementById('redTeamFirst');

var yellowBallSection = document.getElementById('yellowBallSection');
var yellowShadow = document.querySelector('.yellowBall');
var yellowRadioBtn = document.getElementById('yellowTeamFirst');

var btnSubmit = document.getElementById('btnSubmit');

//Visual choose color team
function ClickOnYellowBall() {
    redShadow.classList.add('unselectedBall');
    yellowShadow.classList.remove('unselectedBall');

    yellowRadioBtn.click();
}

//Visual choose color team
function ClickOnRedBall() {
    yellowShadow.classList.add('unselectedBall');
    redShadow.classList.remove('unselectedBall');

    redRadioBtn.click();
}

redBallSection.addEventListener("click", function () { ClickOnRedBall(); });
yellowBallSection.addEventListener("click", function () { ClickOnYellowBall(); });

//Keyboard commands
window.addEventListener("keydown", function (event) {

    switch (event.which) {
        //Enter
        case 13:
            btnSubmit.click();
            break;
        //Flèche gauche
        case 37:
            yellowBallSection.click();
            break;
        //Flèche droite
        case 39:
            redBallSection.click();
            break;
    }
});