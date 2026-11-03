<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OAuthApp extends Model
{
    use PrismaDates;

    const CREATED_AT = null;
    const UPDATED_AT = 'last_modified_time';

    protected $table = 'oauth_app';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'logo',
        'homepage',
        'description',
        'client_id',
        'redirect_uris',
        'scopes',
        'allow_device_flow',
        'dynamic_registration',
        'created_time',
        'created_by',
    ];

    protected $attributes = [
        'allow_device_flow' => false,
        'dynamic_registration' => false,
    ];

    protected $casts = [
        'allow_device_flow' => 'boolean',
        'dynamic_registration' => 'boolean',
        'created_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function secrets(): HasMany
    {
        return $this->hasMany(OAuthAppSecret::class, 'client_id', 'client_id');
    }

    public function authorized(): HasMany
    {
        return $this->hasMany(OAuthAppAuthorized::class, 'client_id', 'client_id');
    }

    public function tokens(): HasMany
    {
        return $this->hasMany(OAuthAppToken::class, 'client_id', 'client_id');
    }
}