<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMDailyPolicyMetrics extends Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['policy_id', 'date'];

    protected $table = 'LiteLLM_DailyPolicyMetrics';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'policy_id',
        'date',
        'requests_evaluated',
        'passed_count',
        'blocked_count',
        'flagged_count',
        'avg_score',
        'avg_latency_ms',
    ];

    protected $attributes = [
        'requests_evaluated' => 0,
        'passed_count' => 0,
        'blocked_count' => 0,
        'flagged_count' => 0,
    ];

    protected $casts = [
        'requests_evaluated' => 'integer',
        'passed_count' => 'integer',
        'blocked_count' => 'integer',
        'flagged_count' => 'integer',
        'avg_score' => 'float',
        'avg_latency_ms' => 'float',
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