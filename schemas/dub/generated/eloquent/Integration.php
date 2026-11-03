<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Integration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Integration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'userId',
        'projectId',
        'name',
        'slug',
        'description',
        'readme',
        'developer',
        'website',
        'logo',
        'screenshots',
        'verified',
        'installUrl',
        'guideUrl',
        'category',
        'comingSoon',
    ];

    protected $attributes = [
        'verified' => false,
        'comingSoon' => false,
    ];

    protected $casts = [
        'screenshots' => 'array',
        'verified' => 'boolean',
        'comingSoon' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function oAuthApp(): HasOne
    {
        return $this->hasOne(OAuthApp::class, 'integrationId');
    }

    public function installations(): HasMany
    {
        return $this->hasMany(InstalledIntegration::class, 'integrationId');
    }
}