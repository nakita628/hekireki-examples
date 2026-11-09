<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ProfileBuilderState extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedAt';
    const KEY_COLUMNS = ['profileBuilderId', 'profileBuilderVersion', 'totalInstances', 'instanceIndex'];

    protected $table = 'ProfileBuilderState';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'profileBuilderId',
        'profileBuilderVersion',
        'instanceIndex',
        'totalInstances',
        'startedAt',
        'lastTimestamp',
        'processedUsers',
        'totalUsers',
        'errorUsers',
        'speed',
    ];

    protected $casts = [
        'profileBuilderVersion' => 'integer',
        'instanceIndex' => 'integer',
        'totalInstances' => 'integer',
        'startedAt' => 'datetime',
        'lastTimestamp' => 'datetime',
        'processedUsers' => 'integer',
        'totalUsers' => 'integer',
        'errorUsers' => 'integer',
        'speed' => 'float',
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