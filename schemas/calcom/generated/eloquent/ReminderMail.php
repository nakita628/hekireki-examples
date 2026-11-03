<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ReminderMail extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ReminderMail';

    protected $fillable = [
        'referenceId',
        'reminderType',
        'elapsedMinutes',
    ];

    protected $casts = [
        'referenceId' => 'integer',
        'reminderType' => ReminderType::class,
        'elapsedMinutes' => 'integer',
    ];
}