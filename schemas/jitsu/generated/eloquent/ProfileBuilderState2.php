<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ProfileBuilderState2 extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ProfileBuilderState2';

    protected $primaryKey = 'profileBuilderId';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'profileBuilderId',
        'fullRebuildInfo',
        'queuesInfo',
        'metrics',
    ];

    protected $casts = [
        'fullRebuildInfo' => 'array',
        'queuesInfo' => 'array',
        'metrics' => 'array',
    ];
}