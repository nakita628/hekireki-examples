<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Notification extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Notification';

    protected $fillable = [
        'isRead',
        'type',
        'toUserId',
        'fromUserId',
        'commentId',
        'content',
        'blurb',
        'url',
        'challengeId',
        'sharedSolutionId',
    ];

    protected $attributes = [
        'isRead' => false,
    ];

    protected $casts = [
        'isRead' => 'boolean',
        'type' => NotificationType::class,
        'commentId' => 'integer',
        'challengeId' => 'integer',
        'sharedSolutionId' => 'integer',
    ];

    public function toUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'toUserId');
    }

    public function fromUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'fromUserId');
    }

    public function comment(): BelongsTo
    {
        return $this->belongsTo(Comment::class, 'commentId');
    }

    public function Challenge(): BelongsTo
    {
        return $this->belongsTo(Challenge::class, 'challengeId');
    }

    public function SharedSolution(): BelongsTo
    {
        return $this->belongsTo(SharedSolution::class, 'sharedSolutionId');
    }
}