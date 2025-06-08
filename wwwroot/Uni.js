if ('serviceWorker' in navigator) {
    window.addEventListener('load', function () {
        navigator.serviceWorker.register('service-worker.js')
            .then(registration => {
                console.log('Сервис-воркер зарегистрирован c областью:', registration.scope);
            })
            .catch(error => {
                console.error('Ошибка регистрации сервис-воркера:', error);
            });
    });
}

window.saveFile = async (file, Content) => {
    var link = document.createElement('a');
    link.download = name;
    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(Content)
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
window.loadJSON = async (url) => {
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