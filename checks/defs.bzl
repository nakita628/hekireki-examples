"""One test per schema that compiles what a hekireki generator wrote with the ORM's own toolchain."""

load("@rules_shell//shell:sh_test.bzl", "sh_test")
load("//schemas:schemas.bzl", "SCHEMAS", "schemas_with")

def lang_checks(lang, harness, schemas = SCHEMAS.keys()):
    """Declares `//checks/<lang>:<schema>` for every schema.

    Each test runs `check.sh <harness package> <schema> <generated dir> <schema.prisma>` outside
    the sandbox,
    with the toolchain (go, cargo, mix, ...) read from the PATH of the shell that ran Bazel.
    The tests are tagged `lang` (left out of `bazel test //...` by .bazelrc) and `lang-<lang>`.

    Args:
        lang: The toolchain's name, the tag `lang-<lang>` picks the tests of this package.
        harness: The files of this package the check needs (go.mod, Cargo.toml, ...).
        schemas: Which schemas to check; every one by default, less those whose generation
            leaves `lang`'s generator out (SKIPPED in //schemas:schemas.bzl).
    """
    for schema in schemas_with(lang, schemas):
        sh_test(
            name = schema,
            size = "large",
            srcs = ["check.sh"],
            args = [
                native.package_name(),
                schema,
                "$(rootpath //schemas/%s:generate)" % schema,
                "$(rootpath //schemas/%s:schema.prisma)" % schema,
            ],
            data = harness + [
                "//schemas/%s:generate" % schema,
                "//schemas/%s:schema.prisma" % schema,
            ],
            tags = [
                "lang",
                "lang-" + lang,
                # The toolchain and its caches (go mod, cargo, pip, hex, bundler, NuGet, Gradle)
                # live in HOME, outside the sandbox, and fetch over the network.
                "no-sandbox",
                "requires-network",
                "external",
            ],
        )
