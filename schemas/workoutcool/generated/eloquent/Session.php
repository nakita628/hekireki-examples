<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Session extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'session';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'expiresAt',
        'token',
        'ipAddress',
        'userAgent',
        'userId',
        'impersonatedBy',
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}