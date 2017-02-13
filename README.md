# OddSpell

An on-demand spell checker that makes you fix your own mistakes. 

## More?

OddSpell serves a specific purpose; to provide simple, information only, spell checking when requested. It will take a file or piped input and provide suggestions on a line by line basis. Corrections you will need to retype yourself. 

### Who would want such a thing?

I made OddSpell because I write in VS Code and I wanted to choose when to check my spelling rather than having the UI continually interrupting the flow of my writing. I also wanted to force myself to correct errors manually on the theory that it will improve my muscle memory for correct spelling.

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
