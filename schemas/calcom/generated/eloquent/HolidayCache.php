<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;

class HolidayCache extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'HolidayCache';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'countryCode',
        'calendarId',
        'eventId',
        'name',
        'date',
        'year',
    ];

    protected $casts = [
        'date' => 'datetime',
        'year' => 'integer',
    ];
}