<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Brand extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Brand';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'logo',
        'hideLogo',
        'banner',
        'brandColor',
        'accentColor',
        'accentButtonColor',
        'applyAccentColorToDataroomView',
        'welcomeMessage',
        'ctaLabel',
        'ctaUrl',
        'privacyPolicyUrl',
        'cardLayout',
        'showFolderTree',
        'viewerLayoutPreset',
        'viewerHeaderStyle',
        'hideFolderIconsInMain',
        'customLinkPreviewEnabled',
        'linkPreviewTitle',
        'linkPreviewDescription',
        'linkPreviewImage',
        'linkPreviewFavicon',
        'defaultLanguage',
        'name',
        'teamId',
    ];

    protected $attributes = [
        'hideLogo' => false,
        'applyAccentColorToDataroomView' => false,
        'cardLayout' => 'LIST',
        'showFolderTree' => true,
        'viewerLayoutPreset' => 'STANDARD',
        'viewerHeaderStyle' => 'DEFAULT',
        'hideFolderIconsInMain' => false,
        'customLinkPreviewEnabled' => false,
        'defaultLanguage' => 'en',
        'name' => 'Default',
    ];

    protected $casts = [
        'hideLogo' => 'boolean',
        'applyAccentColorToDataroomView' => 'boolean',
        'showFolderTree' => 'boolean',
        'hideFolderIconsInMain' => 'boolean',
        'customLinkPreviewEnabled' => 'boolean',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function defaultForTeam(): HasMany
    {
        return $this->hasMany(Team::class, 'defaultBrandId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'brandId');
    }

    public function datarooms(): HasMany
    {
        return $this->hasMany(Dataroom::class, 'brandId');
    }
}