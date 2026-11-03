#!/usr/bin/env bash
# py_compile, django.setup() with the system checks, and mypy --strict with django-stubs on the
# models hekireki-django wrote.
source "$(dirname "$0")/../common.sh"
require python3

cp -RL "$generated/django"/. "$work/app/"
cd "$work"

venv="$cache/venv"
if [[ ! -x "$venv/bin/python" ]]; then
  python3 -m venv "$venv"
fi
python="$venv/bin/python"
"$python" -m pip install --quiet --disable-pip-version-check -r requirements.txt

# The datasource's provider, so the backend's own checks (MySQL's index lengths, ...) run.
case "$provider" in
  mysql) export HEKIREKI_DJANGO_ENGINE=django.db.backends.mysql ;;
  sqlite) export HEKIREKI_DJANGO_ENGINE=django.db.backends.sqlite3 ;;
  *) export HEKIREKI_DJANGO_ENGINE=django.db.backends.postgresql ;;
esac

step "py_compile"
"$python" -m py_compile app/models.py

step "django.setup() and system checks"
"$python" smoke.py

step "mypy --strict"
"$python" -m mypy --config-file mypy.ini app/models.py
