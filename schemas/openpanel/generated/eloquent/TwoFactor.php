<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TwoFactor extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'two_factor';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'secret',
        'backupCodes',
        'verified',
        'failedVerificationCount',
        'lockedUntil',
    ];

    protected $attributes = [
        'verified' => true,
        'failedVerificationCount' => 0,
    ];

    protected $casts = [
        'verified' => 'boolean',
        'failedVerificationCount' => 'integer',
        'lockedUntil' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}