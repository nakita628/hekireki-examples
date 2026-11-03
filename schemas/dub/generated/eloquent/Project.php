<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Project extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Project';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'slug',
        'logo',
        'inviteCode',
        'plan',
        'planTier',
        'planPeriod',
        'stripeId',
        'billingCycleStart',
        'billingCycleEndsAt',
        'trialEndsAt',
        'subscriptionCanceledAt',
        'paymentFailedAt',
        'invoicePrefix',
        'stripeConnectId',
        'stripeConnectMode',
        'shopifyStoreId',
        'totalLinks',
        'totalClicks',
        'usage',
        'usageLimit',
        'linksUsage',
        'linksLimit',
        'domainsLimit',
        'tagsLimit',
        'foldersUsage',
        'foldersLimit',
        'usersLimit',
        'aiUsage',
        'aiLimit',
        'store',
        'siteVisitTrackingSettings',
        'allowedHostnames',
        'publishableKey',
        'conversionEnabled',
        'webhookEnabled',
        'dotLinkClaimed',
        'ssoEmailDomain',
        'ssoEnforcedAt',
        'usageLastChecked',
        'disabledAt',
    ];

    protected $attributes = [
        'plan' => 'free',
        'planTier' => 1,
        'totalLinks' => 0,
        'totalClicks' => 0,
        'usage' => 0,
        'usageLimit' => 1000,
        'linksUsage' => 0,
        'linksLimit' => 25,
        'domainsLimit' => 3,
        'tagsLimit' => 5,
        'foldersUsage' => 0,
        'foldersLimit' => 0,
        'usersLimit' => 1,
        'aiUsage' => 0,
        'aiLimit' => 10,
        'conversionEnabled' => false,
        'webhookEnabled' => false,
        'dotLinkClaimed' => false,
    ];

    protected $casts = [
        'planTier' => 'integer',
        'planPeriod' => PlanPeriod::class,
        'billingCycleStart' => 'integer',
        'billingCycleEndsAt' => 'datetime',
        'trialEndsAt' => 'datetime',
        'subscriptionCanceledAt' => 'datetime',
        'paymentFailedAt' => 'datetime',
        'stripeConnectMode' => StripeConnectMode::class,
        'totalLinks' => 'integer',
        'totalClicks' => 'integer',
        'usage' => 'integer',
        'usageLimit' => 'integer',
        'linksUsage' => 'integer',
        'linksLimit' => 'integer',
        'domainsLimit' => 'integer',
        'tagsLimit' => 'integer',
        'foldersUsage' => 'integer',
        'foldersLimit' => 'integer',
        'usersLimit' => 'integer',
        'aiUsage' => 'integer',
        'aiLimit' => 'integer',
        'store' => 'array',
        'siteVisitTrackingSettings' => 'array',
        'allowedHostnames' => 'array',
        'conversionEnabled' => 'boolean',
        'webhookEnabled' => 'boolean',
        'dotLinkClaimed' => 'boolean',
        'ssoEnforcedAt' => 'datetime',
        'usageLastChecked' => 'datetime',
        'disabledAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('usageLastChecked', $this->attributes)) {
                $this->setAttribute('usageLastChecked', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function users(): HasMany
    {
        return $this->hasMany(ProjectUsers::class, 'projectId');
    }

    public function invites(): HasMany
    {
        return $this->hasMany(ProjectInvite::class, 'projectId');
    }

    public function sentEmails(): HasMany
    {
        return $this->hasMany(SentEmail::class, 'projectId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'projectId');
    }

    public function domains(): HasMany
    {
        return $this->hasMany(Domain::class, 'projectId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(Tag::class, 'projectId');
    }

    public function invoices(): HasMany
    {
        return $this->hasMany(Invoice::class, 'workspaceId');
    }

    public function customers(): HasMany
    {
        return $this->hasMany(Customer::class, 'projectId');
    }

    public function defaultDomains(): HasMany
    {
        return $this->hasMany(DefaultDomains::class, 'projectId');
    }

    public function restrictedTokens(): HasMany
    {
        return $this->hasMany(RestrictedToken::class, 'projectId');
    }

    public function oAuthCodes(): HasMany
    {
        return $this->hasMany(OAuthCode::class, 'projectId');
    }

    public function integrations(): HasMany
    {
        return $this->hasMany(Integration::class, 'projectId');
    }

    public function installedIntegrations(): HasMany
    {
        return $this->hasMany(InstalledIntegration::class, 'projectId');
    }

    public function webhooks(): HasMany
    {
        return $this->hasMany(Webhook::class, 'projectId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(Folder::class, 'projectId');
    }

    public function registeredDomains(): HasMany
    {
        return $this->hasMany(RegisteredDomain::class, 'projectId');
    }

    public function dashboards(): HasMany
    {
        return $this->hasMany(Dashboard::class, 'projectId');
    }

    public function utmTemplates(): HasMany
    {
        return $this->hasMany(UtmTemplate::class, 'projectId');
    }

    public function yearInReviews(): HasMany
    {
        return $this->hasMany(YearInReview::class, 'workspaceId');
    }
}