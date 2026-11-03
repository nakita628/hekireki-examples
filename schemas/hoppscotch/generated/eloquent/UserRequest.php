<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserRequest extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['createdOn', 'updatedOn'];

    protected $table = 'UserRequest';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'collectionID',
        'userUid',
        'title',
        'request',
        'type',
        'mockExamples',
        'orderIndex',
        'createdOn',
    ];

    protected $casts = [
        'request' => 'array',
        'type' => ReqType::class,
        'mockExamples' => 'array',
        'orderIndex' => 'integer',
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

    public function userCollection(): BelongsTo
    {
        return $this->belongsTo(UserCollection::class, 'collectionID');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userUid');
    }
}