<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OAuthAppAuthorized extends Model
{
    use PrismaDates;

    protected $table = 'oauth_app_authorized';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'client_id',
        'user_id',
        'authorized_time',
    ];

    protected $casts = [
        'authorized_time' => 'datetime',
    ];

    public function oAuthApp(): BelongsTo
    {
        return $this->belongsTo(OAuthApp::class, 'client_id', 'client_id');
    }
}