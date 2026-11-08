<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Report extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Report';

    protected $fillable = [
        'text',
        'type',
        'status',
        'reporterId',
        'userId',
        'challengeId',
        'commentId',
        'solutionId',
        'moderatorId',
    ];

    protected $casts = [
        'type' => ReportType::class,
        'status' => ReportStatus::class,
        'challengeId' => 'integer',
        'commentId' => 'integer',
        'solutionId' => 'integer',
    ];

    public function reporter(): BelongsTo
    {
        return $this->belongsTo(User::class, 'reporterId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function challenge(): BelongsTo
    {
        return $this->belongsTo(Challenge::class, 'challengeId');
    }

    public function comment(): BelongsTo
    {
        return $this->belongsTo(Comment::class, 'commentId');
    }

    public function solution(): BelongsTo
    {
        return $this->belongsTo(SharedSolution::class, 'solutionId');
    }

    public function moderator(): BelongsTo
    {
        return $this->belongsTo(User::class, 'moderatorId');
    }

    public function issues(): HasMany
    {
        return $this->hasMany(ReportIssue::class, 'reportId');
    }
}