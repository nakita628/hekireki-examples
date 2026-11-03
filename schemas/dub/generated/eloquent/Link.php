<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Link extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Link';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'domain',
        'key',
        'url',
        'shortLink',
        'archived',
        'expiresAt',
        'expiredUrl',
        'disabledAt',
        'password',
        'trackConversion',
        'proxy',
        'title',
        'description',
        'image',
        'video',
        'utm_source',
        'utm_medium',
        'utm_campaign',
        'utm_term',
        'utm_content',
        'rewrite',
        'linkRetentionCleanupDisabledAt',
        'doIndex',
        'ios',
        'android',
        'geo',
        'testVariants',
        'testStartedAt',
        'testCompletedAt',
        'userId',
        'projectId',
        'folderId',
        'externalId',
        'tenantId',
        'publicStats',
        'clicks',
        'leads',
        'conversions',
        'sales',
        'saleAmount',
        'lastClicked',
        'lastLeadAt',
        'lastConversionAt',
        'comments',
    ];

    protected $attributes = [
        'archived' => false,
        'trackConversion' => false,
        'proxy' => false,
        'rewrite' => false,
        'doIndex' => false,
        'publicStats' => false,
        'clicks' => 0,
        'leads' => 0,
        'conversions' => 0,
        'sales' => 0,
        'saleAmount' => 0,
    ];

    protected $casts = [
        'archived' => 'boolean',
        'expiresAt' => 'datetime',
        'disabledAt' => 'datetime',
        'trackConversion' => 'boolean',
        'proxy' => 'boolean',
        'rewrite' => 'boolean',
        'linkRetentionCleanupDisabledAt' => 'datetime',
        'doIndex' => 'boolean',
        'geo' => 'array',
        'testVariants' => 'array',
        'testStartedAt' => 'datetime',
        'testCompletedAt' => 'datetime',
        'publicStats' => 'boolean',
        'clicks' => 'integer',
        'leads' => 'integer',
        'conversions' => 'integer',
        'sales' => 'integer',
        'saleAmount' => 'integer',
        'lastClicked' => 'datetime',
        'lastLeadAt' => 'datetime',
        'lastConversionAt' => 'datetime',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function folder(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'folderId');
    }

    public function shortDomain(): BelongsTo
    {
        return $this->belongsTo(Domain::class, 'domain', 'slug');
    }

    public function dashboard(): HasOne
    {
        return $this->hasOne(Dashboard::class, 'linkId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(LinkTag::class, 'linkId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(LinkWebhook::class, 'linkId');
    }

    public function customers(): HasMany
    {
        return $this->hasMany(Customer::class, 'linkId');
    }
}