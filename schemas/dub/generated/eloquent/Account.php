<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Account extends Model
{
    protected $table = 'Account';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userId',
        'type',
        'provider',
        'providerAccountId',
        'refresh_token',
        'refresh_token_expires_in',
        'access_token',
        'expires_at',
        'token_type',
        'scope',
        'id_token',
        'session_state',
    ];

    protected $casts = [
        'refresh_token_expires_in' => 'integer',
        'expires_at' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}