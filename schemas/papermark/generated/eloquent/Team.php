<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Team extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Team';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'slug',
        'defaultBrandId',
        'plan',
        'stripeId',
        'subscriptionId',
        'startsAt',
        'endsAt',
        'pausedAt',
        'pauseStartsAt',
        'pauseEndsAt',
        'cancelledAt',
        'trialStartedAt',
        'trialEndsAt',
        'trialReminderRunId',
        'trialExpiredRunId',
        'limits',
        'enableExcelAdvancedMode',
        'replicateDataroomFolders',
        'timezone',
        'ssoEnabled',
        'ssoEmailDomain',
        'ssoEnforcedAt',
        'surveyData',
        'agentsEnabled',
        'vectorStoreId',
        'ignoredDomains',
        'globalBlockList',
        'referralTrack',
        'referralTrackSelectedAt',
        'referralProfile',
    ];

    protected $attributes = [
        'plan' => 'free',
        'enableExcelAdvancedMode' => false,
        'replicateDataroomFolders' => true,
        'timezone' => 'Etc/UTC',
        'ssoEnabled' => false,
        'agentsEnabled' => false,
    ];

    protected $casts = [
        'startsAt' => 'datetime',
        'endsAt' => 'datetime',
        'pausedAt' => 'datetime',
        'pauseStartsAt' => 'datetime',
        'pauseEndsAt' => 'datetime',
        'cancelledAt' => 'datetime',
        'trialStartedAt' => 'datetime',
        'trialEndsAt' => 'datetime',
        'limits' => 'array',
        'enableExcelAdvancedMode' => 'boolean',
        'replicateDataroomFolders' => 'boolean',
        'ssoEnabled' => 'boolean',
        'ssoEnforcedAt' => 'datetime',
        'surveyData' => 'array',
        'agentsEnabled' => 'boolean',
        'referralTrack' => ReferralTrack::class,
        'referralTrackSelectedAt' => 'datetime',
        'referralProfile' => 'array',
    ];

    public function defaultBrand(): BelongsTo
    {
        return $this->belongsTo(Brand::class, 'defaultBrandId');
    }

    public function users(): HasMany
    {
        return $this->hasMany(UserTeam::class, 'teamId');
    }

    public function userDatarooms(): HasMany
    {
        return $this->hasMany(UserDataroom::class, 'teamId');
    }

    public function documents(): HasMany
    {
        return $this->hasMany(Document::class, 'teamId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(Folder::class, 'teamId');
    }

    public function domains(): HasMany
    {
        return $this->hasMany(Domain::class, 'teamId');
    }

    public function invitations(): HasMany
    {
        return $this->hasMany(Invitation::class, 'teamId');
    }

    public function sentEmails(): HasMany
    {
        return $this->hasMany(SentEmail::class, 'teamId');
    }

    public function brands(): HasMany
    {
        return $this->hasMany(Brand::class, 'teamId');
    }

    public function datarooms(): HasMany
    {
        return $this->hasMany(Dataroom::class, 'teamId');
    }

    public function agreements(): HasMany
    {
        return $this->hasMany(Agreement::class, 'teamId');
    }

    public function viewerGroups(): HasMany
    {
        return $this->hasMany(ViewerGroup::class, 'teamId');
    }

    public function visitorGroups(): HasMany
    {
        return $this->hasMany(VisitorGroup::class, 'teamId');
    }

    public function viewers(): HasMany
    {
        return $this->hasMany(Viewer::class, 'teamId');
    }

    public function permissionGroups(): HasMany
    {
        return $this->hasMany(PermissionGroup::class, 'teamId');
    }

    public function linkPresets(): HasMany
    {
        return $this->hasMany(LinkPreset::class, 'teamId');
    }

    public function incomingWebhooks(): HasMany
    {
        return $this->hasMany(IncomingWebhook::class, 'teamId');
    }

    public function restrictedTokens(): HasMany
    {
        return $this->hasMany(RestrictedToken::class, 'teamId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'teamId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'teamId');
    }

    public function dataroomQuestions(): HasMany
    {
        return $this->hasMany(DataroomQuestion::class, 'teamId');
    }

    public function dataroomFaqItems(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'teamId');
    }

    public function uploadedDocuments(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'teamId');
    }

    public function Tag(): HasMany
    {
        return $this->hasMany(Tag::class, 'teamId');
    }

    public function annotations(): HasMany
    {
        return $this->hasMany(DocumentAnnotation::class, 'teamId');
    }

    public function redactionJobs(): HasMany
    {
        return $this->hasMany(DocumentRedactionJob::class, 'teamId');
    }

    public function installedIntegrations(): HasMany
    {
        return $this->hasMany(InstalledIntegration::class, 'teamId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'teamId');
    }

    public function views(): HasMany
    {
        return $this->hasMany(View::class, 'teamId');
    }

    public function workflows(): HasMany
    {
        return $this->hasMany(Workflow::class, 'teamId');
    }

    public function taskLists(): HasMany
    {
        return $this->hasMany(TaskList::class, 'teamId');
    }

    public function tasks(): HasMany
    {
        return $this->hasMany(Task::class, 'teamId');
    }

    public function chats(): HasMany
    {
        return $this->hasMany(Chat::class, 'teamId');
    }

    public function notificationPreferences(): HasMany
    {
        return $this->hasMany(NotificationPreference::class, 'teamId');
    }

    public function referralInvites(): HasMany
    {
        return $this->hasMany(ReferralInvite::class, 'teamId');
    }
}