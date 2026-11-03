<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class GithubAppInstallation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'GithubAppInstallation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'appInstallationId',
        'targetId',
        'targetType',
        'accountHandle',
        'permissions',
        'repositorySelection',
        'installedBy',
        'installedByUserId',
        'organizationId',
        'deletedAt',
        'suspendedAt',
    ];

    protected $casts = [
        'appInstallationId' => 'integer',
        'targetId' => 'integer',
        'permissions' => 'array',
        'repositorySelection' => GithubRepositorySelection::class,
        'deletedAt' => 'datetime',
        'suspendedAt' => 'datetime',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function repositories(): HasMany
    {
        return $this->hasMany(GithubRepository::class, 'installationId');
    }
}