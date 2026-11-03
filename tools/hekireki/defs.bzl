"""Macros for running hekireki over a schema.prisma inside Bazel."""

load("@aspect_rules_js//js:defs.bzl", "js_run_binary")
load("@rules_shell//shell:sh_binary.bzl", "sh_binary")
load("@rules_shell//shell:sh_test.bzl", "sh_test")

def hekireki_generate(name, schema, out_dir = "generated", **kwargs):
    """Runs `prisma generate` on `schema` and holds the committed outputs to what it wrote.

    Every `output` of the schema's generator blocks has to sit under `out_dir`, relative to the
    schema. Three targets come out of it:

    - `<name>`: the generation; its output is the `out_dir` tree in bazel-bin.
    - `<name>.write`: `bazel run` it to copy the tree into the source tree.
    - `<name>.test`: fails when the committed tree differs from a fresh generation.

    Args:
        name: Name of the generating target.
        schema: The schema.prisma.
        out_dir: The directory the generators write under, relative to the schema.
        **kwargs: Passed to every target (`tags`, `visibility`).
    """
    bin_dir = "_" + out_dir
    js_run_binary(
        name = name,
        srcs = [schema],
        args = [
            "--schema",
            "$(rootpath %s)" % schema,
            "--out",
            "%s:%s/%s" % (out_dir, native.package_name(), bin_dir),
        ],
        out_dirs = [bin_dir],
        tool = "//tools/hekireki:generate",
        mnemonic = "HekirekiGenerate",
        progress_message = "Generating %s" % schema,
        # `prisma generate` runs every generator of the schema as a node process of its own, a
        # dozen at a time; this holds the number of schemas generated at once to what the
        # machine's memory takes (see --local_resources).
        tags = ["resources:memory:1500"] + kwargs.pop("tags", []),
        **kwargs
    )
    sh_binary(
        name = "%s.write" % name,
        srcs = ["//tools/hekireki:write_generated.sh"],
        args = [
            "$(rootpath %s)" % name,
            "%s/%s" % (native.package_name(), out_dir),
        ],
        data = [name],
        **kwargs
    )
    sh_test(
        name = "%s.test" % name,
        srcs = ["//tools/hekireki:check_generated.sh"],
        args = [
            "$(rootpath %s)" % name,
            "%s/%s" % (native.package_name(), out_dir),
        ],
        data = [name] + native.glob(["%s/**" % out_dir], allow_empty = True),
        **kwargs
    )
