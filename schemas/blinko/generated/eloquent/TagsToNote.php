<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TagsToNote extends Model
{
    const KEY_COLUMNS = ['noteId', 'tagId'];

    protected $table = 'tagsToNote';

    protected $primaryKey = null;

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'noteId',
        'tagId',
    ];

    protected $attributes = [
        'noteId' => 0,
        'tagId' => 0,
    ];

    protected $casts = [
        'id' => 'integer',
        'noteId' => 'integer',
        'tagId' => 'integer',
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

    public function note(): BelongsTo
    {
        return $this->belongsTo(Notes::class, 'noteId');
    }

    public function tag(): BelongsTo
    {
        return $this->belongsTo(Tag::class, 'tagId');
    }
}