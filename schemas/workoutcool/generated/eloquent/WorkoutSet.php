<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class WorkoutSet extends Model
{
    protected $table = 'workout_sets';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'workoutSessionExerciseId',
        'setIndex',
        'type',
        'types',
        'valuesInt',
        'valuesSec',
        'units',
        'completed',
    ];

    protected $attributes = [
        'completed' => false,
    ];

    protected $casts = [
        'setIndex' => 'integer',
        'type' => WorkoutSetType::class,
        'completed' => 'boolean',
    ];

    public function workoutSessionExercise(): BelongsTo
    {
        return $this->belongsTo(WorkoutSessionExercise::class, 'workoutSessionExerciseId');
    }
}