<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class View extends Model
{
    use PrismaDates;

    protected $table = 'View';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'linkId',
        'documentId',
        'dataroomId',
        'dataroomViewId',
        'viewerEmail',
        'viewerName',
        'verified',
        'viewedAt',
        'downloadedAt',
        'downloadType',
        'downloadMetadata',
        'viewType',
        'viewerId',
        'groupId',
        'isArchived',
        'teamId',
    ];

    protected $attributes = [
        'verified' => false,
        'viewType' => 'DOCUMENT_VIEW',
        'isArchived' => false,
    ];

    protected $casts = [
        'verified' => 'boolean',
        'viewedAt' => 'datetime',
        'downloadedAt' => 'datetime',
        'downloadType' => DownloadType::class,
        'downloadMetadata' => 'array',
        'viewType' => ViewType::class,
        'isArchived' => 'boolean',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('viewedAt', $this->attributes)) {
                $this->setAttribute('viewedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function document(): BelongsTo
    {
        return $this->belongsTo(Document::class, 'documentId');
    }

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function group(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'groupId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function feedbackResponse(): HasOne
    {
        return $this->hasOne(FeedbackResponse::class, 'viewId');
    }

    public function agreementResponse(): HasOne
    {
        return $this->hasOne(AgreementResponse::class, 'viewId');
    }

    public function customFieldResponse(): HasOne
    {
        return $this->hasOne(CustomFieldResponse::class, 'viewId');
    }

    public function reactions(): HasMany
    {
        return $this->hasMany(Reaction::class, 'viewId');
    }

    public function conversationViews(): HasMany
    {
        return $this->hasMany(ConversationView::class, 'viewId');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(Message::class, 'viewId');
    }

    public function initialConversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'initialViewId');
    }

    public function uploadedDocuments(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'viewId');
    }

    public function taskActivities(): HasMany
    {
        return $this->hasMany(TaskActivity::class, 'viewId');
    }

    public function chats(): HasMany
    {
        return $this->hasMany(Chat::class, 'viewId');
    }
}