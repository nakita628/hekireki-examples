<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Dashboard extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Dashboard';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'linkId',
        'folderId',
        'projectId',
        'userId',
        'doIndex',
        'password',
        'showConversions',
    ];

    protected $attributes = [
        'doIndex' => false,
        'showConversions' => false,
    ];

    protected $casts = [
        'doIndex' => 'boolean',
        'showConversions' => 'boolean',
    ];

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function folder(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'folderId');
    }

    public function project(): BelongsTo
    {
        return $this->belongsTo(Project::class, 'projectId');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}