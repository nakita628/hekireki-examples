<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ReprocessingWorkers extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const ZONED_DATES = ['started_at', 'updated_at', 'completed_at'];
    const KEY_COLUMNS = ['job_id', 'worker_index'];

    protected $table = 'reprocessing_workers';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'job_id',
        'worker_index',
        'status',
        'started_at',
        'completed_at',
        'current_file',
        'current_line',
        'current_ts',
        'assigned_files',
        'processed_files',
        'total_lines',
        'success_count',
        'error_count',
        'skipped_count',
        'total_bytes',
        'processed_bytes',
        'error',
    ];

    protected $attributes = [
        'current_line' => 0,
        'assigned_files' => 0,
        'processed_files' => 0,
        'total_lines' => 0,
        'success_count' => 0,
        'error_count' => 0,
        'skipped_count' => 0,
        'total_bytes' => 0,
        'processed_bytes' => 0,
    ];

    protected $casts = [
        'worker_index' => 'integer',
        'started_at' => 'datetime',
        'completed_at' => 'datetime',
        'current_line' => 'integer',
        'assigned_files' => 'integer',
        'processed_files' => 'integer',
        'total_lines' => 'integer',
        'success_count' => 'integer',
        'error_count' => 'integer',
        'skipped_count' => 'integer',
        'total_bytes' => 'integer',
        'processed_bytes' => 'integer',
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

    public function job(): BelongsTo
    {
        return $this->belongsTo(ReprocessingJobs::class, 'job_id');
    }
}