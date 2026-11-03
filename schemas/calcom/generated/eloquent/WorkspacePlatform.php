<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkspacePlatform extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'WorkspacePlatform';

    protected $fillable = [
        'slug',
        'name',
        'description',
        'defaultServiceAccountKey',
        'enabled',
    ];

    protected $attributes = [
        'enabled' => false,
    ];

    protected $casts = [
        'defaultServiceAccountKey' => 'array',
        'enabled' => 'boolean',
    ];

    public function delegationCredentials(): HasMany
    {
        return $this->hasMany(DelegationCredential::class, 'workspacePlatformId');
    }
}