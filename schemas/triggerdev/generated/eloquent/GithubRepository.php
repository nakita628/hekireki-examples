<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class GithubRepository extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'GithubRepository';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'githubId',
        'name',
        'fullName',
        'htmlUrl',
        'private',
        'defaultBranch',
        'installationId',
    ];

    protected $casts = [
        'githubId' => 'integer',
        'private' => 'boolean',
    ];

    public function installation(): BelongsTo
    {
        return $this->belongsTo(GithubAppInstallation::class, 'installationId');
    }

    public function ConnectedGithubRepository(): HasMany
    {
        return $this->hasMany(ConnectedGithubRepository::class, 'repositoryId');
    }
}