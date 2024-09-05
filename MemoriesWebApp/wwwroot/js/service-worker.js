const CACHE_NAME = 'v1';
const assetsToCache = [
    '/',
    '/css/site.css',
    '/js/site.js',
    '/images/icons/logo-192x192.png',
    '/images/icons/logo-512x512.png'
];

self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => {
                return cache.addAll(assetsToCache);
            })
    );
});

self.addEventListener('fetch', event => {
    event.respondWith(
        caches.match(event.request)
            .then(response => {
                return response || fetch(event.request);
            })
    );
});
