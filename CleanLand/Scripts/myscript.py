#!/usr/bin/env python3
import argparse
import json
import sys

def main():
    parser = argparse.ArgumentParser(
        description="A simple greeting script that returns JSON"
    )
    parser.add_argument(
        "--name",
        type=str,
        default="World",
        help="Name to include in the greeting"
    )
    args = parser.parse_args()

    # Build the output object
    result = f"Hello, {args.name}!"
    

    # Print as JSON to stdout
    json.dump(result, sys.stdout)

if __name__ == "__main__":
    main()
