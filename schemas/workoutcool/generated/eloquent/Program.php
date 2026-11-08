<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Program extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'programs';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'slug',
        'slugEn',
        'slugEs',
        'slugPt',
        'slugRu',
        'slugZhCn',
        'title',
        'titleEn',
        'titleEs',
        'titlePt',
        'titleRu',
        'titleZhCn',
        'description',
        'descriptionEn',
        'descriptionEs',
        'descriptionPt',
        'descriptionRu',
        'descriptionZhCn',
        'category',
        'image',
        'level',
        'type',
        'durationWeeks',
        'sessionsPerWeek',
        'sessionDurationMin',
        'equipment',
        'isPremium',
        'isActive',
        'visibility',
        'participantCount',
    ];

    protected $attributes = [
        'durationWeeks' => 4,
        'sessionsPerWeek' => 3,
        'sessionDurationMin' => 30,
        'isPremium' => true,
        'isActive' => true,
        'visibility' => 'DRAFT',
        'participantCount' => 0,
    ];

    protected $casts = [
        'level' => ProgramLevel::class,
        'type' => ExerciseAttributeValueEnum::class,
        'durationWeeks' => 'integer',
        'sessionsPerWeek' => 'integer',
        'sessionDurationMin' => 'integer',
        'isPremium' => 'boolean',
        'isActive' => 'boolean',
        'visibility' => ProgramVisibility::class,
        'participantCount' => 'integer',
    ];

    public function weeks(): HasMany
    {
        return $this->hasMany(ProgramWeek::class, 'programId');
    }

    public function enrollments(): HasMany
    {
        return $this->hasMany(UserProgramEnrollment::class, 'programId');
    }

    public function coaches(): HasMany
    {
        return $this->hasMany(ProgramCoach::class, 'programId');
    }
}