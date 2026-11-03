<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class InAppAgentConversation extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['id', 'project_id'];

    protected $table = 'in_app_agent_conversations';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'created_by_user_id',
        'title',
        'renamed_by_user_at',
        'visibility_scope',
        'provider_session_id',
        'always_allowed_tools',
        'deleted_at',
    ];

    protected $attributes = [
        'visibility_scope' => 'PERSONAL',
    ];

    protected $casts = [
        'renamed_by_user_at' => 'datetime',
        'visibility_scope' => InAppAgentConversationVisibilityScope::class,
        'deleted_at' => 'datetime',
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

    public function createdByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'created_by_user_id');
    }
}