#!/usr/bin/env bash
set -e

start_dir="$(pwd)"
cd ../ || exit 1
root_dir="$(pwd)"

artifacts_path="$root_dir/.artifacts"
nuget_find_dir="$root_dir/src"

# Always return to original directory
trap 'cd "$start_dir"' EXIT

# Create artifacts directory
mkdir -p "$artifacts_path"

# Find and copy .nupkg files
find "$nuget_find_dir" -type f -name "*.nupkg" -print0 | while IFS= read -r -d '' file; do
    cp -afv "$file" "$artifacts_path"
done
