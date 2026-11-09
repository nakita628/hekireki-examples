<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ReverseSyncControl extends Model
{
    const KEY_COLUMNS = ['workspace_id', 'sync_id', 'run_id'];

    protected $table = 'reverse_sync_control';

    protected $primaryKey = null;

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'workspace_id',
        'sync_id',
        'run_id',
        'run_order',
        'detached',
        'revision',
        'target_hash',
        'phase',
        'mode',
        'extraction',
        'artifact_head',
        'base_sequence',
        'next_sequence',
        'checkpoint_sequence',
        'finish_sequence',
        'store',
        'provider_state',
        'finish_result',
        'committed_generation',
    ];

    protected $attributes = [
        'run_order' => 0,
        'detached' => false,
        'phase' => 'new',
        'base_sequence' => 0,
        'next_sequence' => 0,
        'checkpoint_sequence' => 0,
    ];

    protected $casts = [
        'run_order' => 'integer',
        'detached' => 'boolean',
        'mode' => reverse_sync_mode::class,
        'extraction' => reverse_sync_extraction::class,
        'artifact_head' => AsBytes::class,
        'base_sequence' => 'integer',
        'next_sequence' => 'integer',
        'checkpoint_sequence' => 'integer',
        'finish_sequence' => 'integer',
        'store' => AsBytes::class,
        'provider_state' => AsBytes::class,
        'finish_result' => AsBytes::class,
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