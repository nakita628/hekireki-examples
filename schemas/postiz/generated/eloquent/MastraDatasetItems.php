<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraDatasetItems extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';
    const ZONED_DATES = ['createdAtZ', 'updatedAtZ'];
    const KEY_COLUMNS = ['id', 'datasetVersion'];

    protected $table = 'mastra_dataset_items';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'id',
        'datasetId',
        'datasetVersion',
        'validTo',
        'isDeleted',
        'input',
        'groundTruth',
        'requestContext',
        'metadata',
        'source',
        'createdAtZ',
        'updatedAtZ',
        'expectedTrajectory',
        'organizationId',
        'projectId',
        'toolMocks',
        'unmockedToolPolicy',
        'scorerIds',
        'externalId',
    ];

    protected $casts = [
        'datasetVersion' => 'integer',
        'validTo' => 'integer',
        'isDeleted' => 'boolean',
        'input' => 'array',
        'groundTruth' => 'array',
        'requestContext' => 'array',
        'metadata' => 'array',
        'source' => 'array',
        'createdAtZ' => 'datetime',
        'updatedAtZ' => 'datetime',
        'expectedTrajectory' => 'array',
        'toolMocks' => 'array',
        'scorerIds' => 'array',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdAtZ', $this->attributes)) {
                $this->setAttribute('createdAtZ', $this->freshTimestamp());
            }
            if (! array_key_exists('updatedAtZ', $this->attributes)) {
                $this->setAttribute('updatedAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

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