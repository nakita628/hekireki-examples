<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Comment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Comment';

    protected $fillable = [
        'text',
        'visible',
        'parentId',
        'userId',
        'rootType',
        'rootChallengeId',
        'rootSolutionId',
    ];

    protected $attributes = [
        'visible' => true,
        'rootType' => 'CHALLENGE',
    ];

    protected $casts = [
        'visible' => 'boolean',
        'parentId' => 'integer',
        'rootType' => CommentRoot::class,
        'rootChallengeId' => 'integer',
        'rootSolutionId' => 'integer',
    ];

    public function parentComment(): BelongsTo
    {
        return $this->belongsTo(Comment::class, 'parentId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function rootChallenge(): BelongsTo
    {
        return $this->belongsTo(Challenge::class, 'rootChallengeId');
    }

    public function rootSolution(): BelongsTo
    {
        return $this->belongsTo(SharedSolution::class, 'rootSolutionId');
    }

    public function replies(): HasMany
    {
        return $this->hasMany(Comment::class, 'parentId');
    }

    public function Report(): HasMany
    {
        return $this->hasMany(Report::class, 'commentId');
    }

    public function vote(): HasMany
    {
        return $this->hasMany(Vote::class, 'commentId');
    }

    public function Notification(): HasMany
    {
        return $this->hasMany(Notification::class, 'commentId');
    }
}