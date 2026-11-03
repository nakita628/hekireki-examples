<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Account extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'Account';

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
        'created_at',
        'ext_expires_in',
        'token_type',
        'scope',
        'id_token',
        'session_state',
        'password',
    ];

    protected $casts = [
        'userId' => 'integer',
        'expires_at' => 'integer',
        'created_at' => 'integer',
        'ext_expires_in' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}