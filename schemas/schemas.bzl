"""The schemas under schemas/, each a package with a `generate` target, and the datasource
provider of each: hekireki-exposed writes for postgresql only, so //checks/exposed reads it.

Keep this in step with sources.json (the import tool reads that; Bazel cannot)."""

SCHEMAS = {
    "taxonomy": "mysql",
    "umami": "postgresql",
    "hoppscotch": "postgresql",
    "langfuse": "postgresql",
    "calcom": "postgresql",
    "nextauth": "sqlite",
    "ghostfolio": "postgresql",
    "typebot": "postgresql",
    "linkwarden": "postgresql",
    "dub": "mysql",
    "rallly": "postgresql",
    "documenso": "postgresql",
    "teable": "postgresql",
    "postiz": "postgresql",
    "papermark": "postgresql",
    "formbricks": "postgresql",
    "triggerdev": "postgresql",
    "anythingllm": "sqlite",
    "inboxzero": "postgresql",
    "openpanel": "postgresql",
    "chibisafe": "sqlite",
    "typehero": "mysql",
    "workoutcool": "postgresql",
    "plunk": "postgresql",
    "jitsu": "postgresql",
    "opensaas": "postgresql",
    "litellm": "postgresql",
    "blinko": "postgresql",
    "superproductivity": "postgresql",
}

# The generators left out of a schema (`skip` in sources.json): the generator refuses it.
SKIPPED = {
    "formbricks": ["eloquent"],
    "inboxzero": ["eloquent"],
}

POSTGRESQL_SCHEMAS = [name for name, provider in SCHEMAS.items() if provider == "postgresql"]

def schemas_with(lang, schemas = SCHEMAS.keys()):
    """The schemas whose generation includes `lang`'s generator."""
    return [name for name in schemas if lang not in SKIPPED.get(name, [])]
