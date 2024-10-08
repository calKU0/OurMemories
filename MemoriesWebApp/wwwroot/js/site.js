﻿$(document).ready(function () {
    var successMessage = '@TempData["ShowModal"]';
    if (successMessage) {
        $('#infoModal').modal('show');
    }
});
function updateClock() {
    const now = new Date();
    let hours = now.getHours();
    const minutes = now.getMinutes();
    const seconds = now.getSeconds();
    hours = hours % 24;
    const displayHours = (hours === 0) ? 12 : hours;

    const timeRemaining = endDate - now;

    const days = Math.floor(timeRemaining / (1000 * 60 * 60 * 24));
    const hoursRemaining = Math.floor((timeRemaining % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutesRemaining = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
    const secondsRemaining = Math.ceil((timeRemaining % (1000 * 60)) / 1000);

    const countdownElement = document.getElementById("countdown");
    countdownElement.innerHTML = formatCountdown(days, hoursRemaining, minutesRemaining, secondsRemaining);

    const hands = [
        { id: "hourHand", deg: (360 / 24) * hours + (360 / 24) * (minutes / 60), value: hours, top: 75 },
        { id: "hourHand1", deg: (360 / 24) * hours + (360 / 24) * (minutes / 60), value: hours, top: 60 },
        { id: "hourHand2", deg: (360 / 24) * hours + (360 / 24) * (minutes / 60), value: hours, top: 45 },
        { id: "hourHand3", deg: (360 / 24) * hours + (360 / 24) * (minutes / 60), value: hours, top: 30 },
        { id: "minuteHand", deg: (360 / 60) * minutes + (360 / 60) * (seconds / 60), value: minutes, top: 75 },
        { id: "minuteHand1", deg: (360 / 60) * minutes + (360 / 60) * (seconds / 60), value: minutes, top: 60 },
        { id: "minuteHand2", deg: (360 / 60) * minutes + (360 / 60) * (seconds / 60), value: minutes, top: 45 },
        { id: "minuteHand3", deg: (360 / 60) * minutes + (360 / 60) * (seconds / 60), value: minutes, top: 30 },
        { id: "minuteHand4", deg: (360 / 60) * minutes + (360 / 60) * (seconds / 60), value: minutes, top: 15 },
        { id: "minuteHand5", deg: (360 / 60) * minutes + (360 / 60) * (seconds / 60), value: minutes, top: 0 },
        { id: "secondHand", deg: (360 / 60) * seconds, value: seconds, top: 75 },
        { id: "secondHand1", deg: (360 / 60) * seconds, value: seconds, top: 60 },
        { id: "secondHand2", deg: (360 / 60) * seconds, value: seconds, top: 45 },
        { id: "secondHand3", deg: (360 / 60) * seconds, value: seconds, top: 30 },
        { id: "secondHand4", deg: (360 / 60) * seconds, value: seconds, top: 15 },
        { id: "secondHand5", deg: (360 / 60) * seconds, value: seconds, top: 0 },
        { id: "secondHand6", deg: (360 / 60) * seconds, value: seconds, top: -15 },
        { id: "secondHand7", deg: (360 / 60) * seconds, value: seconds, top: -30 }
    ];

    const clock = document.getElementById("clock");
    clock.innerHTML = '';

    hands.forEach(hand => {
        const handElement = document.createElement("div");
        handElement.classList.add("hand");
        handElement.id = hand.id;
        clock.appendChild(handElement);
        updateHand(hand);
    });

    const dot = document.createElement("div");
    dot.classList.add("dot");
    clock.appendChild(dot);
    clock.offsetHeight;

    const totalTime = endDate - startDate;
    const elapsed = now - startDate;

    const progress = Math.max(0, Math.min(100, (elapsed / totalTime) * 100));

    updateProgressBar(progress);

    startLabel.innerHTML = formatDate(startDate);
    endLabel.innerHTML = formatDate(endDate);
    endLabel.style.right = `calc(${100 - progress}% + 40px)`;
}

$(window).on('load', function () {
    var delay = 150;
    $('.fadeDownClass').each(function (index) {
        var element = $(this);
        setTimeout(function () {
            element.addClass('fade-down');
        }, index * delay);
    });
});
function updateHand(hand) {
    const element = document.getElementById(hand.id);
    const deg = hand.deg;
    const value = hand.value;

    element.style.transform = `translate(-50%, -100%) rotate(${deg}deg)`;
    element.innerHTML = `<div class="number" style="top: ${hand.top}%">${value}</div>`;
}

function updateProgressBar(progress) {
    const progressBar = document.getElementById("progress-bar");
    progressBar.style.width = `${progress}%`;
}

function formatDate(date) {
    const options = { day: 'numeric', month: 'numeric', year: 'numeric'};
    return date.toLocaleDateString('pl-PL', options);
}

function formatCountdown(days, hours, minutes, seconds) {
    if (days > 0) return `${days} ${getPolishInflection(days, "day")} ${hours} ${getPolishInflection(hours, 'hour')} ${minutes} ${getPolishInflection(minutes, 'minute')} ${seconds} ${getPolishInflection(seconds, 'second')}`;
    else if (hours > 0) return `${hours} ${getPolishInflection(hours, 'hour')} ${minutesRemaining} ${getPolishInflection(minutesRemaining, 'minute')} ${seconds} ${getPolishInflection(seconds, 'second')}`;
    else if (minutes > 0) return `${minutes} ${getPolishInflection(minutes, 'minute')} ${seconds} ${getPolishInflection(seconds, 'second')}`;
    else if (seconds > 0) return `${seconds} ${getPolishInflection(seconds, 'second')}`;
    else return '';
}

function getPolishInflection(value, unit){
    if (unit == "day") {
        if (value == 1) return "dzień";
        return "dni";
    }
    else if (unit == "hour") {
        if (value == 1) return "godzina";
        if (value >= 2 && value <= 4) return "godziny";
        return "godzin";
    }
    else if (unit == "minute") {
        if (value == 1) return "minuta";
        if (value >= 2 && value <= 4) return "minuty";
        return "minut";
    }
    else if (unit == "second") {
        if (value == 1) return "sekunda";
        if (value >= 2 && value <= 4) return "sekundy";
        return "sekund";
    }
    return unit;
}

const startLabel = document.querySelector('.start-date');
const endLabel = document.querySelector('.end-date');
var endDateValue = endLabel.textContent;
var previousDateValue = startLabel.textContent;
const startDate = new Date(previousDateValue);
const endDate = new Date(endDateValue);

setInterval(updateClock, 1000);

updateClock();






