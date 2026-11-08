<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Challenge extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Challenge';

    protected $fillable = [
        'difficulty',
        'name',
        'slug',
        'description',
        'shortDescription',
        'code',
        'tests',
        'status',
        'tsconfig',
        'userId',
    ];

    protected $attributes = [
        'status' => 'PENDING',
    ];

    protected $casts = [
        'difficulty' => Difficulty::class,
        'status' => ChallengeStatus::class,
        'tsconfig' => 'array',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function vote(): HasMany
    {
        return $this->hasMany(Vote::class, 'challengeId');
    }

    public function submission(): HasMany
    {
        return $this->hasMany(Submission::class, 'challengeId');
    }

    public function comment(): HasMany
    {
        return $this->hasMany(Comment::class, 'rootChallengeId');
    }

    public function bookmark(): HasMany
    {
        return $this->hasMany(Bookmark::class, 'challengeId');
    }

    public function sharedSolution(): HasMany
    {
        return $this->hasMany(SharedSolution::class, 'challengeId');
    }

    public function Report(): HasMany
    {
        return $this->hasMany(Report::class, 'challengeId');
    }

    public function TrackChallenge(): HasMany
    {
        return $this->hasMany(TrackChallenge::class, 'challengeId');
    }

    public function Notification(): HasMany
    {
        return $this->hasMany(Notification::class, 'challengeId');
    }

    public function tags(): BelongsToMany
    {
        return $this->belongsToMany(Tag::class, '_ChallengeToTag', 'A', 'B');
    }
}