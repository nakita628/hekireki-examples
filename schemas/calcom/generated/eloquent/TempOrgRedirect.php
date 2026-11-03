<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TempOrgRedirect extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'TempOrgRedirect';

    protected $fillable = [
        'from',
        'fromOrgId',
        'type',
        'toUrl',
        'enabled',
    ];

    protected $attributes = [
        'enabled' => true,
    ];

    protected $casts = [
        'fromOrgId' => 'integer',
        'type' => RedirectType::class,
        'enabled' => 'boolean',
    ];
}