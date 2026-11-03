<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class SocialMediaAgency extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'SocialMediaAgency';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'userId',
        'name',
        'logoId',
        'website',
        'slug',
        'facebook',
        'instagram',
        'twitter',
        'linkedIn',
        'youtube',
        'tiktok',
        'otherSocialMedia',
        'shortDescription',
        'description',
        'approved',
        'deletedAt',
    ];

    protected $attributes = [
        'approved' => false,
    ];

    protected $casts = [
        'approved' => 'boolean',
        'deletedAt' => 'datetime',
    ];

    public function logo(): BelongsTo
    {
        return $this->belongsTo(Media::class, 'logoId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function niches(): HasMany
    {
        return $this->hasMany(SocialMediaAgencyNiche::class, 'agencyId');
    }
}