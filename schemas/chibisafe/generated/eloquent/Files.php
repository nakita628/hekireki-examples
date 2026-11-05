<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Files extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'files';

    protected $fillable = [
        'uuid',
        'userId',
        'name',
        'original',
        'type',
        'size',
        'hash',
        'ip',
        'sourceUrl',
        'isS3',
        'isWatched',
        'editedAt',
        'quarantine',
    ];

    protected $attributes = [
        'isS3' => false,
        'isWatched' => false,
        'quarantine' => false,
    ];

    protected $casts = [
        'userId' => 'integer',
        'isS3' => 'boolean',
        'isWatched' => 'boolean',
        'editedAt' => 'datetime',
        'quarantine' => 'boolean',
    ];

    public function user(): BelongsTo
    {
        return $this->belongsTo(Users::class, 'userId');
    }

    public function quarantineFile(): HasOne
    {
        return $this->hasOne(QuarantineFiles::class, 'fileId', 'uuid');
    }

    public function albums(): BelongsToMany
    {
        return $this->belongsToMany(Albums::class, '_albumsTofiles', 'B', 'A');
    }

    public function tags(): BelongsToMany
    {
        return $this->belongsToMany(Tags::class, '_filesTotags', 'A', 'B');
    }
}