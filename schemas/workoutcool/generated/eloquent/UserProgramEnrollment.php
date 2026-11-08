<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class UserProgramEnrollment extends Model
{
    use PrismaDates;

    protected $table = 'user_program_enrollments';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'userId',
        'programId',
        'enrolledAt',
        'currentWeek',
        'currentSession',
        'completedSessions',
        'isActive',
        'completedAt',
    ];

    protected $attributes = [
        'currentWeek' => 1,
        'currentSession' => 1,
        'completedSessions' => 0,
        'isActive' => true,
    ];

    protected $casts = [
        'enrolledAt' => 'datetime',
        'currentWeek' => 'integer',
        'currentSession' => 'integer',
        'completedSessions' => 'integer',
        'isActive' => 'boolean',
        'completedAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('enrolledAt', $this->attributes)) {
                $this->setAttribute('enrolledAt', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function program(): BelongsTo
    {
        return $this->belongsTo(Program::class, 'programId');
    }

    public function sessionProgress(): HasMany
    {
        return $this->hasMany(UserSessionProgress::class, 'enrollmentId');
    }
}