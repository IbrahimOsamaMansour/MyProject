(function () {
    "use strict";

    function wrapTable(table) {
        if (!(table instanceof HTMLTableElement)) return;
        if (table.parentElement && table.parentElement.classList.contains("responsive-table-scroll")) return;

        var wrapper = document.createElement("div");
        wrapper.className = "responsive-table-scroll";
        table.parentNode.insertBefore(wrapper, table);
        wrapper.appendChild(table);
    }

    function wrapTables(root) {
        if (root.nodeType !== Node.ELEMENT_NODE && root !== document) return;

        if (root instanceof HTMLTableElement) {
            wrapTable(root);
            return;
        }

        root.querySelectorAll("table").forEach(wrapTable);
    }

    function initialize() {
        wrapTables(document);

        var observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                mutation.addedNodes.forEach(function (node) {
                    if (node.nodeType === Node.ELEMENT_NODE) wrapTables(node);
                });
            });
        });

        observer.observe(document.body, { childList: true, subtree: true });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", initialize, { once: true });
    } else {
        initialize();
    }
})();
