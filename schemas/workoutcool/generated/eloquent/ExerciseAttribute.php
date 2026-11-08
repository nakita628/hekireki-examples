<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ExerciseAttribute extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'exercise_attributes';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'exerciseId',
        'attributeNameId',
        'attributeValueId',
    ];

    public function exercise(): BelongsTo
    {
        return $this->belongsTo(Exercise::class, 'exerciseId');
    }

    public function attributeName(): BelongsTo
    {
        return $this->belongsTo(ExerciseAttributeName::class, 'attributeNameId');
    }

    public function attributeValue(): BelongsTo
    {
        return $this->belongsTo(ExerciseAttributeValue::class, 'attributeValueId');
    }
}