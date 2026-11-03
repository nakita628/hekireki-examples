<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Host extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const KEY_COLUMNS = ['userId', 'eventTypeId'];

    protected $table = 'Host';

    protected $primaryKey = null;

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'eventTypeId',
        'isFixed',
        'priority',
        'weight',
        'weightAdjustment',
        'scheduleId',
        'groupId',
        'memberId',
    ];

    protected $attributes = [
        'isFixed' => false,
    ];

    protected $casts = [
        'userId' => 'integer',
        'eventTypeId' => 'integer',
        'isFixed' => 'boolean',
        'priority' => 'integer',
        'weight' => 'integer',
        'weightAdjustment' => 'integer',
        'scheduleId' => 'integer',
        'memberId' => 'integer',
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

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function schedule(): BelongsTo
    {
        return $this->belongsTo(Schedule::class, 'scheduleId');
    }

    public function group(): BelongsTo
    {
        return $this->belongsTo(HostGroup::class, 'groupId');
    }

    public function member(): BelongsTo
    {
        return $this->belongsTo(Membership::class, 'memberId');
    }
}