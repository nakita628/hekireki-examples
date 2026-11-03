<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ReferralInvite extends Model
{
    use PrismaDates;

    protected $table = 'ReferralInvite';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userId',
        'teamId',
        'scopeId',
        'email',
        'referralLink',
        'sentAt',
    ];

    protected $casts = [
        'sentAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('sentAt', $this->attributes)) {
                $this->setAttribute('sentAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}