<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Conversation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Conversation';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'title',
        'isEnabled',
        'visibilityMode',
        'dataroomId',
        'dataroomDocumentId',
        'documentVersionNumber',
        'documentPageNumber',
        'linkId',
        'viewerGroupId',
        'questionId',
        'origin',
        'category',
        'priority',
        'recipientViewerId',
        'initialViewId',
        'teamId',
        'lastMessageAt',
    ];

    protected $attributes = [
        'isEnabled' => true,
        'visibilityMode' => 'PRIVATE',
        'origin' => 'visitor',
    ];

    protected $casts = [
        'isEnabled' => 'boolean',
        'visibilityMode' => ConversationVisibility::class,
        'documentVersionNumber' => 'integer',
        'documentPageNumber' => 'integer',
        'lastMessageAt' => 'datetime',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function dataroomDocument(): BelongsTo
    {
        return $this->belongsTo(DataroomDocument::class, 'dataroomDocumentId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function viewerGroup(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'viewerGroupId');
    }

    public function question(): BelongsTo
    {
        return $this->belongsTo(DataroomQuestion::class, 'questionId');
    }

    public function recipientViewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'recipientViewerId');
    }

    public function initialView(): BelongsTo
    {
        return $this->belongsTo(View::class, 'initialViewId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function views(): HasMany
    {
        return $this->hasMany(ConversationView::class, 'conversationId');
    }

    public function participants(): HasMany
    {
        return $this->hasMany(ConversationParticipant::class, 'conversationId');
    }

    public function messages(): HasMany
    {
        return $this->hasMany(Message::class, 'conversationId');
    }

    public function faqItems(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'sourceConversationId');
    }
}