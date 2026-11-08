<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ShortURL extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ShortURL';

    protected $fillable = [
        'originalUrl',
        'shortUrlSlug',
        'expiresAt',
        'clicks',
        'userId',
    ];

    protected $attributes = [
        'clicks' => 0,
    ];

    protected $casts = [
        'expiresAt' => 'datetime',
        'clicks' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}