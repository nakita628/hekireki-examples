<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Session extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Session';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'sessionToken',
        'userId',
        'ipAddress',
        'userAgent',
        'expiresAt',
        'isEmbed',
    ];

    protected $attributes = [
        'isEmbed' => false,
    ];

    protected $casts = [
        'userId' => 'integer',
        'expiresAt' => 'datetime',
        'isEmbed' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}