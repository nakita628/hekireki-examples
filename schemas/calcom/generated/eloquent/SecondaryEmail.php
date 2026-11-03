<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SecondaryEmail extends Model
{
    use PrismaDates;

    protected $table = 'SecondaryEmail';

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'email',
        'emailVerified',
    ];

    protected $casts = [
        'userId' => 'integer',
        'emailVerified' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function verificationTokens(): HasMany
    {
        return $this->hasMany(VerificationToken::class, 'secondaryEmailId');
    }

    public function eventTypes(): HasMany
    {
        return $this->hasMany(EventType::class, 'secondaryEmailId');
    }
}