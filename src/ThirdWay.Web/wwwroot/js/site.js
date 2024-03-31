// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// HTMX Configuration
document.body.addEventListener('htmx:beforeSwap', function (evt) {
    if (evt.detail.xhr.status === 204) {
        // allow 204 (no content) responses to swap as we are using this as a signal that
        // the delete was successful.
        evt.detail.shouldSwap = true;
    }
});