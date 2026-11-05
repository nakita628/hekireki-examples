<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Albums extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'albums';

    protected $fillable = [
        'uuid',
        'userId',
        'name',
        'description',
        'zippedAt',
        'editedAt',
        'nsfw',
        'sortOrder',
    ];

    protected $attributes = [
        'nsfw' => false,
    ];

    protected $casts = [
        'userId' => 'integer',
        'zippedAt' => 'datetime',
        'editedAt' => 'datetime',
        'nsfw' => 'boolean',
    ];

    public function files(): BelongsToMany
    {
        return $this->belongsToMany(Files::class, '_albumsTofiles', 'A', 'B');
    }
}