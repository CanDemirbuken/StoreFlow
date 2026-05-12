const todoListRemove = document.getElementById("todoList");

todoListRemove.addEventListener("click", function (event) {

    const icon = event.target.closest(".remove");

    if (!icon) {
        return;
    }

    const todoId = icon.dataset.todoIdRemove;

    fetch(`/Todo/Remove?id=${todoId}`, {
        method: "DELETE"
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Silme işlemi başarısız oldu.");
            }

            return response.json();
        })
        .then(data => {
            if (data.success) {
                icon.closest("li").remove();
                console.log("Task silindi.");
            }
        })
        .catch(error => {
            console.error("Hata:", error);
            alert("Task silinirken bir hata oluştu.");
        });
});