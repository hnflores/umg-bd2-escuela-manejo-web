(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', async function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    await LogIn();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

async function LogIn() {
    const request = {
        token: document.getElementsByName('__RequestVerificationToken')[0].value,
        UserName: document.getElementById('user').value,
        Password: document.getElementById('current-password').value
    }
    await showButtonLoading('btnLogin');
    const response = await http('/Login/LogIn', 'POST', request, '/Home/Error/500', request.token);
    if (response.code === 0) {
        location.href = '/Home';
    } else {
        await hideButtonLoading('btnLogin');
        await showIziToastError(response.description, 'bottomCenter');
    }
}
