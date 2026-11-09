<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Workspace extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Workspace';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'deleted',
        'slug',
        'featuresEnabled',
    ];

    protected $attributes = [
        'deleted' => false,
    ];

    protected $casts = [
        'deleted' => 'boolean',
    ];

    public function workspaceAccess(): HasMany
    {
        return $this->hasMany(WorkspaceAccess::class, 'workspaceId');
    }

    public function workspaceOptions(): HasMany
    {
        return $this->hasMany(WorkspaceOptions::class, 'workspaceId');
    }

    public function configurationObject(): HasMany
    {
        return $this->hasMany(ConfigurationObject::class, 'workspaceId');
    }

    public function configurationObjectLink(): HasMany
    {
        return $this->hasMany(ConfigurationObjectLink::class, 'workspaceId');
    }

    public function preferences(): HasMany
    {
        return $this->hasMany(UserPreferences::class, 'workspaceId');
    }

    public function workspaceUserProperties(): HasMany
    {
        return $this->hasMany(WorkspaceUserProperties::class, 'workspaceId');
    }

    public function profileBuilders(): HasMany
    {
        return $this->hasMany(ProfileBuilder::class, 'workspaceId');
    }

    public function oidcLoginGroups(): HasMany
    {
        return $this->hasMany(OidcLoginGroup::class, 'workspaceId');
    }
}