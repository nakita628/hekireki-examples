<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMAutoRouterDailySpend extends Model
{
    const KEY_COLUMNS = ['date', 'api_key', 'user_id', 'router_name', 'router_type'];

    protected $table = 'LiteLLM_AutoRouterDailySpend';

    protected $primaryKey = null;

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'date',
        'api_key',
        'user_id',
        'router_name',
        'router_type',
        'turns',
        'total_tokens',
        'token_recorded_turns',
        'spend',
        'saved_spend',
        'savings_estimated_turns',
        'savings_estimated_actual_spend',
        'savings_estimated_saved_spend',
        'classifier_cost',
        'classifier_cost_recorded_turns',
    ];

    protected $attributes = [
        'turns' => 0,
        'total_tokens' => 0,
        'token_recorded_turns' => 0,
        'spend' => 0.0,
        'saved_spend' => 0.0,
        'savings_estimated_turns' => 0,
        'savings_estimated_actual_spend' => 0.0,
        'savings_estimated_saved_spend' => 0.0,
        'classifier_cost' => 0.0,
        'classifier_cost_recorded_turns' => 0,
    ];

    protected $casts = [
        'turns' => 'integer',
        'total_tokens' => 'integer',
        'token_recorded_turns' => 'integer',
        'spend' => 'float',
        'saved_spend' => 'float',
        'savings_estimated_turns' => 'integer',
        'savings_estimated_actual_spend' => 'float',
        'savings_estimated_saved_spend' => 'float',
        'classifier_cost' => 'float',
        'classifier_cost_recorded_turns' => 'integer',
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