
function validateContactForm() {
    const nameInput = document.getElementById("name");
    const emailInput = document.getElementById("email");
    const messageInput = document.getElementById("message");

    if (nameInput.value.trim() === "") {
        alert("Name is required");
        nameInput.focus();
    }

    if (emailInput.value.trim() === "") {
        alert("Email is required");
        emailInput.focus();
    }

    if (messageInput.value.trim() === "") {
        alert("Message is required");
        messageInput.focus();
    }
}


