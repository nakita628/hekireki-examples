<?php

namespace App\Models;


class DatasetItem extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['id', 'project_id', 'valid_from'];

    protected $table = 'dataset_items';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'status',
        'input',
        'expected_output',
        'metadata',
        'source_trace_id',
        'source_observation_id',
        'dataset_id',
        'valid_from',
        'valid_to',
        'is_deleted',
    ];

    protected $attributes = [
        'status' => 'ACTIVE',
        'is_deleted' => false,
    ];

    protected $casts = [
        'status' => DatasetStatus::class,
        'input' => 'array',
        'expected_output' => 'array',
        'metadata' => 'array',
        'valid_from' => 'datetime',
        'valid_to' => 'datetime',
        'is_deleted' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('valid_from', $this->attributes)) {
                $this->setAttribute('valid_from', $this->freshTimestamp());
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