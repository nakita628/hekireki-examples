<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Message extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Message';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'content',
        'conversationId',
        'userId',
        'viewerId',
        'viewId',
        'isRead',
        'isInternalNote',
    ];

    protected $attributes = [
        'isRead' => false,
        'isInternalNote' => false,
    ];

    protected $casts = [
        'isRead' => 'boolean',
        'isInternalNote' => 'boolean',
    ];

    public function conversation(): BelongsTo
    {
        return $this->belongsTo(Conversation::class, 'conversationId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function view(): BelongsTo
    {
        return $this->belongsTo(View::class, 'viewId');
    }

    public function faqAsQuestion(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'questionMessageId');
    }

    public function faqAsAnswer(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'answerMessageId');
    }
}