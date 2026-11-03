<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Session extends Model
{
    use PrismaDates;

    const UPDATED_AT = null;
    const ZONED_DATES = ['created_at'];

    protected $table = 'session';

    protected $primaryKey = 'session_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'session_id',
        'website_id',
        'browser',
        'os',
        'device',
        'screen',
        'language',
        'country',
        'region',
        'city',
        'distinct_id',
    ];

    public function websiteEvents(): HasMany
    {
        return $this->hasMany(WebsiteEvent::class, 'session_id');
    }

    public function sessionData(): HasMany
    {
        return $this->hasMany(SessionData::class, 'session_id');
    }

    public function revenue(): HasMany
    {
        return $this->hasMany(Revenue::class, 'session_id');
    }
}