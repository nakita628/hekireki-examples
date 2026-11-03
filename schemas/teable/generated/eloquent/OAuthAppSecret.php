<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class OAuthAppSecret extends Model
{
    use PrismaDates;

    protected $table = 'oauth_app_secret';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'client_id',
        'secret',
        'masked_secret',
        'created_time',
        'created_by',
        'last_used_time',
    ];

    protected $casts = [
        'created_time' => 'datetime',
        'last_used_time' => 'datetime',
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

    public function oAuthApp(): BelongsTo
    {
        return $this->belongsTo(OAuthApp::class, 'client_id', 'client_id');
    }

    public function tokens(): HasMany
    {
        return $this->hasMany(OAuthAppToken::class, 'app_secret_id');
    }
}