async function hanleLogin(event){
    event.preventDefault();  //block form send request default
    
    console.log()

    const dto ={
        email: document.getElementById("email").value,
        password: document.getElementById("password").value
    };
    const response = await fetch('/Auth/Login',{
        method: 'POST',
        headers:{
            'Content-Type' : 'application/json'
        },
        body: JSON.stringify(dto)
    });
    const data = await response.json();
    if (response.ok){
        console.log("Login Successfully");
        window.location.href = data.redirectUrl;
    }else{
        document.getElementById("errorMessage").textContent ="Thông tin đăng nhập không đúng"
    }
}