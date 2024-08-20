Steps to reproduce:

- Set a breakpoint at MyGenerator.cs:40
    - should be return "breakpoint here";
- Debug the launch configuration "DebugSample"
- At the breakpoint, inspect attribute.ConstructorArguments.  It should contain a string "asdf"
- Debug the (only) unit test
- At the breakpoint, inspect attribute.ConstructorArguments.  It should be an empty collection
