#!/usr/bin/env bash

start_dir="$(pwd)"
cd ../ || exit 1

# Ensure we always return to original directory
trap 'cd "$start_dir"' EXIT

find . -type f -name "*.nupkg" | while read -r file; do
	rm -v "$file"
done
