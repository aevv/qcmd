function g(params) {
    StartProcess("chrome.exe", "https://www.google.com/search?q=" + params.replace(" ", "%20"));
}