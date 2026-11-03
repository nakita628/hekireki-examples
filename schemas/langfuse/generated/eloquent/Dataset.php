<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Dataset extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['id', 'project_id'];

    protected $table = 'datasets';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'name',
        'description',
        'metadata',
        'remote_experiment_url',
        'remote_experiment_payload',
        'remote_experiment_enabled',
        'remote_experiment_secret_key',
        'remote_experiment_display_secret_key',
        'remote_experiment_request_headers',
        'remote_experiment_display_headers',
        'input_schema',
        'expected_output_schema',
    ];

    protected $attributes = [
        'remote_experiment_enabled' => true,
    ];

    protected $casts = [
        'metadata' => 'array',
        'remote_experiment_payload' => 'array',
        'remote_experiment_enabled' => 'boolean',
        'remote_experiment_request_headers' => 'array',
        'remote_experiment_display_headers' => 'array',
        'input_schema' => 'array',
        'expected_output_schema' => 'array',
    ];

    public function getKey()
    {
        $key = [];
        foreach (static::KEY_COLUMNS as $column) {
            $key[$column] = $this->getAttribute($column);
        }

        return $key;
    }

    public function newEloquentBuilder($query)
    {
        return new CompositeKeyBuilder($query);
    }

    public function newCollection(array $models = [])
    {
        return new CompositeKeyCollection($models);
    }

    public static function destroy($ids)
    {
        $count = 0;
        foreach ((new static())->newQuery()->whereKey(func_num_args() > 1 ? func_get_args() : $ids)->get() as $model) {
            if ($model->delete()) {
                $count++;
            }
        }

        return $count;
    }

    protected function setKeysForSaveQuery($query)
    {
        foreach (static::KEY_COLUMNS as $column) {
            $query->where($column, '=', $this->original[$column] ?? $this->getAttribute($column));
        }

        return $query;
    }

    protected function setKeysForSelectQuery($query)
    {
        foreach (static::KEY_COLUMNS as $column) {
            $query->where($column, '=', $this->original[$column] ?? $this->getAttribute($column));
        }

        return $query;
    }

    public function delete()
    {
        $this->mergeAttributesFromCachedCasts();

        if (! $this->exists) {
            return null;
        }

        if ($this->fireModelEvent('deleting') === false) {
            return false;
        }

        $this->touchOwners();
        $this->performDeleteOnModel();
        $this->fireModelEvent('deleted', false);

        return true;
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'project_id');
    }
}