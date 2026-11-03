<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Tag extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Tag';

    protected $fillable = [
        'name',
        'ownerId',
        'archiveAsScreenshot',
        'archiveAsMonolith',
        'archiveAsPDF',
        'archiveAsReadable',
        'archiveAsWaybackMachine',
        'aiTag',
        'aiGenerated',
    ];

    protected $attributes = [
        'aiGenerated' => false,
    ];

    protected $casts = [
        'ownerId' => 'integer',
        'archiveAsScreenshot' => 'boolean',
        'archiveAsMonolith' => 'boolean',
        'archiveAsPDF' => 'boolean',
        'archiveAsReadable' => 'boolean',
        'archiveAsWaybackMachine' => 'boolean',
        'aiTag' => 'boolean',
        'aiGenerated' => 'boolean',
    ];

    public function owner(): BelongsTo
    {
        return $this->belongsTo(User::class, 'ownerId');
    }

    public function links(): BelongsToMany
    {
        return $this->belongsToMany(Link::class, '_LinkToTag', 'B', 'A');
    }
}