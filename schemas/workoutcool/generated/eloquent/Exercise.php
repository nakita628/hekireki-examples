<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Exercise extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'exercises';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'nameEn',
        'description',
        'descriptionEn',
        'fullVideoUrl',
        'fullVideoImageUrl',
        'introduction',
        'introductionEn',
        'slug',
        'slugEn',
    ];

    public function attributes(): HasMany
    {
        return $this->hasMany(ExerciseAttribute::class, 'exerciseId');
    }

    public function WorkoutSessionExercise(): HasMany
    {
        return $this->hasMany(WorkoutSessionExercise::class, 'exerciseId');
    }

    public function ProgramSessionExercise(): HasMany
    {
        return $this->hasMany(ProgramSessionExercise::class, 'exerciseId');
    }

    public function favoritesByUsers(): HasMany
    {
        return $this->hasMany(UserFavoriteExercise::class, 'exerciseId');
    }
}