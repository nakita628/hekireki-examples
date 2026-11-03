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
        'userId',
        'type',
        'provider',
        'providerAccountId',
        'refresh_token',
        'access_token',
        'expires_at',
        'token_type',
        'scope',
        'id_token',
        'session_state',
    ];

    protected $casts = [
        'expires_at' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}