let scrollToTopBtn = document.querySelector("#scroll-to-top-btn");
window.addEventListener("scroll", toggleScrollToTopBtn);
scrollToTopBtn.addEventListener("click", scrollToTop);

function toggleScrollToTopBtn() {
    if (window.pageYOffset > 100) {
        scrollToTopBtn.classList.add("show-scroll-to-top-btn");
    } else {
        scrollToTopBtn.classList.remove("show-scroll-to-top-btn");
    }
}

function scrollToTop() {
    window.scrollTo({ top: 0, behavior: "smooth" });
}