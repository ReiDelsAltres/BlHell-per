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
window.detectCompressionTypeWithFfflate = async (arrayBuffer) => {
    const bytes = new Uint8Array(arrayBuffer);

    if (bytes.length >= 2 && bytes[0] === 0x1F && bytes[1] === 0x8B) {
        return 'gzip';
    }

    try {
        const brotliResult = fflate.brotliDecompressSync(bytes);
        if (brotliResult && brotliResult.length > 0) {
            return 'brotli';
        }
    } catch (e) {
    }

    try {
        const deflateResult = fflate.decompressSync(bytes);
        if (deflateResult && deflateResult.length > 0) {
            return 'deflate';
        }
    } catch (e) {
    }

    return 'unknown';
}
window.alert1 = async (string) => {
    try {
        console.warn(string);
    } catch (error) {
        console.error("Ошибка в alert1:", error);
    }
}
window.decompressWithFflate = async (arrayBuffer) => {

    const compressed = new Uint8Array(arrayBuffer);

    const decompressed = fflate.decompressSync(compressed);

    return Array.from(decompressed);
}
window.loadFromCache = async (url) => {
    try {
        const cachedResponse = await caches.match(url);
        if (cachedResponse) {
            const arrayBuffer = await cachedResponse.arrayBuffer();
            const byteArray = Array.from(new Uint8Array(arrayBuffer));
            return byteArray;
        }
        console.warn("Вы офлайн, и данных в кэше не найдено для URL: " + url);
        return null;
    } catch (error) {
        console.error("Ошибка при загрузке из кэша:", error);
        return null;
    }
}