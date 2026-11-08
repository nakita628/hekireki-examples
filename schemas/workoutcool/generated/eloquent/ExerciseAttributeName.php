<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class ExerciseAttributeName extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'exercise_attribute_names';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
    ];

    protected $casts = [
        'name' => ExerciseAttributeNameEnum::class,
    ];

    public function values(): HasMany
    {
        return $this->hasMany(ExerciseAttributeValue::class, 'attributeNameId');
    }

    public function attributes(): HasMany
    {
        return $this->hasMany(ExerciseAttribute::class, 'attributeNameId');
    }
}