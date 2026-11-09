<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class UserProfile extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'UserProfile';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'email',
        'admin',
        'loginProvider',
        'externalUsername',
        'externalId',
    ];

    protected $attributes = [
        'admin' => false,
    ];

    protected $casts = [
        'admin' => 'boolean',
    ];

    public function password(): HasOne
    {
        return $this->hasOne(UserPassword::class, 'userId');
    }

    public function tokens(): HasMany
    {
        return $this->hasMany(UserApiToken::class, 'userId');
    }

    public function workspaceAccess(): HasMany
    {
        return $this->hasMany(WorkspaceAccess::class, 'userId');
    }

    public function userPreferences(): HasMany
    {
        return $this->hasMany(UserPreferences::class, 'userId');
    }

    public function workspaceUserProperties(): HasMany
    {
        return $this->hasMany(WorkspaceUserProperties::class, 'userId');
    }
}