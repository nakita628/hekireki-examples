# Just enough for django.setup() to load the generated app. The database backend follows the
# schema's datasource (check.sh sets HEKIREKI_DJANGO_ENGINE); nothing ever connects.
import os

SECRET_KEY = "hekireki-examples"
INSTALLED_APPS = ["app"]
DATABASES = {
    "default": {
        "ENGINE": os.environ.get("HEKIREKI_DJANGO_ENGINE", "django.db.backends.postgresql"),
        "NAME": "hekireki",
        "HOST": "localhost",
    }
}
USE_TZ = True
DEFAULT_AUTO_FIELD = "django.db.models.BigAutoField"
