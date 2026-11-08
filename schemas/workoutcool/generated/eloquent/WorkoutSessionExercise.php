<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class WorkoutSessionExercise extends Model
{
    protected $table = 'workout_session_exercises';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'workoutSessionId',
        'exerciseId',
        'order',
    ];

    protected $casts = [
        'order' => 'integer',
    ];

    public function workoutSession(): BelongsTo
    {
        return $this->belongsTo(WorkoutSession::class, 'workoutSessionId');
    }

    public function exercise(): BelongsTo
    {
        return $this->belongsTo(Exercise::class, 'exerciseId');
    }

    public function sets(): HasMany
    {
        return $this->hasMany(WorkoutSet::class, 'workoutSessionExerciseId');
    }
}