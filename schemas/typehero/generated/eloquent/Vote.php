<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Vote extends Model
{
    protected $table = 'Vote';

    public $timestamps = false;

    protected $fillable = [
        'userId',
        'rootType',
        'challengeId',
        'commentId',
        'submissionId',
        'sharedSolutionId',
    ];

    protected $casts = [
        'rootType' => VoteType::class,
        'challengeId' => 'integer',
        'commentId' => 'integer',
        'submissionId' => 'integer',
        'sharedSolutionId' => 'integer',
    ];

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

    public function submission(): BelongsTo
    {
        return $this->belongsTo(Submission::class, 'submissionId');
    }

    public function sharedSolution(): BelongsTo
    {
        return $this->belongsTo(SharedSolution::class, 'sharedSolutionId');
    }
}