<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class LinkPreset extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'LinkPreset';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'name',
        'teamId',
        'pId',
        'enableCustomMetaTag',
        'metaTitle',
        'metaDescription',
        'metaImage',
        'metaFavicon',
        'enableNotification',
        'emailProtected',
        'emailAuthenticated',
        'allowDownload',
        'enableAllowList',
        'allowList',
        'enableDenyList',
        'denyList',
        'expiresIn',
        'enableScreenshotProtection',
        'enableConfidentialView',
        'expiresAt',
        'enablePassword',
        'password',
        'enableWatermark',
        'watermarkConfig',
        'enableAgreement',
        'agreementId',
        'enableCustomFields',
        'customFields',
        'showBanner',
        'welcomeMessage',
        'isDefault',
    ];

    protected $attributes = [
        'enableCustomMetaTag' => false,
        'enableNotification' => false,
        'emailProtected' => true,
        'emailAuthenticated' => false,
        'allowDownload' => false,
        'enableAllowList' => false,
        'enableDenyList' => false,
        'enableScreenshotProtection' => false,
        'enableConfidentialView' => false,
        'enablePassword' => false,
        'enableWatermark' => false,
        'enableAgreement' => false,
        'enableCustomFields' => false,
        'showBanner' => false,
        'isDefault' => false,
    ];

    protected $casts = [
        'enableCustomMetaTag' => 'boolean',
        'enableNotification' => 'boolean',
        'emailProtected' => 'boolean',
        'emailAuthenticated' => 'boolean',
        'allowDownload' => 'boolean',
        'enableAllowList' => 'boolean',
        'enableDenyList' => 'boolean',
        'expiresIn' => 'integer',
        'enableScreenshotProtection' => 'boolean',
        'enableConfidentialView' => 'boolean',
        'expiresAt' => 'datetime',
        'enablePassword' => 'boolean',
        'enableWatermark' => 'boolean',
        'watermarkConfig' => 'array',
        'enableAgreement' => 'boolean',
        'enableCustomFields' => 'boolean',
        'customFields' => 'array',
        'showBanner' => 'boolean',
        'isDefault' => 'boolean',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }
}