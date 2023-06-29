var challengeValidationIcon = document.getElementById('challengeState');
var challengeStateDescritption = document.getElementById('challengeStateDescritption');

function ChangeChallengeValidationState() {
    if (challengeValidationIcon.classList == 'challengeNotValidate') {
        challengeValidationIcon.classList = 'challengeValidate';
        challengeStateDescritption.innerHTML = "Défi validé";
    }
    else {
        challengeValidationIcon.classList = 'challengeNotValidate';
        challengeStateDescritption.innerHTML = "Défi non-validé";
    }
}