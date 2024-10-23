#!/usr/bin/env bash

# Check if GODOT environment variable is set
if [[ -z "$GODOT" ]]; then
  echo "Error: GODOT environment variable not set" >&2
  exit 1
fi

# Check if Godot executable exists
if ! [[ -f "$GODOT" ]]; then
  echo "Error: Godot executable not found at $GODOT" >&2
  exit 1
fi

run_target="--confirma-run"
args=()

# Transform long options to short ones
for arg in "$@"; do
  shift
  case "$arg" in
    '--run')             set -- "$@" '-r'   ;;
    '--method')          set -- "$@" '-m'   ;;
    '--category')        set -- "$@" '-c'   ;;
    '--verbose')         set -- "$@" '-v'   ;;
    '--sequential')      set -- "$@" '-s'   ;;
    '--exit')            set -- "$@" '-e'   ;;
    '--disable-orphans') set -- "$@" '-d'   ;;
    '--disable-cs')      set -- "$@" '-h'   ;;
    '--disable-gd')      set -- "$@" '-j'   ;;
    '--gd-path')         set -- "$@" '-g'   ;;
    '--output')          set -- "$@" '-o'   ;;
    '--output-path')     set -- "$@" '-p'   ;;
    *)                   set -- "$@" "$arg" ;;
  esac
done

while getopts ":r:m:c:vsedhjg:o:p:" opt; do
  case "$opt" in
    r) run_target="--confirma-run=$OPTARG";;
    m) args+=("--confirma-method=$OPTARG");;
    c) args+=("--confirma-category=$OPTARG");;
    v) args+=("--confirma-verbose");;
    s) args+=("--confirma-sequential");;
    e) args+=("--confirma-exit-on-failure");;
    d) args+=("--confirma-disable-orphans-monitor");;
    h) args+=("--confirma-disable-cs");;
    j) args+=("--confirma-disable-gd");;
    g) args+=("--confirma-gd-path=$OPTARG");;
    o) args+=("--confirma-output=$OPTARG");;
    p) args+=("--confirma-output-path=$OPTARG");;
    \?) echo "Error: Unknown option: -$OPTARG" >&2; exit 1;;
    :) echo "Error: Option -$OPTARG requires a value" >&2; exit 1;;
  esac
done

# Add remaining arguments
shift $((OPTIND-1))
args+=("$@")

$GODOT ./ --headless -- $run_target "${args[@]}"
