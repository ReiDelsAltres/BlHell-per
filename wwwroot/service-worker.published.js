// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

self.importScripts('./service-worker-assets.js');

self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

// ADD THIS MESSAGE HANDLER.
self.addEventListener('message', event => { if (event.data?.type === 'SKIP_WAITING') self.skipWaiting() });

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [/\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.br$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/,];
const offlineAssetsExclude = [/^service-worker\.js$/];

async function onInstall(event) {
    console.info('Service worker: Install - Begin');

    // Fetch and cache all matching items from the assets manifest

    const assets = self.assetsManifest.assets
        .filter(asset => offlineAssetsInclude.some(pattern => pattern.test(asset.url)))
        .filter(asset => !offlineAssetsExclude.some(pattern => pattern.test(asset.url)))

    const assetsRequests = assets.map(asset => {
        console.info(asset)
        return new Request(asset.url, { integrity: asset.hash, cache: 'no-cache' })
    });

    await caches.open(cacheName).then(cache => cache.addAll(assetsRequests));

    console.info('Service worker: Install - End');
}
async function onFetch(event) {
    if (event.request.method !== 'GET') {
        return fetch(event.request);
    }

    // Network First
    try {
        const networkResponse = await fetch(event.request);
        // Если ответ успешный — обновляем кэш и возвращаем ответ
        if (networkResponse && networkResponse.ok) {
            const cache = await caches.open(cacheName);
            cache.put(event.request, networkResponse.clone());
            return networkResponse;
        }
        // Если ответ не ок — пробуем из кэша
        const cache = await caches.open(cacheName);
        const cachedResponse = await cache.match(event.request);
        if (cachedResponse) {
            return cachedResponse;
        }
        return networkResponse; // даже если не ok
    } catch (error) {
        // Если сеть недоступна — пробуем из кэша
        const cache = await caches.open(cacheName);
        const cachedResponse = await cache.match(event.request);
        if (cachedResponse) {
            return cachedResponse;
        }
        // Можно вернуть кастомный offline-ответ, если нужно
        return Response.error();
    }
}

async function onActivate(event) {
    console.info('Service worker: Activate');

    // Delete unused caches
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}
