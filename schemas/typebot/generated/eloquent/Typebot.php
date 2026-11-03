<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Typebot extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Typebot';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'version',
        'icon',
        'name',
        'folderId',
        'groups',
        'events',
        'variables',
        'edges',
        'theme',
        'selectedThemeTemplateId',
        'settings',
        'publicId',
        'customDomain',
        'workspaceId',
        'resultsTablePreferences',
        'isArchived',
        'isClosed',
        'whatsAppCredentialsId',
        'riskLevel',
        'spaceId',
    ];

    protected $attributes = [
        'isArchived' => false,
        'isClosed' => false,
    ];

    protected $casts = [
        'groups' => 'array',
        'events' => 'array',
        'variables' => 'array',
        'edges' => 'array',
        'theme' => 'array',
        'settings' => 'array',
        'resultsTablePreferences' => 'array',
        'isArchived' => 'boolean',
        'isClosed' => 'boolean',
        'riskLevel' => 'integer',
    ];

    public function folder(): BelongsTo
    {
        return $this->belongsTo(DashboardFolder::class, 'folderId');
    }

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspace::class, 'workspaceId');
    }

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'spaceId');
    }

    public function publishedTypebot(): HasOne
    {
        return $this->hasOne(PublicTypebot::class, 'typebotId');
    }

    public function collaborators(): HasMany
    {
        return $this->hasMany(CollaboratorsOnTypebots::class, 'typebotId');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(Invitation::class, 'typebotId');
    }

    public function results(): HasMany
    {
        return $this->hasMany(Result::class, 'typebotId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'typebotId');
    }

    public function bannedIps(): HasMany
    {
        return $this->hasMany(BannedIp::class, 'responsibleTypebotId');
    }
}