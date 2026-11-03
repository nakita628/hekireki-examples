<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Link extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Link';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'documentId',
        'dataroomId',
        'linkType',
        'url',
        'name',
        'slug',
        'expiresAt',
        'password',
        'allowList',
        'denyList',
        'emailProtected',
        'emailAuthenticated',
        'allowDownload',
        'isArchived',
        'deletedAt',
        'domainId',
        'domainSlug',
        'enableNotification',
        'enableFeedback',
        'enableQuestion',
        'enableScreenshotProtection',
        'enableConfidentialView',
        'enableAgreement',
        'agreementId',
        'showBanner',
        'enableWatermark',
        'watermarkConfig',
        'audienceType',
        'groupId',
        'permissionGroupId',
        'metaTitle',
        'metaDescription',
        'metaImage',
        'metaFavicon',
        'enableCustomMetatag',
        'welcomeMessage',
        'enableConversation',
        'enableAIAgents',
        'enableUpload',
        'isFileRequestOnly',
        'uploadFolderId',
        'uploadFolderIds',
        'enableIndexFile',
        'teamId',
        'brandId',
        'ownerId',
    ];

    protected $attributes = [
        'linkType' => 'DOCUMENT_LINK',
        'emailProtected' => true,
        'emailAuthenticated' => false,
        'allowDownload' => false,
        'isArchived' => false,
        'enableNotification' => true,
        'enableFeedback' => false,
        'enableQuestion' => false,
        'enableScreenshotProtection' => false,
        'enableConfidentialView' => false,
        'enableAgreement' => false,
        'showBanner' => false,
        'enableWatermark' => false,
        'audienceType' => 'GENERAL',
        'enableCustomMetatag' => false,
        'enableConversation' => false,
        'enableAIAgents' => false,
        'enableUpload' => false,
        'isFileRequestOnly' => false,
        'enableIndexFile' => false,
    ];

    protected $casts = [
        'linkType' => LinkType::class,
        'expiresAt' => 'datetime',
        'emailProtected' => 'boolean',
        'emailAuthenticated' => 'boolean',
        'allowDownload' => 'boolean',
        'isArchived' => 'boolean',
        'deletedAt' => 'datetime',
        'enableNotification' => 'boolean',
        'enableFeedback' => 'boolean',
        'enableQuestion' => 'boolean',
        'enableScreenshotProtection' => 'boolean',
        'enableConfidentialView' => 'boolean',
        'enableAgreement' => 'boolean',
        'showBanner' => 'boolean',
        'enableWatermark' => 'boolean',
        'watermarkConfig' => 'array',
        'audienceType' => LinkAudienceType::class,
        'enableCustomMetatag' => 'boolean',
        'enableConversation' => 'boolean',
        'enableAIAgents' => 'boolean',
        'enableUpload' => 'boolean',
        'isFileRequestOnly' => 'boolean',
        'enableIndexFile' => 'boolean',
    ];

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function domain(): BelongsTo
    {
        return $this->belongsTo(Domain::class, 'domainId');
    }

    public function agreement(): BelongsTo
    {
        return $this->belongsTo(Agreement::class, 'agreementId');
    }

    public function group(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'groupId');
    }

    public function permissionGroup(): BelongsTo
    {
        return $this->belongsTo(PermissionGroup::class, 'permissionGroupId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function brand(): BelongsTo
    {
        return $this->belongsTo(Brand::class, 'brandId');
    }

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'ownerId');
    }

    public function feedback(): HasOne
    {
        return $this->hasOne(Feedback::class, 'linkId');
    }

    public function workflow(): HasOne
    {
        return $this->hasOne(Workflow::class, 'entryLinkId');
    }

    public function views(): HasMany
    {
        return $this->hasMany(View::class, 'linkId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'linkId');
    }

    public function questionAssignments(): HasMany
    {
        return $this->hasMany(DataroomQuestionAssignment::class, 'linkId');
    }

    public function dataroomFaqItems(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'linkId');
    }

    public function chats(): HasMany
    {
        return $this->hasMany(Chat::class, 'linkId');
    }

    public function uploadedDocuments(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'linkId');
    }

    public function customFields(): HasMany
    {
        return $this->hasMany(CustomField::class, 'linkId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(TagItem::class, 'linkId');
    }

    public function viewerInvitations(): HasMany
    {
        return $this->hasMany(ViewerInvitation::class, 'linkId');
    }

    public function visitorGroups(): HasMany
    {
        return $this->hasMany(LinkVisitorGroup::class, 'linkId');
    }

    public function taskAssignments(): HasMany
    {
        return $this->hasMany(TaskAssignment::class, 'linkId');
    }
}