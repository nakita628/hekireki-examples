<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Media extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const KEY_COLUMNS = ['project_id', 'id'];

    protected $table = 'media';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'id',
        'sha_256_hash',
        'project_id',
        'uploaded_at',
        'upload_http_status',
        'upload_http_error',
        'bucket_path',
        'bucket_name',
        'content_type',
        'content_length',
    ];

    protected $casts = [
        'uploaded_at' => 'datetime',
        'upload_http_status' => 'integer',
        'content_length' => 'integer',
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
}