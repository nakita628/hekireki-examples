<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ConnectedGithubRepository extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ConnectedGithubRepository';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'projectId',
        'repositoryId',
        'branchTracking',
        'previewDeploymentsEnabled',
        'buildFilters',
    ];

    protected $attributes = [
        'previewDeploymentsEnabled' => true,
    ];

    protected $casts = [
        'branchTracking' => 'array',
        'previewDeploymentsEnabled' => 'boolean',
        'buildFilters' => 'array',
    ];

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function repository(): BelongsTo
    {
        return $this->belongsTo(GithubRepository::class, 'repositoryId');
    }
}