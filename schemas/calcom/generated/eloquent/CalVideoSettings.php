<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class CalVideoSettings extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'CalVideoSettings';

    protected $primaryKey = 'eventTypeId';

    public $incrementing = false;

    protected $fillable = [
        'eventTypeId',
        'disableRecordingForOrganizer',
        'disableRecordingForGuests',
        'enableAutomaticTranscription',
        'enableAutomaticRecordingForOrganizer',
        'redirectUrlOnExit',
        'disableTranscriptionForGuests',
        'disableTranscriptionForOrganizer',
        'requireEmailForGuests',
    ];

    protected $attributes = [
        'disableRecordingForOrganizer' => false,
        'disableRecordingForGuests' => false,
        'enableAutomaticTranscription' => false,
        'enableAutomaticRecordingForOrganizer' => false,
        'disableTranscriptionForGuests' => false,
        'disableTranscriptionForOrganizer' => false,
        'requireEmailForGuests' => false,
    ];

    protected $casts = [
        'disableRecordingForOrganizer' => 'boolean',
        'disableRecordingForGuests' => 'boolean',
        'enableAutomaticTranscription' => 'boolean',
        'enableAutomaticRecordingForOrganizer' => 'boolean',
        'disableTranscriptionForGuests' => 'boolean',
        'disableTranscriptionForOrganizer' => 'boolean',
        'requireEmailForGuests' => 'boolean',
    ];

    public function eventType(): BelongsTo
    {
        return $this->belongsTo(EventType::class, 'eventTypeId');
    }
}