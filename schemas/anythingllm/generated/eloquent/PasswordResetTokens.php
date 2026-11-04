<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class PasswordResetTokens extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'password_reset_tokens';

    protected $fillable = [
        'user_id',
        'token',
        'expiresAt',
    ];

    protected $casts = [
        'user_id' => 'integer',
        'expiresAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'user_id');
    }
}