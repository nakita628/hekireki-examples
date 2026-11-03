<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class EventTypeTranslation extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'EventTypeTranslation';

    protected $primaryKey = 'uid';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'uid',
        'eventTypeId',
        'field',
        'sourceLocale',
        'targetLocale',
        'translatedText',
        'createdBy',
        'updatedBy',
    ];

    protected $casts = [
        'eventTypeId' => 'integer',
        'field' => EventTypeAutoTranslatedField::class,
        'createdBy' => 'integer',
        'updatedBy' => 'integer',
    ];

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }

    public function creator(): BelongsTo
    {
        return $this->belongsTo(User::class, 'createdBy');
    }

    public function updater(): BelongsTo
    {
        return $this->belongsTo(User::class, 'updatedBy');
    }
}