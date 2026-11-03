<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class InvitedUsers extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['invitedOn'];

    protected $table = 'InvitedUsers';

    protected $primaryKey = 'inviteeEmail';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'inviteeEmail',
        'adminUid',
        'adminEmail',
        'invitedOn',
    ];

    protected $casts = [
        'invitedOn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('invitedOn', $this->attributes)) {
                $this->setAttribute('invitedOn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'adminUid');
    }
}