// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });
self.addEventListener('message', event => { if (event.data?.type === 'SKIP_WAITING') self.skipWaiting() });

// Check connectivity when the page loads
self.addEventListener("load", checkConnectivity);
function checkConnectivity() {
    if (navigator.onLine) {
        console.log("Online: You have an internet connection.");
    } else {
        alert("Offline: Your internet connection is lost. Please check your connection.");
    }
}

// Listen for changes in connectivity
self.addEventListener("online", () => alert("Back Online: Your connection has been restored."));
self.addEventListener("offline", () => alert("Offline: Your internet connection is lost."));
