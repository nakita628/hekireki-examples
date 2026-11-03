<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class OrganizationDataStore extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OrganizationDataStore';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'key',
        'organizationIds',
        'kind',
        'config',
    ];

    protected $casts = [
        'kind' => DataStoreKind::class,
        'config' => 'array',
    ];
}