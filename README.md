# OddSpell

An on-demand spell checking cli app. 

## More?

OddSpell serves a specific purpose; to provide simple, information only, spell checking when requested. It will take a file or piped input and provide suggestions on a line by line basis. Corrections you will have to retype yourself. 

## Requires

.net 4.6.2, Windows 10.

## Usage

Current usage, but soon to be improved.
	
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
