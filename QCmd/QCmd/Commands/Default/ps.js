function ps(arg) {
    psDir = "c:/";
    if (arg != "")
        psDir = arg;

    StartProcess("powershell", "-noexit -command \"cd " + psDir + "\"");
};