<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

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
        'icon',
        'plan',
        'stripeId',
        'additionalChatsIndex',
        'additionalStorageIndex',
        'chatsLimitFirstEmailSentAt',
        'storageLimitFirstEmailSentAt',
        'chatsLimitSecondEmailSentAt',
        'storageLimitSecondEmailSentAt',
        'customChatsLimit',
        'customStorageLimit',
        'customSeatsLimit',
        'isQuarantined',
        'isSuspended',
        'isPastDue',
        'isVerified',
        'chatsHardLimit',
        'settings',
        'lastActivityAt',
    ];

    protected $attributes = [
        'plan' => 'FREE',
        'additionalChatsIndex' => 0,
        'additionalStorageIndex' => 0,
        'isQuarantined' => false,
        'isSuspended' => false,
        'isPastDue' => false,
    ];

    protected $casts = [
        'plan' => Plan::class,
        'additionalChatsIndex' => 'integer',
        'additionalStorageIndex' => 'integer',
        'chatsLimitFirstEmailSentAt' => 'datetime',
        'storageLimitFirstEmailSentAt' => 'datetime',
        'chatsLimitSecondEmailSentAt' => 'datetime',
        'storageLimitSecondEmailSentAt' => 'datetime',
        'customChatsLimit' => 'integer',
        'customStorageLimit' => 'integer',
        'customSeatsLimit' => 'integer',
        'isQuarantined' => 'boolean',
        'isSuspended' => 'boolean',
        'isPastDue' => 'boolean',
        'isVerified' => 'boolean',
        'chatsHardLimit' => 'integer',
        'settings' => 'array',
        'lastActivityAt' => 'datetime',
    ];

    public function claimableCustomPlan(): HasOne
    {
        return $this->hasOne(ClaimableCustomPlan::class, 'workspaceId');
    }

    public function credentials(): HasMany
    {
        return $this->hasMany(Credentials::class, 'workspaceId');
    }

    public function customDomains(): HasMany
    {
        return $this->hasMany(CustomDomain::class, 'workspaceId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(DashboardFolder::class, 'workspaceId');
    }

    public function members(): HasMany
    {
        return $this->hasMany(MemberInWorkspace::class, 'workspaceId');
    }

    public function typebots(): HasMany
    {
        return $this->hasMany(Typebot::class, 'workspaceId');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(WorkspaceInvitation::class, 'workspaceId');
    }

    public function themeTemplates(): HasMany
    {
        return $this->hasMany(ThemeTemplate::class, 'workspaceId');
    }

    public function spaces(): HasMany
    {
        return $this->hasMany(Space::class, 'workspaceId');
    }
}