# SpellCheckTask

A console app in .net 4.6.2 to spell check text using the Windows Spell Checking API. 

## Usage
	
```
SpellCheckTask 'file to check' 
```

_or_

```
<Your input> | SpellCheckTask
```

## Output

If misspellings are found, each will be output on a separate line. 

```
[line number]:[column] [misspelled word] -> [comma separated list of suggestions]
```