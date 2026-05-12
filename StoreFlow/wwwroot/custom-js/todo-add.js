document.getElementById("add-task").addEventListener("click", function () {
    const input = document.getElementById("todoDescription");
    const description = input.value.trim();

    if (!description) {
        alert("Lütfen bir task girin.");
        return;
    }

    fetch("/Todo/Create/", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            description: description
        })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Kayıt işlemi başarısız oldu.");
            }

            return response.json();
        })
        .then(data => {
            if (data.success) {
                const todoList = document.getElementById("todoList");
                const li = document.createElement("li");

                li.innerHTML = `
                <div class="form-check">
                    <label class="form-check-label">
                        <input data-todo-id="${data.id}" class="checkbox" type="checkbox">
                        ${data.description}
                        <i class="input-helper"></i>
                    </label>
                </div>
                <i data-todo-id-remove="${data.id}" class="remove fa fa-times-circle"></i>
            `;

                todoList.prepend(li);

                input.value = "";
            }
        })
        .catch(error => {
            console.error("Hata:", error);
            alert("Task eklenirken bir hata oluştu.");
        });
});