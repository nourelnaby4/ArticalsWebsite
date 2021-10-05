let profile_icon = document.querySelector(".profile-icon");
let profile_dropdown_list = document.querySelector(".profile-dropdown-list");
let dropdown_list = document.querySelector(".dropdown-list");
let utilities = document.querySelector(".utilties");

profile_icon.addEventListener("click", () => {
    profile_dropdown_list.classList.toggle("d-none")
});

if (window.matchMedia("(max-width:800px)").matches) {
    dropdown_list.appendChild(profile_icon);
    dropdown_list.children[0].classList.remove("d-none");
} else {
    utilities.appendChild(profile_icon);
};


let points_display = document.querySelectorAll(".fa-ellipsis-h")
let options_list = document.querySelectorAll(".options-list")
points_display.forEach(points => {
    points.addEventListener("click", () => {
        points.parentElement.children[1].classList.toggle("d-none")
    })
});