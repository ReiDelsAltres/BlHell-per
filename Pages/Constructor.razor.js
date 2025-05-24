function downloadFileFromStream(filename, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}
function saveFile(file, Content) {
    var link = document.createElement('a');
    link.download = name;
    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(Content)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

async function loadJSON(url) {
    if (navigator.onLine) {
        try {
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error(`Ошибка загрузки: ${response.status}`);
            }
            const data = await response.json();
            console.log("Загруженные данные:", data);
            return data;
        } catch (error) {
            console.error("Ошибка запроса:", error);
            alert("Не удалось загрузить данные из сети. Попробуйте позже.");
            return null;
        }
    } else {
        // Пытаемся получить данные из кэша, если нет интернета
        const cachedResponse = await caches.match(url);
        if (cachedResponse) {
            const data = await cachedResponse.json();
            console.log("Данные из кэша:", data);
            return data;
        } else {
            alert("Вы офлайн, и данных в кэше не найдено.");
            return null;
        }
    }
}