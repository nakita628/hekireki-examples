<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class FilterSegment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'FilterSegment';

    protected $fillable = [
        'name',
        'tableIdentifier',
        'scope',
        'activeFilters',
        'sorting',
        'columnVisibility',
        'columnSizing',
        'perPage',
        'searchTerm',
        'userId',
        'teamId',
    ];

    protected $casts = [
        'scope' => FilterSegmentScope::class,
        'activeFilters' => 'array',
        'sorting' => 'array',
        'columnVisibility' => 'array',
        'columnSizing' => 'array',
        'perPage' => 'integer',
        'userId' => 'integer',
        'teamId' => 'integer',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function userPreferences(): HasMany
    {
        return $this->hasMany(UserFilterSegmentPreference::class, 'segmentId');
    }
}