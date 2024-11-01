 document.addEventListener("DOMContentLoaded", function () {
    loadProjects();
    const memberList = document.getElementById('memberList');
});

//Modal
function openModal(){
    document.getElementById('modal').style.display= 'flex';
    document.getElementById('header').style.zIndex='-1';
}


//Modal for create
function openModal2(projectId){
    const handle = (data) => {
       document.getElementById("projectName").value = data.name;
       document.getElementById("status").value =data.status;
       document.getElementById("description").value = data.description;
    }

    fetchProjectDetail(projectId, handle);
    const saveButton = document.querySelector('.save-button');
    saveButton.onclick = () => updateProject(projectId);
    openModal();
}

function closeModal(){
    document.getElementById('modal').style.display ='none';
    document.getElementById('header').style.zIndex='0';
    document.getElementById('projectForm').reset();

    const saveButton = document.querySelector('.save-button');
    saveButton.onclick = () => createProject();
}


async function updateProject(projectId){
    const dto={
        name: document.getElementById('projectName').value,
        // startDate: document.getElementById('startDate').value,
        // endDate: document.getElementById('endDate').value,
        status: document.getElementById('status').value,
        description: document.getElementById('description').value
    }
    
    try{
        const response = await fetch(`/projects/${projectId}`,{
            method: 'PUT',
            headers: {
                'Accept' : 'application/json',
                "Content-Type": "application/json"
            },
            body: JSON.stringify(dto)
        });
        
        if (!response.ok)
        {
            throw new Error(`HTTP Error! Status: ${response.status}`);
        }
        
        const data = await response.json();
        if (data.isSuccess){
            alert("Project updated successfully");
            closeModal();
            loadProjectDetails(projectId);
            loadProjects();
        }else{
            alert("Error: " + data.errorMessage);
        }
    }catch (error){
        console.log("Error:", error);
        alert("An error occured while updating the project:" + error.message);
    }
}

async function createProject() {
    const dto = {
        projectName: document.getElementById('projectName').value,   
        // startDate: document.getElementById('startDate').value,        
        // endDate: document.getElementById('endDate').value,
        status: document.getElementById('status').value,
        description: document.getElementById('description').value   
    };

    console.log('DTO being sent:', dto);  
    console.log('JSON being sent:', JSON.stringify(dto));  
    
    try {
        const response = await fetch("/projects/", {
            method: "POST", 
            headers: {
                'Accept': 'application/json',
                "Content-Type": "application/json" 
            },
            body: JSON.stringify(dto) 
        });

       
        if (!response.ok) {
            
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json(); 
        
        if (data.isSuccess) {
            alert("Project created successfully!");
            closeModal();
            loadProjects();
        } else {
            alert("Error: " + data.errorMessage);
        }
    } catch (error) {
        
        console.log("Error:", error);
        alert("An error occurred while creating the project: " + error.message);
    }
}


// Sample member data
const members = [
    { userId: 1, name: 'Username', email: 'example@gmail.com'},
    { userId: 2, name: 'Username', email: 'example@gmail.com' },
    { userId: 3, name: 'Username', email: 'example@gmail.com' },
    { userId: 4, name: 'Username', email: 'example@gmail.com'},
    { userId: 5, name: 'Username', email: 'example@gmail.com' }
];

// const memberList = document.getElementById('memberList');

// Render members
function renderMembers(members) {
    memberList.innerHTML = members.map(member => `
                <div class="member-item">
                    <div class="member-info">
                        <div class="avatar"></div>
                        <span>${member.name}(${member.email})</span>
                    </div>
                    <button class="btn btn-delete" onclick="deleteMember(${member.userId})">Delete</button>
                </div>
            `).join('');
}

// Delete member function
function deleteMember(id) {
    const index = members.findIndex(m => m.id === id);
    if (index !== -1) {
        members.splice(index, 1);
        renderMembers();
    }
}

// Initial render
//renderMembers();



function loadProjects() {
    fetch(`/projects/`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            const projectSection = document.querySelector('.projects-section nav');
            projectSection.innerHTML = ''; // Clear the existing content

            // Loop through the projects and create HTML elements
            data.forEach(project => {
                const projectLink = document.createElement('a');
                projectLink.href = ``;
                projectLink.classList.add('project-link');
                projectLink.innerHTML = `
                    <img src="image-svgrepo-com.svg" alt="" class="project-icon">
                    ${project.projectName}
                `;
                
                projectLink.addEventListener('click',() =>{
                    event.preventDefault();
                    // history.pushState(null, '', ``);
                    highlightProject(projectLink);
                   loadProjectDetails(project.id);
                });
                
                projectSection.appendChild(projectLink);
            });
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
           
         
function highlightProject(selectedLink){
    const projectLinks = document.querySelectorAll('.project-link');
    projectLinks.forEach(link=> link.classList.remove('active'));
    selectedLink.classList.add('active');
}

const fetchProjectDetail = async (projectId, callback) => {
    fetch(`projects/${projectId}`)
        .then(response =>{
            if (!response.ok)
            {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data =>{
            callback(data);
        })
        .catch(error =>{
            console.error('Fetch error', error);
        });
}
async function loadProjectDetails(projectId){
    
    const handle = (data) => {
        document.querySelector('.project').innerText = data.name;
        document.querySelector('.project-details p:nth-child(1)').innerText = `PM: ${data.userProject.find(u => u.isPM)?.name || 'Chưa có PM'}`;
        document.querySelector('.project-details p:nth-child(2)').innerText = `Description: ${data.description}`;
        document.getElementById("mainContent").style.display = "block";
        renderMembers(data.userProject);
    }
    
    await fetchProjectDetail(projectId, handle);
    document.querySelector('.btn-update').onclick = () =>{
        openModal2(projectId);
    }

    document.getElementById('btn-delete-project').onclick= ()=>{
        deleteProject2(projectId);
    }
}


function deleteProject2(projectId){
    let result = confirm("Bạn có muốn xóa không?");
    if (result){
        deleteProject(projectId);
    }
}

async function deleteProject(projectId){
    try{
        const response = await fetch(`/projects/${projectId}`,{
           method: 'DELETE',
           headers: {
               'Accept' : 'application/json',
               "Content-Type" : "application/json"
           }
        });
        
        if (!response.ok){
            throw new Error(`HTTP Error! Status: ${response.status}`);
        }
        const data = await response.json();
        if(data.isSuccess){
            alert("Project delete successfully!");
            loadProjects();
            document.getElementById("mainContent").style.display = "none";
        }else{
            alert("Error: "+ data.errorMessage);
        }
    }catch (error){
        console.log("Error:",error);
        alert("An error occured while deleting the project:" + error.message);
    }
}
