<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SharedSolution extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'SharedSolution';

    protected $fillable = [
        'isPinned',
        'title',
        'description',
        'userId',
        'challengeId',
    ];

    protected $attributes = [
        'isPinned' => false,
    ];

    protected $casts = [
        'isPinned' => 'boolean',
        'challengeId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function challenge(): BelongsTo
    {
        return $this->belongsTo(Challenge::class, 'challengeId');
    }

    public function vote(): HasMany
    {
        return $this->hasMany(Vote::class, 'sharedSolutionId');
    }

    public function solutionComment(): HasMany
    {
        return $this->hasMany(Comment::class, 'rootSolutionId');
    }

    public function Report(): HasMany
    {
        return $this->hasMany(Report::class, 'solutionId');
    }

    public function Notification(): HasMany
    {
        return $this->hasMany(Notification::class, 'sharedSolutionId');
    }
}