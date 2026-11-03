<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class App extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'App';

    protected $primaryKey = 'slug';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'slug',
        'dirName',
        'keys',
        'categories',
        'enabled',
    ];

    protected $attributes = [
        'enabled' => false,
    ];

    protected $casts = [
        'keys' => 'array',
        'enabled' => 'boolean',
    ];

    public function credentials(): HasMany
    {
        return $this->hasMany(Credential::class, 'appId');
    }

    public function payments(): HasMany
    {
        return $this->hasMany(Payment::class, 'appId');
    }

    public function Webhook(): HasMany
    {
        return $this->hasMany(Webhook::class, 'appId');
    }

    public function ApiKey(): HasMany
    {
        return $this->hasMany(ApiKey::class, 'appId');
    }
}