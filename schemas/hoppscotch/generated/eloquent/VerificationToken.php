<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class VerificationToken extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['expiresOn'];

    protected $table = 'VerificationToken';

    protected $primaryKey = 'token';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'token',
        'deviceIdentifier',
        'userUid',
        'expiresOn',
    ];

    protected $casts = [
        'expiresOn' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userUid');
    }
}