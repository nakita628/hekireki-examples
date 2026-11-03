<?php

namespace App\Models;


class ObservationMedia extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    protected $table = 'observation_media';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'project_id',
        'media_id',
        'trace_id',
        'observation_id',
        'field',
        'origin',
    ];

    protected $attributes = [
        'origin' => 'UNKNOWN',
    ];

    protected $casts = [
        'origin' => MediaAssociationOrigin::class,
    ];
}