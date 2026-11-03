<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DataroomFaqItem extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DataroomFaqItem';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'title',
        'editedQuestion',
        'originalQuestion',
        'answer',
        'description',
        'dataroomId',
        'linkId',
        'dataroomDocumentId',
        'sourceConversationId',
        'questionMessageId',
        'answerMessageId',
        'teamId',
        'publishedByUserId',
        'visibilityMode',
        'status',
        'isAnonymized',
        'viewCount',
        'tags',
        'documentPageNumber',
        'documentVersionNumber',
    ];

    protected $attributes = [
        'visibilityMode' => 'PUBLIC_DATAROOM',
        'status' => 'PUBLISHED',
        'isAnonymized' => true,
        'viewCount' => 0,
    ];

    protected $casts = [
        'visibilityMode' => FaqVisibility::class,
        'status' => FaqStatus::class,
        'isAnonymized' => 'boolean',
        'viewCount' => 'integer',
        'documentPageNumber' => 'integer',
        'documentVersionNumber' => 'integer',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function dataroomDocument(): BelongsTo
    {
        return $this->belongsTo(DataroomDocument::class, 'dataroomDocumentId');
    }

    public function sourceConversation(): BelongsTo
    {
        return $this->belongsTo(Conversation::class, 'sourceConversationId');
    }

    public function questionMessage(): BelongsTo
    {
        return $this->belongsTo(Message::class, 'questionMessageId');
    }

    public function answerMessage(): BelongsTo
    {
        return $this->belongsTo(Message::class, 'answerMessageId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function publishedByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'publishedByUserId');
    }
}