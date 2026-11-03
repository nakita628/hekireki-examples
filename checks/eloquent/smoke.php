<?php
// Loads every generated model against the real Eloquent, which `php -l` cannot see: a trait or
// cast class that does not exist, a method signature that no longer matches the parent's.
declare(strict_types=1);

require __DIR__ . '/vendor/autoload.php';

$dir = $argv[1] ?? __DIR__ . '/models';
$files = glob($dir . '/*.php');
if ($files === false || $files === []) {
    fwrite(STDERR, "no generated models in $dir\n");
    exit(1);
}
// The helpers (PrismaQueryBuilder, casts, ...) are loaded on demand by name.
spl_autoload_register(function (string $class) use ($dir): void {
    $file = $dir . '/' . basename(str_replace('\\', '/', $class)) . '.php';
    if (is_file($file)) {
        require_once $file;
    }
});
$models = 0;
foreach ($files as $file) {
    require_once $file;
}
foreach (get_declared_classes() as $class) {
    if (is_subclass_of($class, Illuminate\Database\Eloquent\Model::class)) {
        $reflection = new ReflectionClass($class);
        if ($reflection->isAbstract()) {
            continue;
        }
        $model = new $class();
        $model->getTable();
        $model->getKeyName();
        $model->getCasts();
        $models++;
    }
}
if ($models === 0) {
    fwrite(STDERR, "no Eloquent models loaded\n");
    exit(1);
}
echo "ok: $models models\n";
