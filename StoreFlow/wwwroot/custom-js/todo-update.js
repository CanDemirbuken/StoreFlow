const todoList = document.getElementById("todoList");

todoList.addEventListener("change", function (event) {

    if (!event.target.classList.contains("checkbox")) {
        return;
    }

    const checkbox = event.target;
    const todoId = checkbox.dataset.todoId;
    const isChecked = checkbox.checked;

    fetch(`/Todo/Update?id=${todoId}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            status: isChecked
        })
    })
        .then(response => {

            if (!response.ok) {
                throw new Error("Güncelleme işlemi başarısız oldu.");
            }

            return response.json();
        })
        .then(data => {

            const listItem = checkbox.closest("li");

            if (data.status) {
                listItem.classList.add("completed");
            }
            else {
                listItem.classList.remove("completed");
            }

            console.log("Task durumu güncellendi.");
        })
        .catch(error => {

            console.error("Hata:", error);

            checkbox.checked = !isChecked;

            alert("Task güncellenirken bir hata oluştu.");
        });
});