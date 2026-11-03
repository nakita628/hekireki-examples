<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Account extends \Illuminate\Database\Eloquent\Model
{
    protected $table = 'Account';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'user_id',
        'type',
        'provider',
        'providerAccountId',
        'refresh_token',
        'access_token',
        'expires_at',
        'expires_in',
        'ext_expires_in',
        'token_type',
        'scope',
        'id_token',
        'session_state',
        'refresh_token_expires_in',
        'created_at',
    ];

    protected $casts = [
        'expires_at' => 'integer',
        'expires_in' => 'integer',
        'ext_expires_in' => 'integer',
        'refresh_token_expires_in' => 'integer',
        'created_at' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}