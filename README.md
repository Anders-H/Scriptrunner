# Scriptrunner

Run templated Powershell scripts.

In the input tab, use:

```
`[Parameter name]|[Parameter datatype]´
```

Example:

```
`important.docx|string´
```

All values are treated as strings, datatype is for validation only. Datatype can be `string`, `int`, `float` or `bool`.

The datatype `bool` accepts only `true` (resolves to `$True`) and `false` (resloves to `$False`).

Scriptrunner cannot have two parameters sharing the same name that only differs in case, but is case senstive in resolving parameters.

If a parameter is used more than once, all instances will have the same value.