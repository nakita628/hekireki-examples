<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ShortenedLinks extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'shortenedLinks';

    protected $fillable = [
        'uuid',
        'userId',
        'identifier',
        'destination',
        'views',
    ];

    protected $attributes = [
        'views' => 0,
    ];

    protected $casts = [
        'userId' => 'integer',
        'views' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'userId');
    }
}