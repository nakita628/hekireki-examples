<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Shortcode extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedOn';
    const ZONED_DATES = ['createdOn', 'updatedOn'];

    protected $table = 'Shortcode';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'request',
        'creatorUid',
        'embedProperties',
        'createdOn',
    ];

    protected $casts = [
        'request' => 'array',
        'embedProperties' => 'array',
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

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'creatorUid');
    }
}