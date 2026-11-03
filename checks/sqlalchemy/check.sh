#!/usr/bin/env bash
# py_compile, an import, and mypy --strict on the models hekireki-sqlalchemy wrote, against the
# real SQLAlchemy.
source "$(dirname "$0")/../common.sh"
require python3

cp -RL "$generated/sqlalchemy"/. "$work"/
cd "$work"

venv="$cache/venv"
if [[ ! -x "$venv/bin/python" ]]; then
  python3 -m venv "$venv"
fi
python="$venv/bin/python"
"$python" -m pip install --quiet --disable-pip-version-check -r requirements.txt

step "py_compile"
"$python" -m py_compile models.py

step "import models (resolves the mappers)"
"$python" -c "import models; from sqlalchemy.orm import configure_mappers; configure_mappers()"

step "mypy --strict"
"$python" -m mypy --config-file mypy.ini models.py
