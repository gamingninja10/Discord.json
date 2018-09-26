function commandEditor()
{
    var id = "command-" + Math.random();
    var editor = document.createElement("div");
    editor.id = id;
    editor.classList.add("spacer-max");

    var deleteBtn = document.createElement("button");
    deleteBtn.classList.add("spacer-right");
    deleteBtn.textContent = "X";
    deleteBtn.setAttribute("onClick", "remove(\""+id+"\")");

    var input = document.createElement("input")
    input.className = "commandInput";
    input.classList.add("spacer-right");
    input.placeholder = "Name";
    input.size = 8;

    var argAmount = document.createElement("input");
    argAmount.classList.add("right-spacer");
    argAmount.type = "number";
    argAmount.min = 0;
    argAmount.max = 9;
    argAmount.placeholder = "Args"
    argAmount.className = "argCountInput"

    var addAction = document.createElement("button");
    addAction.textContent = "+";
    addAction.setAttribute("onClick", "actionEditor(\""+id+"\")");

    editor.appendChild(deleteBtn);
    editor.appendChild(input);
    editor.appendChild(argAmount);
    editor.appendChild(addAction);

    return editor;
}

function actionEditor(_parentDiv)
{
    const possibleActions = ["Reply", "SendMessage", "Purge", "AddRole", "CreateRole", "DeleteRole", "CreateTextChannel", "DeleteTextChannel", "ChangeNickname", "MoveToCategory", "ChangeServerName"];
    var parentDiv = document.getElementById(_parentDiv);

    var id = "action-" + Math.random();
    var editor = document.createElement("div");
    editor.id = id;
    editor.classList.add("spacer");
    editor.classList.add("move-right");

    var deleteBtn = document.createElement("button");
    deleteBtn.classList.add("spacer-right");
    deleteBtn.textContent = "X";
    deleteBtn.setAttribute("onClick", "remove(\""+id+"\")");

    var input = document.createElement("select")
    input.className = "actionInput";
    input.classList.add("spacer-right");
    for (var i = 0; i < possibleActions.length; i++)
    {
        var option = document.createElement("option");
        option.value = possibleActions[i];
        option.text = possibleActions[i];
        input.appendChild(option);
    }
    //input.size = 10;

    var addArg = document.createElement("button");
    addArg.textContent = "+";
    addArg.setAttribute("onClick", "argumentEditor(\""+id+"\")");

    editor.appendChild(deleteBtn);
    editor.appendChild(input);
    editor.appendChild(addArg);

    parentDiv.appendChild(editor);
}

function argumentEditor(_parentDiv)
{
    var parentDiv = document.getElementById(_parentDiv);

    var id = "arg-" + Math.random();

    var editor = document.createElement("div");
    editor.id = id;
    editor.classList.add("spacer");
    editor.classList.add("move-right");

    var deleteBtn = document.createElement("button");
    deleteBtn.classList.add("spacer-right");
    deleteBtn.textContent = "X";
    deleteBtn.setAttribute("onClick", "remove(\""+id+"\")");

    var input = document.createElement("input");
    input.className = "argInput";
    input.classList.add("spacer-right");
    input.placeholder = "Arg";
    input.size = 8;

    editor.appendChild(deleteBtn);
    editor.appendChild(input);

    parentDiv.appendChild(editor);
}

function remove(id)
{
    var elem = document.getElementById(id);
    elem.remove();
}

function download(filename, text) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', filename);
  
    element.style.display = 'none';
    document.body.appendChild(element);
  
    element.click();
  
    document.body.removeChild(element);
  }