<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Account extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['loggedIn'];

    protected $table = 'Account';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userId',
        'provider',
        'providerAccountId',
        'providerRefreshToken',
        'providerAccessToken',
        'providerScope',
        'loggedIn',
    ];

    protected $casts = [
        'loggedIn' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('loggedIn', $this->attributes)) {
                $this->setAttribute('loggedIn', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}