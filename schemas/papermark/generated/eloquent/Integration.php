<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Integration extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Integration';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'slug',
        'description',
        'readme',
        'developer',
        'website',
        'logo',
        'screenshots',
        'verified',
        'installUrl',
        'category',
        'comingSoon',
    ];

    protected $attributes = [
        'verified' => false,
        'comingSoon' => false,
    ];

    protected $casts = [
        'screenshots' => 'array',
        'verified' => 'boolean',
        'comingSoon' => 'boolean',
    ];

    public function installedIntegrations(): HasMany
    {
        return $this->hasMany(InstalledIntegration::class, 'integrationId');
    }
}