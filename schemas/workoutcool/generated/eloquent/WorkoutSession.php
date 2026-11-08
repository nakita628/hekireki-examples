<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class WorkoutSession extends Model
{
    use PrismaDates;

    protected $table = 'workout_sessions';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userId',
        'startedAt',
        'endedAt',
        'duration',
        'muscles',
        'rating',
        'ratingComment',
    ];

    protected $casts = [
        'startedAt' => 'datetime',
        'endedAt' => 'datetime',
        'duration' => 'integer',
        'rating' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function userSessionProgress(): HasOne
    {
        return $this->hasOne(UserSessionProgress::class, 'workoutSessionId');
    }

    public function exercises(): HasMany
    {
        return $this->hasMany(WorkoutSessionExercise::class, 'workoutSessionId');
    }
}