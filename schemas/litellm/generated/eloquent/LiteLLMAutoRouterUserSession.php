<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMAutoRouterUserSession extends Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['user_id', 'api_key', 'session_id', 'router_name'];

    protected $table = 'LiteLLM_AutoRouterUserSession';

    protected $primaryKey = null;

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'user_id',
        'api_key',
        'session_id',
        'router_name',
        'router_type',
        'first_turn_at',
        'last_turn_at',
        'last_model',
        'models',
        'turns',
        'unordered_turns',
        'covered_turns',
        'cache_hits',
        'same_model_turns',
        'same_model_hits',
        'first_visit_turns',
        'first_visit_hits',
        'return_turns',
        'return_hits',
        'return_expired_misses',
        'return_within_ttl_misses',
        'ttl_5m_turns',
        'ttl_1h_turns',
        'total_tokens',
        'spend',
        'saved_spend',
        'savings_estimated_turns',
        'savings_estimated_actual_spend',
        'savings_estimated_saved_spend',
        'savings_estimated_baseline_models',
        'classifier_cost',
        'classifier_cost_recorded_turns',
        'tier_turns',
        'baseline_models',
    ];

    protected $attributes = [
        'models' => '{}',
        'turns' => 0,
        'unordered_turns' => 0,
        'covered_turns' => 0,
        'cache_hits' => 0,
        'same_model_turns' => 0,
        'same_model_hits' => 0,
        'first_visit_turns' => 0,
        'first_visit_hits' => 0,
        'return_turns' => 0,
        'return_hits' => 0,
        'return_expired_misses' => 0,
        'return_within_ttl_misses' => 0,
        'ttl_5m_turns' => 0,
        'ttl_1h_turns' => 0,
        'total_tokens' => 0,
        'spend' => 0.0,
        'saved_spend' => 0.0,
        'savings_estimated_turns' => 0,
        'savings_estimated_actual_spend' => 0.0,
        'savings_estimated_saved_spend' => 0.0,
        'savings_estimated_baseline_models' => '{}',
        'classifier_cost' => 0.0,
        'classifier_cost_recorded_turns' => 0,
        'tier_turns' => '{}',
        'baseline_models' => '{}',
    ];

    protected $casts = [
        'first_turn_at' => 'datetime',
        'last_turn_at' => 'datetime',
        'models' => 'array',
        'turns' => 'integer',
        'unordered_turns' => 'integer',
        'covered_turns' => 'integer',
        'cache_hits' => 'integer',
        'same_model_turns' => 'integer',
        'same_model_hits' => 'integer',
        'first_visit_turns' => 'integer',
        'first_visit_hits' => 'integer',
        'return_turns' => 'integer',
        'return_hits' => 'integer',
        'return_expired_misses' => 'integer',
        'return_within_ttl_misses' => 'integer',
        'ttl_5m_turns' => 'integer',
        'ttl_1h_turns' => 'integer',
        'total_tokens' => 'integer',
        'spend' => 'float',
        'saved_spend' => 'float',
        'savings_estimated_turns' => 'integer',
        'savings_estimated_actual_spend' => 'float',
        'savings_estimated_saved_spend' => 'float',
        'savings_estimated_baseline_models' => 'array',
        'classifier_cost' => 'float',
        'classifier_cost_recorded_turns' => 'integer',
        'tier_turns' => 'array',
        'baseline_models' => 'array',
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