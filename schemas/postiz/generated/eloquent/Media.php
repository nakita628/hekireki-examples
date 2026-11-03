<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Media extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Media';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'name',
        'originalName',
        'path',
        'organizationId',
        'deletedAt',
        'fileSize',
        'type',
        'thumbnail',
        'alt',
        'thumbnailTimestamp',
        'status',
        'processingError',
    ];

    protected $attributes = [
        'fileSize' => 0,
        'type' => 'image',
        'status' => 'ready',
    ];

    protected $casts = [
        'deletedAt' => 'datetime',
        'fileSize' => 'integer',
        'thumbnailTimestamp' => 'integer',
    ];

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function agencies(): HasMany
    {
        return $this->hasMany(SocialMediaAgency::class, 'logoId');
    }

    public function userPicture(): HasMany
    {
        return $this->hasMany(User::class, 'pictureId');
    }

    public function oauthApps(): HasMany
    {
        return $this->hasMany(OAuthApp::class, 'pictureId');
    }
}