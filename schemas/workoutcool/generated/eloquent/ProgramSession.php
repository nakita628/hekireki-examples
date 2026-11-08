<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ProgramSession extends Model
{
    protected $table = 'program_sessions';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'weekId',
        'sessionNumber',
        'title',
        'titleEn',
        'titleEs',
        'titlePt',
        'titleRu',
        'titleZhCn',
        'slug',
        'slugEn',
        'slugEs',
        'slugPt',
        'slugRu',
        'slugZhCn',
        'description',
        'descriptionEn',
        'descriptionEs',
        'descriptionPt',
        'descriptionRu',
        'descriptionZhCn',
        'equipment',
        'estimatedMinutes',
        'isPremium',
    ];

    protected $attributes = [
        'isPremium' => true,
    ];

    protected $casts = [
        'sessionNumber' => 'integer',
        'estimatedMinutes' => 'integer',
        'isPremium' => 'boolean',
    ];

    public function week(): BelongsTo
    {
        return $this->belongsTo(ProgramWeek::class, 'weekId');
    }

    public function exercises(): HasMany
    {
        return $this->hasMany(ProgramSessionExercise::class, 'sessionId');
    }

    public function userProgress(): HasMany
    {
        return $this->hasMany(UserSessionProgress::class, 'sessionId');
    }
}