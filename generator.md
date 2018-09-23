---
title: Discord.json Bot Generator
---

<html>
<body>
        <H1>Discord.json Bot Creator</H1>
        <label>Bot Token:</label>
        <input id="token" placeholder="token" />
        <br>
        <label>Playing</label>
        <input id="playing" placeholder="a game" />
        <br>
        <label>Activity</label>
        <select id="activity">
            <option value=0>Playing</option>
            <option value=1>Streaming</option>
            <option value=3>Watching</option>
            <option value=4>Listening</option>
        </select>
        <br>
        <button onclick="addCommand()">+</button>
        <br>
    </body>
    <script src="creator.js"></script>
    <script>
        var commands = new Array();
        function addCommand()
        {
            var editor = commandEditor();
            commands.push(editor);
            document.body.appendChild(editor);
        }
    </script>
        </html>
