document.addEventListener("DOMContentLoaded", function () {
    // Typed.js for motivational cycling text
    new Typed('#typed-text', {
        strings: [
            "Prioritize rest. Elevate performance.",
            "Sync your life with your rhythm.",
            "Better sleep, better days.",
            "Rest isn't idle — it's essential."
        ],
        typeSpeed: 50,
        backSpeed: 25,
        loop: true
    });

    // GSAP for entrance animations
    gsap.registerPlugin(ScrollTrigger);

    gsap.from(".display-4", {
        opacity: 0,
        y: -40,
        duration: 1,
        ease: "power2.out"
    });

    gsap.from(".card", {
        scrollTrigger: {
            trigger: ".card",
            start: "top 85%",
        },
        opacity: 0,
        y: 50,
        stagger: 0.2,
        duration: 0.8,
        ease: "power3.out",
        clearProps: "transform"  // Remove the transform after anim
    });

});
