// Caution! Be sure you understand the caveats before publishing an application with
// offline support. See https://aka.ms/blazor-offline-considerations

self.importScripts('./service-worker-assets.js');

self.addEventListener('install', event => event.waitUntil(onInstall(event)));
self.addEventListener('activate', event => event.waitUntil(onActivate(event)));
self.addEventListener('fetch', event => event.respondWith(onFetch(event)));

// ADD THIS MESSAGE HANDLER.
self.addEventListener('message', event => { if (event.data?.type === 'SKIP_WAITING') self.skipWaiting() });
// Check connectivity when the page loads
self.addEventListener("load", checkConnectivity);

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;
const offlineAssetsInclude = [/\.dll$/, /\.pdb$/, /\.wasm/, /\.html/, /\.js$/, /\.json$/, /\.css$/, /\.woff$/, /\.png$/, /\.jpe?g$/, /\.gif$/, /\.ico$/, /\.blat$/, /\.dat$/];
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

async function onActivate(event) {
    console.info('Service worker: Activate');

    // Delete unused caches
    const cacheKeys = await caches.keys();
    await Promise.all(cacheKeys
        .filter(key => key.startsWith(cacheNamePrefix) && key !== cacheName)
        .map(key => caches.delete(key)));
}

async function onFetch(event) {
    let cachedResponse = null;
    if (event.request.method === 'GET') {
        // For all navigation requests, try to serve index.html from cache
        // If you need some URLs to be server-rendered, edit the following check to exclude those URLs
        const shouldServeIndexHtml = event.request.mode === 'navigate';

        //const request = shouldServeIndexHtml ? 'index.html' : event.request;

        const request = shouldServeIndexHtml ?
            (new URL(event.request.url)).pathname.replace(/\/$/, '') + '/index.html' :
            event.request;

        console.info(request);

        const cache = await caches.open(cacheName);
        cachedResponse = await cache.match(request);
    }

    return cachedResponse || fetch(event.request);
}

function checkConnectivity() {
    if (navigator.onLine) {
        console.log("Online: You have an internet connection.");
    } else {
        alert("Offline: Your internet connection is lost. Please check your connection.");
    }
}
