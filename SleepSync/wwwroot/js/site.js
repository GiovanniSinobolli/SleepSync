// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", () => {
    const bedInput = document.getElementById("bedTime");
    const wakeInput = document.getElementById("wokeUpTime");
    const hoursInput = document.getElementById("hoursSlept");

    function calculateHoursSlept() {
        const bed = bedInput.value;
        const wake = wakeInput.value;

        if (!bed || !wake) return;

        const [bedH, bedM] = bed.split(":").map(Number);
        const [wakeH, wakeM] = wake.split(":").map(Number);

        let bedMinutes = bedH * 60 + bedM;
        let wakeMinutes = wakeH * 60 + wakeM;

        let sleptMinutes = wakeMinutes - bedMinutes;
        if (sleptMinutes <= 0) sleptMinutes += 24 * 60;

        hoursInput.value = (sleptMinutes / 60).toFixed(2);
    }

    bedInput.addEventListener("change", calculateHoursSlept);
    wakeInput.addEventListener("change", calculateHoursSlept);

    document.querySelector("form").addEventListener("submit", function () {
        calculateHoursSlept(); // Final update before submitting
    });
});

