<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserHistory extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['executedOn'];

    protected $table = 'UserHistory';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userUid',
        'reqType',
        'request',
        'responseMetadata',
        'isStarred',
        'executedOn',
    ];

    protected $casts = [
        'reqType' => ReqType::class,
        'request' => 'array',
        'responseMetadata' => 'array',
        'isStarred' => 'boolean',
        'executedOn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('executedOn', $this->attributes)) {
                $this->setAttribute('executedOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userUid');
    }
}