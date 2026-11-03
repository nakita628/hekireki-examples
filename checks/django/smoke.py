# Loads the generated app and runs Django's system checks and the migration autodetector, which
# catch what a type check cannot: a field name Django rejects, a related_name clash, a default
# the migration writer cannot serialize.
import os
import sys

os.environ.setdefault("DJANGO_SETTINGS_MODULE", "settings")

import django

django.setup()

from django.apps import apps
from django.core import checks
from django.db.migrations.autodetector import MigrationAutodetector
from django.db.migrations.loader import MigrationLoader
from django.db.migrations.state import ProjectState

models = apps.get_app_config("app").get_models()
names = [model.__name__ for model in models]
if not names:
    print("no models loaded", file=sys.stderr)
    sys.exit(1)

errors = checks.run_checks()
for error in errors:
    print(error, file=sys.stderr)
if any(error.is_serious() for error in errors):
    sys.exit(1)

autodetector = MigrationAutodetector(ProjectState(), ProjectState.from_apps(apps))
changes = autodetector.changes(graph=MigrationLoader(None, ignore_no_migrations=True).graph)
for migration in changes.get("app", []):
    for operation in migration.operations:
        operation.deconstruct()

print(f"ok: {len(names)} models, {len(errors)} warnings")
