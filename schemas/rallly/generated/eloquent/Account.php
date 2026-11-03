<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Account extends Model
{
    use PrismaDates;

    protected $table = 'accounts';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'user_id',
        'provider',
        'provider_account_id',
        'refresh_token',
        'access_token',
        'expires_at',
        'scope',
        'id_token',
        'access_token_expires_at',
        'refresh_token_expires_at',
        'password',
    ];

    protected $casts = [
        'expires_at' => 'integer',
        'access_token_expires_at' => 'datetime',
        'refresh_token_expires_at' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}