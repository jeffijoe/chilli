# Chilli

Rename your files so they appear in the correct order.

Written in F# - _my first ever F# project! :smile:_

# What?

Let's say you have the following files:

```
Favorite.Show Season 1 Episode 1.mp4
Favorite.Show Season 1 Episode 2.mp4
Favorite.Show Season 1 Episode 3.mp4
...
Favorite.Show Season 1 Episode 9.mp4
Favorite.Show Season 1 Episode 10.mp4
Favorite.Show Season 1 Episode 11.mp4
```

On most devices, the order of the files would then be

```
Favorite.Show Season 1 Episode 1.mp4
Favorite.Show Season 1 Episode 10.mp4
Favorite.Show Season 1 Episode 11.mp4
Favorite.Show Season 1 Episode 2.mp4
Favorite.Show Season 1 Episode 3.mp4
...
```

That sucks.

# Let's spice it up

What you really want, is this:

```
001.mp4
002.mp4
003.mp4
...
010.mp4
011.mp4
```

That will ensure the correct order, and that's what Chilli does.

# Where do I get it?

You can download the binaries from [the releases section](/releases/latest).

# Usage

```
Chilli - episode renamer because you have better things to do
Usage: chilli [directory] pattern [--dry-run]
  directory: The directory containing files to rename. Defaults to current working directory.
  pattern: E.g. Pokemon S01E$EPNUM$.mp4 will rename "Pokemon S01E20.mp4" to "020.mp4".
  --dry-run: Only prints what will happen, but won't actually rename anything.
```

For example, for the case mentioned earlier, in a folder on the desktop containing all the episodes:

```
Chilli "C:\Users\Jeff\Desktop\Favorite Show Season 1" "Favorite.Show Season 1 Episode $EPNUM$.mp4"
```

The `$EPNUM$` is for Chilli to know what actually represents the number that decides the correct order.

If you don't want it to rename anything, but just show you what _will_ happen, tack on `--dry-run`:

```
Chilli "C:\Users\Jeff\Desktop\Favorite Show Season 1" "Favorite.Show Season 1 Episode $EPNUM$.mp4" --dry-run
```

# Author

Jeff Hansen - [@Jeffijoe](https://twitter.com/jeffijoe)
