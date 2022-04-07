//Scripts para manejar la pantalla del login 



const handleLogin = async(e) => {

    e.preventDefault();

    const loginForm = document.getElementById('login_form');

    const email = loginForm[0].value;
    const password = loginForm[1].value;

    if (email === '' || password === '') {
        return alert('Please fill in all fields');
    }

    


    //TODO: Enviar los datos al servidor para validar, en caso de ser correctos ir a home, de lo contrario mostrar mensaje de error
    

    //Guardar los datos en el localStorage en caso de que el login sea exitoso
    localStorage.setItem('mail', email);

    //Redirigir a home
    window.location.href = '/';

}


function type_effect(){
    let i = 0
    let placeholder_1 = ""
    let placeholder_2 = ""
    const txt_1 = "example@mail.com"
    const txt_2 = "password"
    const speed = 150

    function effect(){
        placeholder_1 += txt_1.charAt(i)
        placeholder_2 += txt_2.charAt(i)
        document.getElementById("exampleInputEmail1").setAttribute("placeholder", placeholder_1)
        document.getElementById("exampleInputPassword1").setAttribute("placeholder", placeholder_2)
        i++
        setTimeout(effect, speed)
    } 
    effect()
}

function main(){
    type_effect()
}