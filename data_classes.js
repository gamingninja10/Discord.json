class Command
{
    constructor(name, args, actions)
    {
        this.name = name;
        this.args = new Number(args);
        this.actions = actions;
    }

}

class Action
{
    constructor(action, args)
    {
        this.action = action;
        this.args = args;
    }

}