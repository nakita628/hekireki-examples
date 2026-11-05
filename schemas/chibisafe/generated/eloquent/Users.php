<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Users extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'users';

    protected $fillable = [
        'uuid',
        'username',
        'password',
        'enabled',
        'storageQuota',
        'apiKey',
        'passwordEditedAt',
        'apiKeyEditedAt',
        'editedAt',
    ];

    protected $attributes = [
        'enabled' => true,
        'storageQuota' => '0',
    ];

    protected $casts = [
        'enabled' => 'boolean',
        'passwordEditedAt' => 'datetime',
        'apiKeyEditedAt' => 'datetime',
        'editedAt' => 'datetime',
    ];

    public function files(): HasMany
    {
        return $this->hasMany(Files::class, 'userId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(ShortenedLinks::class, 'userId');
    }

    public function roles(): BelongsToMany
    {
        return $this->belongsToMany(Roles::class, '_rolesTousers', 'B', 'A');
    }
}