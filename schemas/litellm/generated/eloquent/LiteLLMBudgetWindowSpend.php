<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMBudgetWindowSpend extends Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['entity_type', 'entity_id', 'window_duration'];

    protected $table = 'LiteLLM_BudgetWindowSpend';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'entity_type',
        'entity_id',
        'window_duration',
        'window_start',
        'spend',
    ];

    protected $attributes = [
        'spend' => 0.0,
    ];

    protected $casts = [
        'window_start' => 'datetime',
        'spend' => 'float',
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
}