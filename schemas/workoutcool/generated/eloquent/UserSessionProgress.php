<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class UserSessionProgress extends Model
{
    use PrismaDates;

    protected $table = 'user_session_progress';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'enrollmentId',
        'sessionId',
        'startedAt',
        'completedAt',
        'workoutSessionId',
    ];

    protected $casts = [
        'startedAt' => 'datetime',
        'completedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('startedAt', $this->attributes)) {
                $this->setAttribute('startedAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function enrollment(): BelongsTo
    {
        return $this->belongsTo(UserProgramEnrollment::class, 'enrollmentId');
    }

    public function session(): BelongsTo
    {
        return $this->belongsTo(ProgramSession::class, 'sessionId');
    }

    public function workoutSession(): BelongsTo
    {
        return $this->belongsTo(WorkoutSession::class, 'workoutSessionId');
    }
}