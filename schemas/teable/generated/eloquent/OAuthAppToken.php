<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OAuthAppToken extends Model
{
    use PrismaDates;

    protected $table = 'oauth_app_token';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'client_id',
        'app_secret_id',
        'refresh_token_sign',
        'expired_time',
        'created_time',
        'created_by',
    ];

    protected $casts = [
        'expired_time' => 'datetime',
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

    public function oAuthAppSecret(): BelongsTo
    {
        return $this->belongsTo(OAuthAppSecret::class, 'app_secret_id');
    }

    public function oAuthApp(): BelongsTo
    {
        return $this->belongsTo(OAuthApp::class, 'client_id', 'client_id');
    }
}