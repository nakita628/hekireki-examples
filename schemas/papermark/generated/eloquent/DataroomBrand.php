<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DataroomBrand extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'DataroomBrand';

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
        'cardLayout',
        'showFolderTree',
        'viewerLayoutPreset',
        'viewerHeaderStyle',
        'hideFolderIconsInMain',
        'ctaLabel',
        'ctaUrl',
        'customLinkPreviewEnabled',
        'linkPreviewTitle',
        'linkPreviewDescription',
        'linkPreviewImage',
        'linkPreviewFavicon',
        'defaultLanguage',
        'dataroomId',
    ];

    protected $attributes = [
        'applyAccentColorToDataroomView' => false,
        'cardLayout' => 'LIST',
        'showFolderTree' => true,
        'viewerLayoutPreset' => 'STANDARD',
        'viewerHeaderStyle' => 'DEFAULT',
        'hideFolderIconsInMain' => false,
        'customLinkPreviewEnabled' => false,
        'defaultLanguage' => 'en',
    ];

    protected $casts = [
        'hideLogo' => 'boolean',
        'applyAccentColorToDataroomView' => 'boolean',
        'showFolderTree' => 'boolean',
        'hideFolderIconsInMain' => 'boolean',
        'customLinkPreviewEnabled' => 'boolean',
    ];

    public function dataroom(): BelongsTo
    {
        return $this->belongsTo(Dataroom::class, 'dataroomId');
    }
}