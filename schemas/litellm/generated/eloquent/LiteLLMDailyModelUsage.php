<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMDailyModelUsage extends Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['date', 'model_group', 'model', 'custom_llm_provider', 'task_type'];

    protected $table = 'LiteLLM_DailyModelUsage';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'date',
        'model_group',
        'model',
        'custom_llm_provider',
        'task_type',
        'spend',
        'prompt_tokens',
        'completion_tokens',
        'request_count',
        'successful_requests',
        'failed_requests',
        'total_response_time_ms',
        'timed_requests',
    ];

    protected $attributes = [
        'spend' => 0.0,
        'prompt_tokens' => 0,
        'completion_tokens' => 0,
        'request_count' => 0,
        'successful_requests' => 0,
        'failed_requests' => 0,
        'total_response_time_ms' => 0,
        'timed_requests' => 0,
    ];

    protected $casts = [
        'spend' => 'float',
        'prompt_tokens' => 'integer',
        'completion_tokens' => 'integer',
        'request_count' => 'integer',
        'successful_requests' => 'integer',
        'failed_requests' => 'integer',
        'total_response_time_ms' => 'integer',
        'timed_requests' => 'integer',
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