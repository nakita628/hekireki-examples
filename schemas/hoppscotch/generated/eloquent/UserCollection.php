<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class UserCollection extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['createdOn', 'updatedOn'];

    protected $table = 'UserCollection';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'parentID',
        'userUid',
        'title',
        'type',
        'orderIndex',
        'data',
        'createdOn',
    ];

    protected $casts = [
        'type' => ReqType::class,
        'orderIndex' => 'integer',
        'data' => 'array',
        'createdOn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdOn', $this->attributes)) {
                $this->setAttribute('createdOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function parent(): BelongsTo
    {
        return $this->belongsTo(UserCollection::class, 'parentID');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userUid');
    }

    public function children(): HasMany
    {
        return $this->hasMany(UserCollection::class, 'parentID');
    }

    public function requests(): HasMany
    {
        return $this->hasMany(UserRequest::class, 'collectionID');
    }
}