<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LiteLLMAdaptiveRouterSession extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_activity_at';
    const KEY_COLUMNS = ['session_id', 'router_name', 'model_name'];

    protected $table = 'LiteLLM_AdaptiveRouterSession';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'session_id',
        'router_name',
        'model_name',
        'classified_type',
        'misalignment_count',
        'stagnation_count',
        'disengagement_count',
        'satisfaction_count',
        'failure_count',
        'loop_count',
        'exhaustion_count',
        'last_user_content',
        'last_assistant_content',
        'tool_call_history',
        'pending_tool_calls',
        'turn_count',
        'last_processed_turn',
        'clean_credit_awarded',
        'terminal_status',
    ];

    protected $attributes = [
        'misalignment_count' => 0,
        'stagnation_count' => 0,
        'disengagement_count' => 0,
        'satisfaction_count' => 0,
        'failure_count' => 0,
        'loop_count' => 0,
        'exhaustion_count' => 0,
        'tool_call_history' => '[]',
        'pending_tool_calls' => '{}',
        'turn_count' => 0,
        'last_processed_turn' => -1,
        'clean_credit_awarded' => false,
    ];

    protected $casts = [
        'misalignment_count' => 'integer',
        'stagnation_count' => 'integer',
        'disengagement_count' => 'integer',
        'satisfaction_count' => 'integer',
        'failure_count' => 'integer',
        'loop_count' => 'integer',
        'exhaustion_count' => 'integer',
        'tool_call_history' => 'array',
        'pending_tool_calls' => 'array',
        'turn_count' => 'integer',
        'last_processed_turn' => 'integer',
        'clean_credit_awarded' => 'boolean',
        'terminal_status' => 'integer',
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