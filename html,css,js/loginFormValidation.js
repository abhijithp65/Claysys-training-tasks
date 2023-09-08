
function validateLoginForm() {
    const emailInput = document.getElementById("email");
    const passwordInput = document.getElementById("password");
    const emailError = document.getElementById("emailError");
    const passwordError = document.getElementById("passwordError");


    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(emailInput.value.trim())) {
        emailError.textContent = "Invalid email address";
        return; 
    }

   
    const passwordValue = passwordInput.value.trim();
    if (passwordValue === "") {
        passwordError.textContent = "Password is required";
        return; 
    }

    if (passwordValue.length < 8) {
        passwordError.textContent = "Password must be at least 8 characters long";
        return; 
    }

    alert("Login successful!");
}
