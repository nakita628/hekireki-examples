<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PendingPasskeyRegistration extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'pending_passkey_registrations';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'verification_token',
        'verification_token_expires_at',
        'credential_id',
        'public_key',
        'counter',
        'transports',
        'user_id',
    ];

    protected $attributes = [
        'counter' => 0,
    ];

    protected $casts = [
        'verification_token_expires_at' => 'integer',
        'credential_id' => AsBytes::class,
        'public_key' => AsBytes::class,
        'counter' => 'integer',
        'user_id' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}