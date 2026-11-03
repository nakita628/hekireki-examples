<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TableQueryObservationShard extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['window_start', 'created_time', 'last_modified_time'];
    const KEY_COLUMNS = ['table_id', 'query_kind', 'shape_hash', 'window_start', 'writer_id'];

    protected $table = 'table_query_observation_shard';

    protected $primaryKey = null;

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'space_id',
        'base_id',
        'table_id',
        'query_kind',
        'shape_hash',
        'window_start',
        'writer_id',
        'window_size_seconds',
        'request_count',
        'slow_count',
        'timeout_count',
        'db_error_count',
        'total_duration_ms',
        'max_duration_ms',
        'total_db_duration_ms',
        'max_db_duration_ms',
        'shape',
        'sql_diagnostics',
        'created_time',
        'last_modified_time',
    ];

    protected $casts = [
        'window_start' => 'datetime',
        'window_size_seconds' => 'integer',
        'request_count' => 'integer',
        'slow_count' => 'integer',
        'timeout_count' => 'integer',
        'db_error_count' => 'integer',
        'total_duration_ms' => 'float',
        'max_duration_ms' => 'float',
        'total_db_duration_ms' => 'float',
        'max_db_duration_ms' => 'float',
        'shape' => 'array',
        'sql_diagnostics' => 'array',
        'created_time' => 'datetime',
        'last_modified_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

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
}