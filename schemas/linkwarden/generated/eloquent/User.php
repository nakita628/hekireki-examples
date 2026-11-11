<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;
use Illuminate\Support\Str;

class User extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'User';

    protected $fillable = [
        'uuid',
        'name',
        'username',
        'email',
        'emailVerified',
        'unverifiedNewEmail',
        'image',
        'password',
        'locale',
        'parentSubscriptionId',
        'collectionOrder',
        'linksRouteTo',
        'aiTaggingMethod',
        'aiPredefinedTags',
        'aiTagExistingLinks',
        'theme',
        'dismissedAnnouncementId',
        'readableFontFamily',
        'readableFontSize',
        'readableLineHeight',
        'readableLineWidth',
        'preventDuplicateLinks',
        'archiveAsScreenshot',
        'archiveAsMonolith',
        'archiveAsPDF',
        'archiveAsReadable',
        'archiveAsWaybackMachine',
        'referredBy',
        'lastPickedAt',
        'acceptPromotionalEmails',
        'shareUsageAnalytics',
        'trialEndEmailSent',
        'sessionVersion',
        'passwordRetries',
        'passwordRetryAt',
    ];

    protected $attributes = [
        'locale' => 'en',
        'linksRouteTo' => 'ORIGINAL',
        'aiTaggingMethod' => 'DISABLED',
        'aiTagExistingLinks' => false,
        'theme' => 'dark',
        'readableFontFamily' => 'sans-serif',
        'readableFontSize' => '20px',
        'readableLineHeight' => '1.8',
        'readableLineWidth' => 'normal',
        'preventDuplicateLinks' => false,
        'archiveAsScreenshot' => true,
        'archiveAsMonolith' => true,
        'archiveAsPDF' => true,
        'archiveAsReadable' => true,
        'archiveAsWaybackMachine' => false,
        'acceptPromotionalEmails' => false,
        'shareUsageAnalytics' => true,
        'trialEndEmailSent' => false,
        'sessionVersion' => 0,
        'passwordRetries' => 0,
    ];

    protected $casts = [
        'emailVerified' => 'datetime',
        'parentSubscriptionId' => 'integer',
        'linksRouteTo' => LinksRouteTo::class,
        'aiTaggingMethod' => AiTaggingMethod::class,
        'aiTagExistingLinks' => 'boolean',
        'theme' => Theme::class,
        'preventDuplicateLinks' => 'boolean',
        'archiveAsScreenshot' => 'boolean',
        'archiveAsMonolith' => 'boolean',
        'archiveAsPDF' => 'boolean',
        'archiveAsReadable' => 'boolean',
        'archiveAsWaybackMachine' => 'boolean',
        'lastPickedAt' => 'datetime',
        'acceptPromotionalEmails' => 'boolean',
        'shareUsageAnalytics' => 'boolean',
        'trialEndEmailSent' => 'boolean',
        'sessionVersion' => 'integer',
        'passwordRetries' => 'integer',
        'passwordRetryAt' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('uuid', $this->attributes)) {
                $this->setAttribute('uuid', (string) Str::orderedUuid());
            }
        }

        return parent::save($options);
    }

    public function parentSubscription(): BelongsTo
    {
        return $this->belongsTo(Subscription::class, 'parentSubscriptionId');
    }

    public function subscriptions(): HasOne
    {
        return $this->hasOne(Subscription::class, 'userId');
    }

    public function accounts(): HasMany
    {
        return $this->hasMany(Account::class, 'userId');
    }

    public function collections(): HasMany
    {
        return $this->hasMany(Collection::class, 'ownerId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(Tag::class, 'ownerId');
    }

    public function createdLinks(): HasMany
    {
        return $this->hasMany(Link::class, 'createdById');
    }

    public function createdCollections(): HasMany
    {
        return $this->hasMany(Collection::class, 'createdById');
    }

    public function highlights(): HasMany
    {
        return $this->hasMany(Highlight::class, 'userId');
    }

    public function collectionsJoined(): HasMany
    {
        return $this->hasMany(UsersAndCollections::class, 'userId');
    }

    public function accessTokens(): HasMany
    {
        return $this->hasMany(AccessToken::class, 'userId');
    }

    public function dashboardSections(): HasMany
    {
        return $this->hasMany(DashboardSection::class, 'userId');
    }

    public function pinnedLinks(): BelongsToMany
    {
        return $this->belongsToMany(Link::class, '_PinnedLinks', 'B', 'A');
    }
}