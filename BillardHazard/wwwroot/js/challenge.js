// README : decodedJson is declared in html balises scripts
// JS page Challenge

window.addEventListener('load', function () {

    var rules = JSON.parse(decodedJson);

    var display = document.getElementById("display");
    var btnDice = document.getElementById("btnDice");

    function WriteInDisplay(text) {
        display.innerHTML = text;
    }

    // Retrieve a random rule in rules list and display it
    function DisplaysRandomRule() {
        WriteInDisplay(rules[Math.floor(Math.random() * rules.length)].Explanation);
    }

    btnDice.addEventListener("click", function () { DisplaysRandomRule(); ChallengeDiceDecrease(); });

    // To display challenge's rule
    DisplaysRandomRule();

    var challengeState = document.getElementById('challengeState');
    var challengeStateDescritption = document.getElementById('challengeStateDescritption');

    var isChallengeValidate = document.getElementById('isChallengeValidate');
    var isChallengeNotValidate = document.getElementById('isChallengeNotValidate');

    // Init state on loading page
    isChallengeNotValidate.click();

    var root = document.querySelector(':root');
    var challengeDiceCounter = 2;

    var btnOneMoreTurn = document.getElementById('btnOneMoreTurn');
    var btnNextTeamTurn = document.getElementById('btnNextTeamTurn');

    var btnEndGame = document.getElementById('btnEndGame');

    function ChangeChallengeValidationState() {
        if (challengeStateDescritption.classList == 'challengeNotValidate') {
            challengeStateDescritption.classList = 'challengeValidate';
            challengeStateDescritption.innerHTML = "Défi validé";
            isChallengeValidate.click();
        }
        else {
            challengeStateDescritption.classList = 'challengeNotValidate';
            challengeStateDescritption.innerHTML = "Défi non-validé";
            isChallengeNotValidate.click();
        }
    }

    challengeState.addEventListener("click", function () { ChangeChallengeValidationState(); });

    function ChallengeDiceDecrease() {
        challengeDiceCounter--;

        let diceCode = "\"\\268" + challengeDiceCounter + "\"";

        root.style.setProperty("--dice", diceCode);

        if (challengeDiceCounter < 0) {
            btnDice.style.display = 'none';
        }
    }

    // Keyboard commands
    window.addEventListener("keydown", function (event) {

        switch (event.which) {
            // Down arrow
            case 40:
                challengeState.click();
                break;
            // R (like Reroll)
            case 82:
                if (challengeDiceCounter > -1) {
                    btnDice.click();
                }
                break;
            // Left arrow
            case 37:
                btnOneMoreTurn.click();
                break;
            // Right arrow
            case 39:
                btnNextTeamTurn.click();
                break;
            // Escape
            case 27:
                btnEndGame.click();
                break;
        }
    });
});