function commandEditor()
{
    var editor = document.createElement("div");

    var label = document.createElement("label")
    label.innerHTML = "Test: "
    var input = document.createElement("input")

    editor.appendChild(label);
    editor.appendChild(input);

    return editor;
}